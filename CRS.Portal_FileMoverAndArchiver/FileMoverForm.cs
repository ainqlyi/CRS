using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using log4net;

namespace CRS.Portal_FileMoverAndArchiver
{
    public partial class MainForm : Form
    {
        public static readonly ILog Log = LogManager.GetLogger(
            MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BackgroundWorker _bwDistributeFiles = new BackgroundWorker();
        private readonly BackgroundWorker _bwArchiveFiles = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            File.ReportConfiguration.Load(ConfigurationManager.AppSettings["ReportConfigurationFile"]);
            _bwDistributeFiles.WorkerReportsProgress = true;
            _bwArchiveFiles.WorkerReportsProgress = true;
            _bwDistributeFiles.WorkerSupportsCancellation = true;
            _bwArchiveFiles.WorkerSupportsCancellation = true;
            _bwDistributeFiles.DoWork += BwDistributeFilesDoWork;
            _bwArchiveFiles.DoWork += BwArchiveReportsDoWork;
            _bwDistributeFiles.ProgressChanged += BwDistributeFilesProgressChanged;
            _bwArchiveFiles.ProgressChanged += BwArchiveFilesProgressChanged;
            _bwDistributeFiles.RunWorkerCompleted += BwDistributeFilesRunWorkerCompleted;
            _bwArchiveFiles.RunWorkerCompleted += BwArchiveFilesRunWorkerCompleted;
        }

        /// <summary>
        /// Actions to take place upon loading of the UI form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_Load(object sender, EventArgs e)
        {
            File.NumFilesLeftToDistribute = File.GetTotalNumFilesToDistribute();
            File.NumReportsLeftToArchive = File.GetTotalNumReportsToArchive();

            if (File.NumFilesLeftToDistribute > 0)
            {
                Log.Info(String.Format("There are {0} files left to distribute.\n", File.NumFilesLeftToDistribute));
                _bwDistributeFiles.RunWorkerAsync();
            }

            if (File.NumReportsLeftToArchive > 0)
            {
                Log.Info(String.Format("There are {0} files left to archive\n", File.NumReportsLeftToArchive));
                _bwArchiveFiles.RunWorkerAsync();
            }

            DisplayReportsToDistribute();
            DisplayReportsToArchive();
            reportsToDistributeHeaderTextBox.AppendText(" " + File.GetTotalNumFilesToDistribute().ToString(CultureInfo.InvariantCulture));
            reportsToArchiveHeaderTextbox.AppendText(" " + File.GetTotalNumReportsToArchive().ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Performs asynchronous actions using the backgroundworker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BwDistributeFilesDoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                Thread.Sleep(1000);
                int distributionProgress = ((File.TotalNumFilesToDistribute - File.NumFilesLeftToDistribute) / File.TotalNumFilesToDistribute) * 100;
                _bwDistributeFiles.ReportProgress(distributionProgress);
            } while (File.NumFilesLeftToDistribute != 0);

            //Report 100% completion on operation completed
            _bwDistributeFiles.ReportProgress(100);
        }

        public void BwArchiveReportsDoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                Thread.Sleep(1000);
                int archivalProgress = ((File.TotalNumReportsToArchive - File.NumReportsLeftToArchive) / File.TotalNumReportsToArchive) *100;
                _bwArchiveFiles.ReportProgress(archivalProgress);
            } while (File.NumReportsLeftToArchive != 0);

