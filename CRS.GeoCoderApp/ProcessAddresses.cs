using CRSGeoCoder.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CRSGeoCoder
{
	class ProcessAddresses
	{
		// Argument declarations
		string connectionString;
		string queryString;
		string countQuery;
		
		int concurrent_thread;
		int max_rows;
		int max_loop;
		List<AddressModel> geoCodedAddresses = new List<AddressModel>();

		public ProcessAddresses(string connectionString, string queryString, string countQuery, int concurrent_thread, int max_rows)
		{
			this.connectionString = connectionString;
			this.queryString = queryString;
			this.countQuery = countQuery;
			this.concurrent_thread = concurrent_thread;
			this.max_rows = max_rows;
			
			this.max_loop = (int)Math.Ceiling(CountRows(connectionString, countQuery) / (double)max_rows);
		}
		public List<AddressModel> GetGeoCodedAddresses()
		{

			List<Task> tasks = new List<Task>();
			List<Action> actions = new List<Action>();

			for (int i = 0; i < max_loop; i++)
			{
				string formattedQuery = String.Format(queryString, max_rows * i, max_rows);
				actions.Add(() => GeoCodingThread(formattedQuery));
			}

			var options = new ParallelOptions();
			options.MaxDegreeOfParallelism = concurrent_thread;
			Parallel.Invoke(options, actions.ToArray());

			return geoCodedAddresses;
		}

		private void GeoCodingThread(string formattedQuery)
		{
			Console.WriteLine("Thread #: {0}", Thread.CurrentThread.ManagedThreadId);
			// convert to record set for input to geocoder
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();



			GeoCodeClient client = new GeoCodeClient();
			geoCodedAddresses.AddRange(client.ProcessAddresses(GetQueryResults(formattedQuery)));

			stopWatch.Stop();

			// Get the elapsed time as a TimeSpan value.
			TimeSpan ts = stopWatch.Elapsed;
			// Format and display the TimeSpan value. 
			string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
				ts.Hours, ts.Minutes, ts.Seconds,
				ts.Milliseconds / 10);

			Console.WriteLine("Thread #: {0}, Thread Time: {1}", Thread.CurrentThread.ManagedThreadId, elapsedTime);
		}

		private List<AddressModel> GetQueryResults(string formattedQuery)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// Create the Command and Parameter objects.
				SqlCommand command = new SqlCommand(formattedQuery, connection);
				command.CommandTimeout = Properties.Settings.Default.DB_Timeout;
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				List<AddressModel> addresses = new List<AddressModel>();

				while (reader.Read())
				{
					addresses.Add(new AddressModel()
					{
						ID = (int)reader[0],
						Address = reader[1].ToString(),
						City = reader[2].ToString(),
						State = reader[3].ToString(),
						Zip = reader[4].ToString()
					});
				}

				return addresses;
			}
		}

		private static int CountRows(string connectionString, string countQuery)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand();
				command.CommandTimeout = Properties.Settings.Default.DB_Timeout;
				command.Connection = connection;
				command.CommandType = CommandType.Text;
				command.CommandText = countQuery;

				return Convert.ToInt32(command.ExecuteScalar());
			}
		}
	}
}
