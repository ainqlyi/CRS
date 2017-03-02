using System.Globalization;
using System.Net;
using log4net;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using System.Configuration;

namespace CRS.ReportExtractor
{
    /// <summary>
    /// This program runs HSCRC reports and extracts them to a folder in Excel format.
    /// </summary>
    internal class Program
    {
        //Initialize logger
        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main()
        {
            try
            {
                var automationConfig = new XmlDocument();
                automationConfig.Load("AutomationConfig.xml");

                var reports = automationConfig.SelectNodes("//Reports//Report");

                if (reports != null)
                    foreach (XmlNode report in reports)
                    {
                        var xmlElement = report["ExportReport"];
                        if (xmlElement == null || xmlElement.InnerText != "true") continue;
                        var element = report["ReportPath"];
                        if (element == null) continue;
                        var reportPath = element.InnerText;
                        var xmlElement1 = report["FileName"];
                        if (xmlElement1 == null) continue;
                        var fileName = xmlElement1.InnerText;
                        var element1 = report["ExportFormat"];
                        if (element1 == null) continue;
                        var exportFormat = element1.InnerText;
                        var xmlElement2 = report["HospitalListName"];
                        if (xmlElement2 == null) continue;
                        var hospitalListName = xmlElement2.InnerText;

                        var t = new Thread(() => CreateFiles(reportPath, fileName, exportFormat, hospitalListName));
                        t.Start();

                        Log.Info(String.Format("Starting execution: {0}-{1}-{2}", reportPath, exportFormat,
                            hospitalListName));
                    }
            }
            catch (Exception e)
            {
                Log.Error("Exception caught at root", e);
            }

            Log.Info("Exiting application");
        }

        /// <summary>
        /// This method uses a CSV file to construct a datatable with the sourceId, sourceCode, and hospitalSystem
        /// for each desired hospital.
        /// </summary>
        /// <param name="csvFilePath">The location to search for the file</param>
        /// <returns>a datatable with the contents of the CSV file</returns>
        private static DataTable GetDataTableFromCsvFile(string csvFilePath)
        {
            var csvData = new DataTable();
            using (var csvReader = new TextFieldParser(csvFilePath))
            {
                csvReader.SetDelimiters(new string[] {","});
                csvReader.HasFieldsEnclosedInQuotes = true;
                //read column names
                var colFields = csvReader.ReadFields();
                if (colFields != null)
                    foreach (var datacolumn in colFields.Select(column => new DataColumn(column)))
                    {
                        datacolumn.AllowDBNull = true;
                        csvData.Columns.Add(datacolumn);
                    }
                while (!csvReader.EndOfData)
                {
                    object[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (var i = 0; i < fieldData.Length; i++)
                    {
                        if ((string) fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
            return csvData;
        }

        /// <summary>
        /// Runs reports in SSRS and creates them in a specific folder
        /// </summary>
        /// <param name="reportPath">File system location to create the report</param>
        /// <param name="fileName">The name of the file to create</param>
        /// <param name="exportFormat">Format of file to export</param>
        /// <param name="hospitalListName">The parameter to use for the hospital name</param>
        private static void CreateFiles(string reportPath, string fileName, string exportFormat, string hospitalListName)
        {
            //var csvMappingFile = ConfigurationManager.AppSettings["ReportExtractorOutputFormat"];
            DataTable hospitalMapping =
                GetDataTableFromCsvFile(ConfigurationManager.AppSettings["ReportExtractorOutputFormat"]);
            DataTable excludedHospitals = GetDataTableFromCsvFile(ConfigurationManager.AppSettings["excludedHospitals"]);
            var excludedHospitalsList =
                excludedHospitals.AsEnumerable().Select(row => row.Field<string>("sourceID")).ToArray();

            var rs = new ReportExecutionService
            {
                Credentials =
                    new NetworkCredential(
                        ConfigurationManager.AppSettings["NetworkCredentialUsername"].ToString(
                            CultureInfo.InvariantCulture),
                        ConfigurationManager.AppSettings["NetworkCredentialPassword"].ToString(
                            CultureInfo.InvariantCulture)),
                Url = Properties.Settings.Default.ServiceURL
            };

            rs.Timeout = Int32.Parse(ConfigurationManager.AppSettings["ReportTimeoutInMillis"]);

            //Render arguments
            const string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            var execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;
            var execInfo = rs.LoadReport(reportPath, null);

            // Run the report extractor for every valid hospital
            foreach (
                ValidValue sourceId in
                    execInfo.Parameters.Where(i => i.Name == hospitalListName).Select(i => i.ValidValues).First()
                        .Where(i => !excludedHospitalsList.Contains(i.Value)))
            {
                Log.Info(String.Format("Starting report execution process: {0}-{1}-{2}", reportPath, sourceId.Value,
                    rs.ExecutionHeaderValue.ExecutionID));

                //Prepare report parameter.
                var parameters = new ParameterValue[1];
                parameters[0] = new ParameterValue {Name = hospitalListName, Value = sourceId.Value};
                rs.SetExecutionParameters(parameters, "en-us");

                string encoding;
                string mimeType;
                string extension;
                Warning[] warnings = null;
                string[] streamIDs = null;

                var result = rs.Render(exportFormat, devInfo, out extension, out encoding, out mimeType, out warnings,
                    out streamIDs);

                // Write the contents of the report to an Excel file.
                Log.Info(String.Format("Starting create file process: {0}-{1}-{2}-{3}", reportPath, sourceId.Value,
                    rs.ExecutionHeaderValue.ExecutionID, fileName));

                var id = sourceId;

                var sourceCode = (from hospital in hospitalMapping.AsEnumerable()
                    where hospital.Field<string>("SourceID") == id.Value
                    select hospital.Field<string>("SourceCode")).First();

                var id1 = sourceId;
                var hospitalSystem = (hospitalMapping.AsEnumerable()
                    .Where(hospital => hospital.Field<string>("SourceID") == id1.Value)
                    .Select(hospital => hospital.Field<string>("hospitalSystem"))).First();

                string[] fromArray =
                    execInfo.Parameters.Where(i => i.Name.ToLower() == "from").Select(i => i.DefaultValues).First();
                string from = fromArray[0];
                DateTime fromTime = Convert.ToDateTime(from);

                string[] toArray = execInfo.Parameters.Where(i => i.Name.ToLower() == "to").Select(i => i.DefaultValues).First();
                string to = toArray[0];
                DateTime toTime = Convert.ToDateTime(to);

                try
                {
                    var stream =
                        File.Create(String.Format(fileName,
                            hospitalSystem,
                            sourceId.Value + " " + sourceCode,
                            fromTime.ToString("yy-MM"),
                            toTime.ToString("yy-MM"),
                            DateTime.Now.ToString("yyyy-MM-dd"),
                            sourceId.Value,
                            sourceCode),
                            result.Length);

                    stream.Write(result, 0, result.Length);
                    stream.Close();
                }
                catch (DirectoryNotFoundException ex)
                {
                }
            }
        }
    }
}