            //Report 100% completion on operation completed
            _bwArchiveFiles.ReportProgress(100);
        }

        /// <summary>
        /// Updates progress using an asynchronous backgroundworker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BwDistributeFilesProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarDistributeFiles.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BwArchiveFilesProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarArchiveFiles.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Actions to perform upon completion of the asynchronous backgroundworker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BwDistributeFilesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BwArchiveFilesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /// <summary>
        /// Populates a textbox on the main form with reports that will be distributed
        /// </summary>
        public void DisplayReportsToDistribute()
        {
            foreach (string fileToDistribute in File.Files)
            {
                File.SetFileElementsForReport(File.Reports, fileToDistribute);
                if (!File.IsBaseFile && File.Name != null)
                {
                    Hospital.SourceId = File.GetSourceIdFromFileName(fileToDistribute);
                    Hospital.SetHospitalSystem(Hospital.SourceId);
                    Hospital.SetSourceCode(Hospital.SourceId);
                }

                if (File.IsBaseFile)
                {
                    if (!File.IsFileAReportNode(File.Reports, fileToDistribute)) continue;
                    reportsToDistributeTextBox.AppendText(Path.GetFileName(fileToDistribute));
                    reportsToDistributeTextBox.AppendText("\n");
                }
                else
                {
                    if (!File.IsFileAReportNode(File.Reports, fileToDistribute)) continue;
                    reportsToDistributeTextBox.AppendText(Path.GetFileName(fileToDistribute));
                    reportsToDistributeTextBox.AppendText("\n");
                }
            }
        }

        /// <summary>
        /// Populates a textbox on the main form with reports that will be archived
        /// </summary>
        public void DisplayReportsToArchive()
        {
            File.SetChildElementsForReportNode();
            foreach (var node in from XmlNode report in File.Reports
                let isArchivedNode = report.ChildNodes.Item(4)
                where isArchivedNode != null && isArchivedNode.InnerText == "TRUE"
                select report.ChildNodes.Item(0)
                into node
                where node != null
                select node)
            {
                reportsToArchiveTextBox.AppendText(node.InnerText);
                reportsToArchiveTextBox.AppendText("\n");
            }
        }

        /// <summary>
        /// Archives files per specification in configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void archiveFilesButton_Click(object sender, EventArgs e)
        {
            archiveFilesButton.Enabled = false;
            File.GetReportsForArchival();
            // Archive files as specified in ReportConfiguration.xml
            foreach (DataRow row in Hospital.HospitalOutputFormat.Rows)
            {
                if (!Hospital.ExcludedHospitalsList.Contains(row["SourceId"].ToString()))
                {
                    List<string> filesForArchival = File.GetFilesForArchivalFromHospitalDirectory(row);
                    foreach (string fileToArchive in filesForArchival)
                    {
                        File.ArchiveFile(fileToArchive);
                    }
                }
            }

            // Foreach hospital, archive base files as specified in FileConfiguration.xml
                foreach (DataRow row in Hospital.HospitalOutputFormat.Rows.Cast<DataRow>())
                {
                    if (!Hospital.ExcludedHospitalsList.Contains(row["SourceId"].ToString()))
                    {
                        File.ArchiveBaseFiles(row);
                    }
                }
            File.NumReportsLeftToArchive = 0;
        }

        /// <summary>
        /// Distributes files per specification in ReportConfiguration xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void distributeFilesButton_Click(object sender, EventArgs e)
        {
            //Change the status of the buttons on the UI accordingly 
            //The start button is disabled as soon as the background operation is started
            //The Cancel button is enabled so that the user can stop the operation 
            //at any point of time during the execution
            distributeFilesButton.Enabled = false;

            // Kickoff the worker thread to begin its DoWork function.
            //Bw_DistributeFiles.RunWorkerAsync();

            foreach (string filesToDistribute in File.Files)
            {
                if (File.IsFileAReportNode(File.Reports, filesToDistribute))
                {
                    File.SetFileElementsForReport(File.Reports, filesToDistribute);
                    if (!File.IsBaseFile)
                    {
                        Hospital.SourceId = File.GetSourceIdFromFileName(filesToDistribute);
                        Hospital.SetHospitalSystem(Hospital.SourceId);
                        Hospital.SetSourceCode(Hospital.SourceId);
                    }

                    if (File.IsBaseFile)
                    {
                        // Copy to all non-excluded hospital folders for path
                        foreach (DataRow row in Hospital.HospitalOutputFormat.Rows)
                        {
                            if (!Hospital.ExcludedHospitalsList.Contains(row["SourceId"]))
                            {
                                File.DistributeBaseFile(filesToDistribute, row);
                            }
                        }
                    }
                    else
                    {
                        //Copy to path for one hospital
                        File.DistributeHospitalFile(filesToDistribute);
                    }
                    File.MoveFileToDistributedFolder(filesToDistribute);
                    File.NumFilesLeftToDistribute--;
                    Log.Info(String.Format("There are now {0} files left to distribute", File.NumFilesLeftToDistribute));
                }
            }
        }
    }
}