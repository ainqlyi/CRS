using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Threading;
using OpenQA.Selenium;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace CRS.AutomatedUiTests.Utility
{
    /// <summary>
    /// Contains helpful methods for use in the automated UI tests
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Returns true or false depending on whether an element is present on the current webpage
        /// </summary>
        /// <param name="element">The element to look for</param>
        /// <returns>true or false</returns>
        public static Boolean IsElementPresent(By element)
        {
            try
            {
                WebBrowser.Current.FindElement(element);
            }
            catch (NoSuchElementException e)
            {
                try
                {
                    Thread.Sleep(3000);
                    WebBrowser.Current.FindElement(element);
                }
                catch (NoSuchElementException f)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets an element using Selenium
        /// </summary>
        /// <param name="by">The method of finding the element</param>
        /// <returns>The element</returns>
        public static IWebElement GetElement(By by)
        {
            try
            {
                IWebElement element = WebBrowser.Current.FindElement(by);
                return element;
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the parent element by using XPath
        /// </summary>
        /// <param name="e">The element to use for finding its parent</param>
        /// <returns>Parent element</returns>
        public static IWebElement GetParent(IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }

        /// <summary>
        /// Get a DataTable from an Excel file
        /// </summary>
        /// <param name="filePath">The file to convert to DataTable</param>
        /// <returns>DataTable</returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string hdr = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //Looping Total Sheet of Xl File
            /*foreach (DataRow schemaRow in schemaTable.Rows)
            {
            }*/
            //Looping a first Sheet of Xl File
            if (schemaTable != null)
            {
                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                }
            }

            conn.Close();
            return dtexcel;
        }

        /// <summary>
        /// This method uses a CSV file to construct a datatable 
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
    }
}