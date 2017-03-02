using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using log4net.Layout;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using TechTalk.SpecFlow;
using DataTable = System.Data.DataTable;

namespace CRS.AutomatedUiTests.Steps
{
    [Binding]
    public class DatabaseSteps
    {
        private int _numRecordsInDatabase;
        private int _numRecordsInFiles;
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        #region Given

        [Given(@"the total number of records is calculated for all files in a directory")]
        public void GivenTheTotalNumberOfRecordsIsCalculatedForAllFilesInADirectory()
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["fileSource"]);
            foreach (string file in files)
            {
                _numRecordsInFiles += GetNumberOfRowsFromExcelFile(file, Path.GetFileNameWithoutExtension(file));
            }
        }

        [Given(@"the total number of IP records is calculated for all files in a directory")]
        public void GivenTheTotalNumberOfIpRecordsIsCalculatedForAllFilesInADirectory()
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["fileSource"]);
            foreach (string file in files)
            {
                _numRecordsInFiles += GetNumberOfRowsFromExcelFile(file, Path.GetFileNameWithoutExtension(file), "DATATYPE = 'IP'");
            }
        }

        [Given(@"the total number of OP records is calculated for all files in a directory")]
        public void GivenTheTotalNumberOfOpRecordsIsCalculatedForAllFilesInADirectory()
        {
            string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["fileSource"]);
            foreach (string file in files)
            {
                _numRecordsInFiles += GetNumberOfRowsFromExcelFile(file, Path.GetFileNameWithoutExtension(file), "DATATYPE = 'OP'");
            }
        }

        [Given(@"the total number of records queried in the database for table '(.*)'")]
        public void GivenTheTotalNumberOfRecordsQueriedInTheDatabaseForTable(string table)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(String.Format("SELECT Count(*) FROM {0}", table), connection))
                {
                    _numRecordsInDatabase = (int) cmd.ExecuteScalar();
                }
            }
        }

        [Given(@"the total number of IP records queried in the database for table '(.*)'")]
        public void GivenTheTotalNumberOfIpRecordsQueriedInTheDatabaseForTable(string table)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(String.Format("SELECT Count(*) FROM {0} WHERE DATATYPE = 'IP'", table), connection))
                {
                    _numRecordsInDatabase = (int)cmd.ExecuteScalar();
                }
            }
        }

        [Given(@"the total number of OP records queried in the database for table '(.*)'")]
        public void GivenTheTotalNumberOfOpRecordsQueriedInTheDatabaseForTable(string table)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(String.Format("SELECT Count(*) FROM {0} WHERE DATATYPE = 'OP'", table), connection))
                {
                    _numRecordsInDatabase = (int)cmd.ExecuteScalar();
                }
            }
        }

        #endregion

        #region Then

        [Then(@"the number of records in the database matches the number in the files")]
        public void ThenTheNumberOfRecordsInTheDatabaseMatchesTheNumberInTheFiles()
        {
            Assert.IsTrue(_numRecordsInDatabase.Equals(_numRecordsInFiles));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the number of rows from an excel file
        /// </summary>
        /// <param name="filename">The file</param>
        /// <param name="sheetName">The sheet to count</param>
        /// <returns>Number of rows</returns>
        private int GetNumberOfRowsFromExcelFile(string filename, string sheetName)
        {
            string connectionString = null;
            int count = 0;

            if (filename.EndsWith(".xlsx"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Mode=ReadWrite;Extended Properties=\"Excel 12.0;HDR=NO\"";
            }
            else if (filename.EndsWith(".xls"))
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Mode=ReadWrite;Extended Properties=\"Excel 8.0;HDR=NO;\"";
            }

            string sql = "SELECT COUNT (*) FROM [" + sheetName + "$]";

            if (connectionString != null)
                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                reader.Read();
                                count = (int) reader[0];
                            }
                        }
                    }

                    conn.Close();
                }
            // Remove 1 from count to account for header
            return --count;
        }

        /// <summary>
        /// Gets the number of rows from an excel file
        /// </summary>
        /// <param name="filename">The file</param>
        /// <param name="sheetName">The sheet to count</param>
        /// <param name="predicateClause">The 'where' section of the SQL statement. Example: DATASET = 'IP'</param>
        /// <returns>Number of rows</returns>
        private int GetNumberOfRowsFromExcelFile(string filename, string sheetName, string predicateClause)
        {
            string connectionString = null;
            int count = 0;

            if (filename.EndsWith(".xlsx"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Mode=ReadWrite;Extended Properties=\"Excel 12.0;HDR=NO\"";
            }
            else if (filename.EndsWith(".xls"))
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Mode=ReadWrite;Extended Properties=\"Excel 8.0;HDR=NO;\"";
            }

            if (connectionString != null)
                using (var conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    DataTable dt = Utility.Utility.ExcelToDataTable(filename);
                    count = dt.Select(predicateClause).Length;
                    conn.Close();
                }
            return count;
        }
        #endregion
    }
}