using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RestfulGeoCoder.GISObjects.Request;
using RestfulGeoCoder.GISObjects.Response;
using RestfulGeocoder.HttpUtils;

namespace RestfulGeoCoder
{
    /// <summary>
    /// Contains operations necessary when interacting with the database
    /// </summary>
    public class GeoDatabaser
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        private static readonly HttpUtils HttpUtils = new HttpUtils();

        /// <summary>
        /// Performs a bulk insert into the database with the current batch of addresses
        /// </summary>
        /// <param name="rootResponseObject">The C# objectified response from ArcGIS</param>
        public void InsertBatchOfGeocodedAddressesToDatabase(RootResponseObject rootResponseObject)
        {
            GeoCodeAddresses.Log.Debug(String.Format("Insert batch of geocoded addresses to database: \n" +
                                                     "rootResponseObject.Count = {0}",
                rootResponseObject.locations.Count));
            DataTable geoCodedDataTableLocations =
                Utility.Utility.ConvertRootResponseToDataTable<RootResponseObject>(rootResponseObject);
            GeoCodeAddresses.Log.Debug(String.Format("Convert Root Response to DataTable: \n" +
                                                     "geoCodedDataTableLocations.Rows.Count = {0}\n" +
                                                     "geoCodedDataTableLocations.Columns.Count = {1}",
                geoCodedDataTableLocations.Rows.Count, geoCodedDataTableLocations.Columns.Count));
            var columnMappings = new List<string>
            {
                "EID,EID", "PointX,PointX", "PointY,PointY",
                "score,score",
                "address,address", "Loc_name,Loc_name", "Status,Status", "Match_Addr,Match_Addr", "Addr_Type,Addr_Type",
                "PlaceName,PlaceName", "Place_addr,Place_addr", "Rank,Rank", "AddBldg,AddBldg", "AddNum,AddNum",
                "AddNumFrom,AddNumFrom", "AddNumTo,AddNumTo", "Side,Side", "StPreDir,StPreDir", "StPreType,StPreType",
                "StName,StName", "StType,StType", "StDir,StDir", "StAddr,StAddr", "Nbrhd,Nbrhd", "City,City",
                "Subregion,Subregion", "Region,Region", "Postal,Postal", "PostalExt,PostalExt", "Country,Country", 
                "LangCode,LangCode", "Distance,Distance", "X,X", "Y,Y", "DisplayX,DisplayX", "DisplayY,DisplayY",
                "Xmin,Xmin", "Xmax,Xmax", "Ymin,Ymin", "Ymax,Ymax"
            };

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var bulkCopy = new SqlBulkCopy(connection)
                {
                    DestinationTableName =
                        ConfigurationManager.AppSettings["destinationTableName"].ToString(CultureInfo.InvariantCulture)
                };
                BatchBulkCopy(geoCodedDataTableLocations, bulkCopy.DestinationTableName,
                    GeoCodeAddresses.Form.GetNumAddressesPerBatch(), columnMappings);
            }
        }

        /// <summary>
        /// Gets a batch of geocoded addresses from the database and returns them as a RootObject.
        /// </summary>
        /// <param name="root">The object to put the addresses into</param>
        /// <returns>RootObject</returns>
        public RootRequestObject GetBatchOfNonGeocodedAddressesFromDatabase(RootRequestObject root)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (
                    SqlCommand command =
                        new SqlCommand(String.Format("SELECT L.EID, L.Street, L.City, L.State, L.ZipCode" +
                                                     " FROM " +
                                                     ConfigurationManager.AppSettings["sourceTableName"].ToString(
                                                         CultureInfo.InvariantCulture) + " L" +
                                                     " ORDER BY L.EID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY",
                            GeoCodeAddresses.Offset,
                            GeoCodeAddresses.Form.GetNumAddressesPerBatch()
                            ), connection))
                {
                    GeoCodeAddresses.Offset += GeoCodeAddresses.Form.GetNumAddressesPerBatch();
                    List<Record> records = new List<Record>();
                    connection.Open();
                    command.CommandTimeout = 90;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            records.Add(new Record());
                            records[i].attributes.OBJECTID = Convert.ToInt32(reader[0]);
                            records[i].attributes.Address = reader[1].ToString();
                            records[i].attributes.Address = HttpUtils.SanitizeUrlString(records[i].attributes.Address);
                            records[i].attributes.City = reader[2].ToString();
                            records[i].attributes.Region = reader[3].ToString();
                            records[i].attributes.Postal = reader[4].ToString();
                            if (records[i].attributes.Postal.Length > 5)
                            {
                                records[i].attributes.Postal = records[i].attributes.Postal.Substring(0, 5);
                            }
                            root.records.Add(records[i]);
                            i++;
                        }
                    }
                }
            }
            return root;
        }

        /// <summary>
        /// Deletes EIDs from the source table that exist in the destination table
        /// </summary>
        public void DeleteOverlappingEiDsFromDestinationTable()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (
                    SqlCommand command =
                        new SqlCommand(
                            String.Format("DELETE FROM " +
                                          ConfigurationManager.AppSettings["sourceTableName"].ToString(
                                              CultureInfo.InvariantCulture) +
                                          " FROM " +
                                          ConfigurationManager.AppSettings["sourceTableName"].ToString(
                                              CultureInfo.InvariantCulture) + " L" +
                                          " INNER JOIN " +
                                          ConfigurationManager.AppSettings["destinationTableName"].ToString(
                                              CultureInfo.InvariantCulture) +
                                          " G ON L.EID = G.EID"), connection))
                {
                    connection.Open();
                    command.CommandTimeout = 90;
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Joins the Dimension.Location table to the Dimension.GeoLocation table to get EIDs 
        /// from Dimension.Location that do not exist in Dimension.GeoLocation
        /// </summary>
        /// <returns>NumAddresses</returns>
        public int GetNumAddressesLeftToProcess()
        {
            int result;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT Count(L.EID)" +
                                                           " FROM " +
                                                           ConfigurationManager.AppSettings["sourceTableName"].ToString(
                                                               CultureInfo.InvariantCulture) + " L" +
                                                           " LEFT JOIN " +
                                                           ConfigurationManager.AppSettings["destinationTableName"]
                                                               .ToString(CultureInfo.InvariantCulture) +
                                                           " G ON L.EID = G.EID" +
                                                           " WHERE L.ZipCode IS NOT NULL AND L.ZipCode != '' and G.EID IS NULL"
                    , connection))
                {
                    connection.Open();
                    result = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return result;
        }

        /// <summary>
        /// Uses SqlBulkCopy to use a DataTable as source and SQL table as destination.
        /// </summary>
        /// <param name="dataTable">The source</param>
        /// <param name="destinationTbl">The destination</param>
        /// <param name="batchSize">Number of records in batch</param>
        /// <param name="columnMapping">The columns to map</param>
        public void BatchBulkCopy(DataTable dataTable, string destinationTbl, int batchSize, List<string> columnMapping)
        {
            GeoCodeAddresses.Log.Debug(
                String.Format("Batch Bulk Copy(DataTable {0}, destinationTbl {1}, batchSize {2}, columnMapping {3})",
                    dataTable, destinationTbl, batchSize, columnMapping));
            // Get the DataTable

            DataTable dtInsertRows = dataTable;

            using (SqlBulkCopy sbc = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.KeepIdentity))
            {
                sbc.BulkCopyTimeout = 360;
                sbc.DestinationTableName = destinationTbl;

                // Number of records to be processed in one go
                sbc.BatchSize = batchSize;

                // Add your column mappings here
                foreach (var mapping in columnMapping)
                {
                    var split = mapping.Split(new[] {','});
                    sbc.ColumnMappings.Add(split.First(), split.Last());
                }

                // Finally write to server
                try
                {
                    sbc.WriteToServer(dtInsertRows);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    Application.Exit();
                }   
            }
        }
    }
}