using CRSGeoCoder.Model;
using FastMember;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRSGeoCoder
{
	class Program
	{
		//Initialize logger
		private static readonly ILog log = LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		static void Main(string[] args)
		{
			try
			{
				// convert to record set for input to geocoder
				Stopwatch stopWatch = new Stopwatch();
				stopWatch.Start();

				// Argument declarations
				string connectionString = args[0];
				string queryString = args[1];
				string countQuery = args[2];
				string destinationTable = args[3];
				int concurrent_thread = Int32.Parse(args[4]);
				int max_rows = Int32.Parse(args[5]);

				ProcessAddresses addresses = new ProcessAddresses(connectionString, queryString, countQuery, concurrent_thread, max_rows);

				List<AddressModel> geoCodedAddress = new List<AddressModel>();
				geoCodedAddress = addresses.GetGeoCodedAddresses();

				//Stop timers
				stopWatch.Stop();
				// Get the elapsed time as a TimeSpan value.
				TimeSpan ts = stopWatch.Elapsed;
				// Format and display the TimeSpan value. 
				string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
					ts.Hours, ts.Minutes, ts.Seconds,
					ts.Milliseconds / 10);
				log.Info("Total Time since launch " + elapsedTime);

				InsertAddresses(geoCodedAddress, connectionString, destinationTable);
			}
			catch (Exception ex)
			{
				log.Error("Exception", ex);
			}

		}
		static void InsertAddresses(List<AddressModel> geoCodedAddress, string connectionString, string destinationTable)
		{
			DataTable addressTable = new DataTable();
			using (var reader = ObjectReader.Create(geoCodedAddress, "ID", "Address", "City", "State", "Zip", "Longitude", "Latitude"))
			{
				addressTable.Load(reader);
			}

			using (SqlConnection destinationConnection = new SqlConnection(connectionString))
			{
				destinationConnection.Open();
				using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
				{
					bulkCopy.BulkCopyTimeout = Properties.Settings.Default.DB_Timeout;
					bulkCopy.DestinationTableName = destinationTable;
					try
					{
						// Write from the source to the destination.
						bulkCopy.WriteToServer(addressTable);
					}
					catch (Exception ex)
					{
						log.Error("Exception Bulk Copy", ex);
					}
				}
			}
		}
	}
}
