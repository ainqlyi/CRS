using CRSGeoCoder.GeoCodeService;
using CRSGeoCoder.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CRSGeoCoder
{
	public class GeoCodeClient
	{
		//Initialize logger
		private static readonly ILog log = LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private USA_GeocodeServer proxy;
		
		public GeoCodeClient()
		{
			proxy = GetWebServiceObject();
		}

		private USA_GeocodeServer GetWebServiceObject()
		{
			USA_GeocodeServer webService;

			try
			{
				webService = new USA_GeocodeServer();
				webService.Timeout = Properties.Settings.Default.WS_Timeout;
			}
			catch (Exception ex)
			{
				log.Error(ex.StackTrace);
				webService = null;
				throw ex;
			}
			return webService;
		}


		public List<AddressModel> ProcessAddresses(List<AddressModel> addresses)
		{		
			#region Property Set
			PropertySet geocodePropSet = new PropertySet();
			PropertySetProperty[] propArray = new PropertySetProperty[2];

			PropertySetProperty geocodePropAddr = new PropertySetProperty();
			geocodePropAddr.Key = "Address";
			geocodePropAddr.Value = "Address";
			propArray[0] = geocodePropAddr;


			PropertySetProperty geocodePropPostal = new PropertySetProperty();
			geocodePropPostal.Key = "Postal";
			geocodePropPostal.Value = "Postal";
			propArray[1] = geocodePropPostal;
			
			geocodePropSet.PropertyArray = propArray;
			#endregion

			#region Input Record Set
			// Create a new recordset to store input addresses to be batch geocoded
			RecordSet addressTable = new RecordSet();
			Field[] fieldarray = new Field[5];

			// Following field properties are required for batch geocode to work:
			// Length, Name, Type.  There also needs to be a field of type OID.

			Field field0 = new Field();
			field0.Name = "OID";
			field0.Type = esriFieldType.esriFieldTypeOID;
			field0.Length = 50;
			fieldarray[0] = field0;

			Field field1 = new Field();
			field1.Name = "Address";
			field1.Type = esriFieldType.esriFieldTypeString;
			field1.Length = 50;
			fieldarray[1] = field1;

			Field field2 = new Field();
			field2.Name = "City";
			field2.Type = esriFieldType.esriFieldTypeString;
			field2.Length = 50;
			fieldarray[2] = field2;

			Field field3 = new Field();
			field3.Name = "State";
			field3.Type = esriFieldType.esriFieldTypeString;
			field3.Length = 50;
			fieldarray[3] = field3;

			Field field4 = new Field();
			field4.Name = "Postal";
			field4.Type = esriFieldType.esriFieldTypeString;
			field4.Length = 50;
			fieldarray[4] = field4;

			Fields fields = new Fields();
			fields.FieldArray = fieldarray;
			addressTable.Fields = fields;
			#endregion

			List<Record> records = new List<Record>();
			foreach (AddressModel address in addresses)
			{
				Record record = new Record();
				record.Values = new object[5] { address.ID, address.Address, address.City, address.State, address.Zip };
				records.Add(record);
			}

			// convert to record set for input to geocoder
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			addressTable.Records = records.ToArray();
			RecordSet results = proxy.GeocodeAddresses(addressTable, geocodePropSet, null);

			List<AddressModel> geoCodedAddresses = new List<AddressModel>();
				
			foreach(Record record in results.Records)
			{
				string[] address = record.Values[6].ToString().Split(',');
				geoCodedAddresses.Add(new AddressModel()
				{
					ID = (int)record.Values[1],
					Address = address[0].Trim(),
					Zip = record.Values[18].ToString(),
					Longitude = record.Values[24].ToString(),
					Latitude = record.Values[25].ToString(),
					City = record.Values[15].ToString(),
					State = record.Values[17].ToString()
				});
			}
			stopWatch.Stop();

			// Get the elapsed time as a TimeSpan value.
			TimeSpan ts = stopWatch.Elapsed;

			// Format and display the TimeSpan value. 
			string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
				ts.Hours, ts.Minutes, ts.Seconds,
				ts.Milliseconds / 10);

			Console.WriteLine("RunTime " + elapsedTime);
			proxy.Dispose();

			return geoCodedAddresses;
		}
	}
}
