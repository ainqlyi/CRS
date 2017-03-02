using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;
using log4net;
using RestfulGeoCoder.GISObjects.Request;
using RestfulGeoCoder.GISObjects.Response;

namespace RestfulGeoCoder
{
    /// <summary>
    /// Geocodes addresses
    /// </summary>
    public class GeoCodeAddresses
    {
        public static int NumAddresses;
        public static int NumBatches;
        public static int Offset = 0;
        public static int BatchesProcessed;
        public static int AddressesProcessed;
        public static string Token;
        public static GeoCoderForm Form = new GeoCoderForm();
        public static GeoCodeAddresses geoCodeAddresses = new GeoCodeAddresses();

        public static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Geocodes all addresses in Dimension.Location table
        /// </summary>
        public static void GeocodeAllAddresses()
        {
            GeoDatabaser geoDatabaser = new GeoDatabaser();
            RootResponseObject root = new RootResponseObject();
            List<Location> locations = new List<Location>();
            root.locations = locations;
            int locationCount = 0;

            // Get a new token every x batches based on appConfig key. 
            // Token expires every 60 minutes according to ArcGIS Server settings
            geoCodeAddresses.GetToken(BatchesProcessed);

            RootRequestObject rootRequestObject = new RootRequestObject();
            RootResponseObject rootResponseObject = new RootResponseObject();
            rootRequestObject = geoDatabaser.GetBatchOfNonGeocodedAddressesFromDatabase(rootRequestObject);

            if (rootRequestObject.records.Count != 0)
            {
                rootResponseObject = RestServices.GeocodeABatchOfAddresses(rootRequestObject, Token);

                if (rootResponseObject.locations != null)
                {
                    Log.Info(
                        String.Format("rootResponseObject.locations.count = {0}",
                            rootResponseObject.locations.Count));
                    AddressesProcessed += rootResponseObject.locations.Count;
                    foreach (Location location in rootResponseObject.locations)
                    {
                        Log.Debug(String.Format("Location Info: x: {0} y: {1} score: {2} address: {3}",
                            location.location.x, location.location.y, location.score, location.address));
                        if (!(location.location.x > 0 || location.location.x < 0) &&
                            !(location.location.y > 0 || location.location.y < 0))
                        {
                            location.location.x = null;
                            location.location.y = null;
                        }
                            root.locations.Add(new Location());
                            locations[locationCount] = location;
                            locationCount++;
                    }
                    geoDatabaser.InsertBatchOfGeocodedAddressesToDatabase(root);
                    root.locations.Clear();
                }
                else
                {
                    Log.Debug(
                        String.Format("rootResponseObject is null"));
                }
            }
        }

        /// <summary>
        /// Gets a new ArcGIS Token
        /// </summary>
        /// <returns>The token</returns>
        public string GetToken(int i)
        {
            if (i%Convert.ToInt16(ConfigurationManager.AppSettings["numBatchesPerTokenUpdate"]) == 0)
            {
                Token = RestServices.GetToken(
                    String.Format(
                        ConfigurationManager.AppSettings["ArcGISBaseAddress"].ToString(CultureInfo.InvariantCulture) +
                        "/arcgis/tokens/?request=gettoken&username={0}&password={1}",
                        ConfigurationManager.AppSettings["username"].ToString(CultureInfo.InvariantCulture),
                        ConfigurationManager.AppSettings["password"].ToString(CultureInfo.InvariantCulture)));
            }

            if (Token == null)
            {
                MessageBox.Show("Token could not be retrieved from server", "Server Error");
            }
            return Token;
        }

        /// <summary>
        /// Gets the number of addresses left to geocode
        /// </summary>
        /// <returns>the number of addresses left to geocode</returns>
        public int GetNumAddressesToProcess()
        {
            GeoDatabaser geoDatabaser = new GeoDatabaser();
            NumAddresses = geoDatabaser.GetNumAddressesLeftToProcess();
            return NumAddresses;
        }

        /// <summary>
        /// Gets the number of batches left to geocode
        /// </summary>
        /// <returns>the number of batches left to geocode</returns>
        public int GetNumBatchesToProcess()
        {
            NumBatches = NumAddresses/Convert.ToInt32(ConfigurationManager.AppSettings["numAddressesInBatch"]);
            return NumBatches;
        }

        /// <summary>
        /// Gets the number of addresses in a batch
        /// </summary>
        /// <returns>the number of addresses in a batch</returns>
        public int GetNumAddressesPerBatch()
        {
            int numAddressesPerBatch = Convert.ToInt32(ConfigurationManager.AppSettings["numAddressesInBatch"]);
            return numAddressesPerBatch;
        }
    }
}