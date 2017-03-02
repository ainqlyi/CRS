using System.Configuration;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Linq;

namespace CRS.Portal_FileMoverAndArchiver
{
    /// <summary>
    /// Contains properties and methods related to hospitals for the CRS Portal File Mover application
    /// </summary>
    public class Hospital
    {
        public static string SourceId;
        public static string SourceCode;
        public static string System;
        public static DataTable ExcludedHospitals = Hospital.GetDataTableFromCsvFile(ConfigurationManager.AppSettings["excludedHospitals"]);
        public static DataTable HospitalOutputFormat =
            GetDataTableFromCsvFile(ConfigurationManager.AppSettings["HospitalOutputFormat"]);
        public static string[] ExcludedHospitalsList = ExcludedHospitals.AsEnumerable().Select(row => row.Field<string>("sourceID")).ToArray();

        /// <summary>
        /// This method uses a CSV file to construct a datatable with the sourceId, sourceCode, and hospitalSystem
        /// for each desired hospital.
        /// </summary>
        /// <param name="csvFilePath">The location to search for the file</param>
        /// <returns>a datatable with the contents of the CSV file</returns>
        public static DataTable GetDataTableFromCsvFile(string csvFilePath)
        {
            var csvData = new DataTable();
            using (var csvReader = new TextFieldParser(csvFilePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
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
                        if ((string)fieldData[i] == "")
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
        /// Gets and updates the SourceCode value for the current hospital using the HospitalOutputFormat.csv file
        /// </summary>
        /// <param name="sourceId">Uses the provided sourceId to find the respective SourceCode</param>
        public static void SetSourceCode(string sourceId)
        {
            SourceCode = (from hospital in HospitalOutputFormat.AsEnumerable()
                          where hospital.Field<string>("SourceID") == sourceId
                          select hospital.Field<string>("SourceCode")).First();
        }

        /// <summary>
        /// Gets and updates the System value for the current hospital using the HospitalOutputFormat.csv file
        /// </summary>
        /// <param name="sourceId">Uses the provided sourceId to find the respective System</param>
        public static void SetHospitalSystem(string sourceId)
        {
            System = (HospitalOutputFormat.AsEnumerable()
            .Where(hospital => hospital.Field<string>("SourceID") == sourceId)
            .Select(hospital => hospital.Field<string>("hospitalSystem"))).First();
        }
    }
}