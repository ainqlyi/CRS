using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace RestfulGeoCoder
{
    /// <summary>
    /// The application form
    /// </summary>
    public partial class GeoCoderForm : Form
    {
        private readonly GeoCodeAddresses _geocodeAddresses = new GeoCodeAddresses();
        public readonly BackgroundWorker Bw = new BackgroundWorker();
        private readonly GeoDatabaser _geoDatabaser = new GeoDatabaser();
        private readonly Stopwatch _stopwatch = new Stopwatch();      

        public GeoCoderForm()
        {
            _geoDatabaser.DeleteOverlappingEiDsFromDestinationTable();
            InitializeComponent();
            _stopwatch.Start();
            DisplayInformation();
            Bw.WorkerReportsProgress = true;
            Bw.WorkerSupportsCancellation = true;
            Bw.DoWork += bw_DoWork;
            Bw.ProgressChanged += bw_ProgressChanged;
            Bw.RunWorkerCompleted += bw_RunWorkerCompleted;
        }

        /// <summary>
        /// Displays text for number of addresses, batches, and addresses per batch.
        /// </summary>
        public void DisplayInformation()
        {
            numAddressesTextBox.Text = String.Format("{0:#,0}", _geocodeAddresses.GetNumAddressesToProcess());
            numberOfBatchesBox.Text = String.Format("{0:#,0}", _geocodeAddresses.GetNumBatchesToProcess());
            numAddressesPerBatchTextBox.Text = String.Format("{0:#,0}", _geocodeAddresses.GetNumAddressesPerBatch());
        }

        /// <summary>
        /// Updates textboxes providing runtime information to the user, including what's left to process.
        /// </summary>
        public void UpdateRuntimeInfo()
        {
            float numAddressesLeftToProcess = _geocodeAddresses.GetNumAddressesToProcess();
            addressesProcessedTextBox.Text = (Convert.ToInt32(numAddressesTextBox.Text) -
                                            numAddressesLeftToProcess).ToString(CultureInfo.InvariantCulture);

            float numBatchesLeftToProcess = _geocodeAddresses.GetNumBatchesToProcess();
            batchesProcessedTextBox.Text = (Convert.ToInt32(numberOfBatchesBox.Text) -
                                            numBatchesLeftToProcess).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Provides information regarding use of the geocoding application.
        /// </summary>
        /// <param name="sender">The object causing the event</param>
        /// <param name="e">Any arguments</param>
        private void helpButton_Click(object sender, EventArgs e)
        {
            string message = String.Format("This program geocodes latitude and longitude to the {0} table.", 
                ConfigurationManager.AppSettings["destinationTableName"].ToString(CultureInfo.InvariantCulture));
            MessageBox.Show(message, "Help", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Gets the number of addresses per batch from the form's textbox.
        /// </summary>
        /// <returns>the number of addresses per batch</returns>
        public int GetNumAddressesPerBatch()
        {
            numAddressesTextBox.Refresh();
            return Convert.ToInt16(ConfigurationManager.AppSettings["numAddressesInBatch"]);
        }

        /// <summary>
        /// Operations to perform when loading the geocoder form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeoCoderForm_Load(object sender, EventArgs e)
        {
            Bw.RunWorkerAsync();
        }

        #region background operations

        /// <summary>
        /// Background worker that handles completed actions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.progressBar.Text = ("Canceled!");
            }

            else if (e.Error != null)
            {
                progressBar.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                progressBar.Text = "Done!";
            }
        }

        /// <summary>
        /// Background worker that handles changed progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Text = (e.ProgressPercentage.ToString(CultureInfo.InvariantCulture) + "%");
            progressBar.Value = e.ProgressPercentage;
            batchesProcessedTextBox.Text = String.Format("{0:#,0}", GeoCodeAddresses.BatchesProcessed);
            addressesProcessedTextBox.Text = String.Format("{0:#,0}", GeoCodeAddresses.AddressesProcessed);
            timeElapsedTextBox.Text = _stopwatch.Elapsed.ToString();
            addressesPerSecondTextBox.Text = Convert.ToString(Math.Round(Convert.ToDouble(GeoCodeAddresses.AddressesProcessed) / Convert.ToDouble(_stopwatch.Elapsed.TotalSeconds)));
            if (GeoCodeAddresses.AddressesProcessed != 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(GeoCodeAddresses.NumAddresses) / (Convert.ToDouble(GeoCodeAddresses.AddressesProcessed) / Convert.ToDouble(_stopwatch.Elapsed.TotalSeconds)));
                timeRemainingTextBox.Text = String.Format("{0:#,0}", t.TotalMinutes);
            }
            addressesRemainingTextBox.Text = String.Format("{0:#,0}", (GeoCodeAddresses.NumAddresses - GeoCodeAddresses.AddressesProcessed));
            batchesRemainingTextBox.Text = String.Format("{0:#,0}", GeoCodeAddresses.NumBatches - GeoCodeAddresses.BatchesProcessed);
        }

        /// <summary>
        /// Background worker that updates the interface and runs the geocoding process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var worker = sender as BackgroundWorker)
            {
                for (GeoCodeAddresses.BatchesProcessed = 0; GeoCodeAddresses.BatchesProcessed <= GeoCodeAddresses.NumBatches; GeoCodeAddresses.BatchesProcessed++)
                {
                    GeoCodeAddresses.GeocodeAllAddresses();
                    if (GeoCodeAddresses.NumBatches > 0)
                    {
                        int progress = Convert.ToInt16((Convert.ToDouble(GeoCodeAddresses.BatchesProcessed) / Convert.ToDouble(GeoCodeAddresses.NumBatches)) * 1000);
                        if (worker != null) worker.ReportProgress(progress);
                    }
                    else if (GeoCodeAddresses.NumBatches == 0)
                    {
                        int progress = 0;
                        if (worker != null) worker.ReportProgress(progress);
                    }
                    
                    GeoCodeAddresses.Log.Info(String.Format("Next batch: {0} time elapsed: {1} " +
                                           "Addresses processed: {2}", GeoCodeAddresses.BatchesProcessed, _stopwatch.Elapsed,
                        GeoCodeAddresses.AddressesProcessed));
                }
                _stopwatch.Stop();

                //MessageBox.Show(String.Format("All addresses geocoded successfully in {0}!", _stopwatch.Elapsed),
                //    "Geocoding Results", MessageBoxButtons.OK);
                    Thread.Sleep(1000);
                Application.Exit();
            }
        }

        #endregion
    }
}