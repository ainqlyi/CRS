using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace CRS.Portal_FileMoverAndArchiver
{
    /// <summary>
    /// Contains properties and methods associated with a file
    /// </summary>
    public class File
    {
        public static bool IsBaseFile { get; set; }
        public static string Name { get; set; }
        public static string Path { get; set; }
        public static string ArchivePath { get; set; }
        public static bool IsArchived { get; set; }
        private static string _distributedDirectory;
        public static int TotalNumFilesToDistribute;
        public static int TotalNumReportsToArchive;
        public static int NumFilesLeftToDistribute;
        public static int NumReportsLeftToArchive;
        public static readonly string[] Files = Directory.GetFiles(ConfigurationManager.AppSettings["Source_Path"]);
        public static readonly XmlDocument ReportConfiguration = new XmlDocument();
        public static readonly XmlNodeList Reports = ReportConfiguration.SelectNodes("/Reports/Report");
        private static readonly Dictionary<string, string> HospitalSpecificReportsAndArchivePath = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> BaseReportsAndArchivePath = new Dictionary<string, string>();

        /// <summary>
        /// Gets hospital specific and base reports for archival from the various hospital folders
        /// </summary>
        public static void GetReportsForArchival()
        {
            SetChildElementsForReportNode();
            foreach (XmlNode report in Reports)
            {
                // If isArchived == TRUE AND isBaseFile == FALSE, add report to list of hospital specific reports to archive
                if (report.ChildNodes.Item(4).InnerText == "TRUE" && report.ChildNodes.Item(2).InnerText == "FALSE")
                {
                    // Add report name and archive path
                    HospitalSpecificReportsAndArchivePath.Add(report.ChildNodes.Item(0).InnerText, report.ChildNodes.Item(1).InnerText);
                }
                    // If isArchived == TRUE and isBaseFile == TRUE, add report to list of base reports to archive
                else if (report.ChildNodes.Item(4).InnerText == "TRUE" && report.ChildNodes.Item(2).InnerText == "TRUE")
                {
                    BaseReportsAndArchivePath.Add(report.ChildNodes.Item(0).InnerText, report.ChildNodes.Item(1).InnerText);
                }
            }
        }
        
        /// <summary>
        /// Gets the sourceId from the filename
        /// </summary>
        /// <param name="fileName">The filename to check</param>
        /// <returns>the SourceId</returns>
        public static string GetSourceIdFromFileName(string fileName)
        {
            string sourceId = string.Empty;

            foreach (char t in fileName)
            {
                if (sourceId.Length == 0)
                {
                    if (Char.IsDigit(t) && Char.ToString(t) == "2")
                        sourceId += t;
                }
                else if (sourceId.Length > 0 && sourceId.Length < 6)
                {
                    if (!Char.IsDigit(t))
                    {
                        sourceId = string.Empty;
                    }
                    else if (Char.IsDigit(t))
                        sourceId += t;
                }
                else
                {
                    break;
                }
            }

            return sourceId;
        }

        /// <summary>
        /// Copies a base file to a new location
        /// </summary>
        /// <param name="file">The file to copy</param>
        /// <param name="row">Contains childElements for Report node in ReportConfiguration.xml</param>
        public static void DistributeBaseFile(string file, DataRow row)
        {
            if (
                System.IO.File.Exists(String.Format(File.Path + "{3}", Environment.Name, row["hospitalSystem"],
                    row["SourceId"] + " " + row["SourceCode"], System.IO.Path.GetFileName(file))))
            {
                System.IO.File.Delete(String.Format(File.Path + "{3}", Environment.Name, row["hospitalSystem"],
                    row["SourceId"] + " " + row["SourceCode"], System.IO.Path.GetFileName(file)));
            }

            System.IO.File.Copy(file.ToString(CultureInfo.InvariantCulture),
                String.Format(File.Path + "{3}", Environment.Name, row["hospitalSystem"],
                    row["SourceId"] + " " + row["SourceCode"], System.IO.Path.GetFileName(file)));

            MainForm.Log.Info(String.Format("Copied file {0} to {1}", file, String.Format(File.Path + "{3}", Environment.Name, row["hospitalSystem"],
                    row["SourceId"] + " " + row["SourceCode"], System.IO.Path.GetFileName(file))));
        }

        /// <summary>
        /// Distributes a hospital file to folders in CRS Portal
        /// </summary>
        /// <param name="file">The file to distribute</param>
        public static void DistributeHospitalFile(string file)
        {
            if (
                System.IO.File.Exists(String.Format(File.Path + "{3}", Environment.Name, Hospital.System,
                    Hospital.SourceId + " " + Hospital.SourceCode, System.IO.Path.GetFileName(file))))
            {
                System.IO.File.Delete(String.Format(File.Path + "{3}", Environment.Name, Hospital.System,
                    Hospital.SourceId + " " + Hospital.SourceCode, System.IO.Path.GetFileName(file)));
            }
            System.IO.File.Copy(file.ToString(CultureInfo.InvariantCulture),
                String.Format(File.Path + "{3}", Environment.Name, Hospital.System,
                    Hospital.SourceId + " " + Hospital.SourceCode, System.IO.Path.GetFileName(file)));

            MainForm.Log.Info(String.Format("Copied file {0} to {1}", file, String.Format(Path + "{3}", Environment.Name, Hospital.System,
                    Hospital.SourceId + " " + Hospital.SourceCode, System.IO.Path.GetFileName(file))));
        }

        /// <summary>
        /// Archives a files into its archive folder
        /// </summary>
        /// <param name="file">The file to archive</param>
        /// <param name="path">The path of where the file should go</param>
        public static void ArchiveFile(string file)
        {
            if (System.IO.File.Exists(System.IO.Path.GetDirectoryName(file) + "\\Archive\\" + System.IO.Path.GetFileName(file)))
            {
                System.IO.File.Delete(System.IO.Path.GetDirectoryName(file) + "\\Archive\\" + System.IO.Path.GetFileName(file));
            }
            System.IO.File.Move(file,
                String.Format(System.IO.Path.GetDirectoryName(file) + "\\Archive\\" + System.IO.Path.GetFileName(file)));

            MainForm.Log.Info(String.Format("Moved file {0} to {1}", file, String.Format(System.IO.Path.GetDirectoryName(file) + "\\Archive\\" + System.IO.Path.GetFileName(file))));
        }

        /// <summary>
        /// Gets hospital specific files for archival from each directory based on the settings in the ReportConfiguration.xml file
        /// </summary>
        /// <returns>List of files for archival</returns>
        public static List<string> GetFilesForArchivalFromHospitalDirectory(DataRow hospitalRow)
        {
            List<string> filesForArchival = new List<string>();

            // If any exist, get base files for archival
            if (BaseReportsAndArchivePath.Count > 0)
            {
                foreach (KeyValuePair<string, string> reportName in BaseReportsAndArchivePath)
                {
                    string directoryLocation = String.Format(reportName.Value, Environment.Name, 
                        hospitalRow["hospitalSystem"], hospitalRow["sourceId"] + " " + hospitalRow["sourceCode"]);
                    foreach (string file in Directory.GetFiles(directoryLocation))
                    {
                        if (file.Contains(reportName.Key.ToString(CultureInfo.InvariantCulture)))
                        {
                            filesForArchival.Add(file);
                        }
                    }
                }
            }

            // If any exist, get hospital specific files for archival
            if (HospitalSpecificReportsAndArchivePath.Count > 0)
            {
                foreach (KeyValuePair<string, string> reportName in HospitalSpecificReportsAndArchivePath)
                {
                    string directoryLocation = String.Format(reportName.Value, Environment.Name,
                        hospitalRow["hospitalSystem"], hospitalRow["sourceId"] + " " + hospitalRow["sourceCode"]);
                    foreach (string file in Directory.GetFiles(directoryLocation))
                    {
                        //Hospital.System = hospitalRow["hospitalSystem"].ToString();
                        //Hospital.SourceCode = hospitalRow["sourceCode"].ToString();
                        //Hospital.SourceId = hospitalRow["sourceId"].ToString();
                        if (file.Contains(reportName.Key.ToString(CultureInfo.InvariantCulture)))
                        {
                            filesForArchival.Add(file);
                        }
                    }
                }
            }
           
            return filesForArchival;
        }

        /// <summary>
        /// Gets base files for archival from the hospital folder
        /// </summary>
        /// <param name="hospitalRow">The hospital to check</param>
        /// <returns>List of base files for archival</returns>
        public static void ArchiveBaseFiles(DataRow hospitalRow)
        {
            foreach (KeyValuePair<string, string> reportName in BaseReportsAndArchivePath)
            {
                foreach (string file in Directory.GetFiles(String.Format(reportName.Value.ToString(CultureInfo.InvariantCulture), 
                    Environment.Name, hospitalRow["hospitalSystem"].ToString(), hospitalRow["SourceId"].ToString() + " " + hospitalRow["SourceCode"].ToString())))
                {
                    Hospital.System = hospitalRow["hospitalSystem"].ToString();
                    Hospital.SourceCode = hospitalRow["sourceCode"].ToString();
                    Hospital.SourceId = hospitalRow["sourceId"].ToString();
                    if (file.Contains(reportName.Key.ToString(CultureInfo.InvariantCulture)))
                    {
                        ArchiveFile(file);
                    }
                }
            }
        }

        /// <summary>
        /// Sets child elements for the report node in the ReportConfiguration.xml
        /// </summary>
        public static void SetChildElementsForReportNode()
        {
            foreach (XmlNode report in Reports)
            {
                var xmlElementName = report.ChildNodes.Item(0).InnerText;
                var xmlElementPath = report.ChildNodes.Item(1).InnerText;
                var xmlElementBase = report.ChildNodes.Item(2).InnerText;
                var xmlElementArchivePath = report.ChildNodes.Item(3).InnerText;
                var xmlElementIsArchived = report.ChildNodes.Item(4).InnerText;

                if (xmlElementBase.ToUpper() == "TRUE")
                {
                    IsBaseFile = true;
                }
                else
                {
                    IsBaseFile = false;
                }

                if (xmlElementIsArchived.ToUpper() == "TRUE")
                {
                    IsArchived = true;
                }
                else
                {
                    IsArchived = false;
                }

                if (xmlElementName != null) Name = xmlElementName;
                if (xmlElementPath != null) Path = xmlElementPath;
                if (xmlElementArchivePath != null) ArchivePath = xmlElementArchivePath;
            }
        }

        /// <summary>
        /// Finds xml nodes in ReportConfiguration.xml file and updates variables with childNode values
        /// </summary>
        /// <param name="reports">The nodes to search from ReportConfiguration.xml</param>
        /// <param name="filename">The filename to use for searching for the appropriate report node</param>
        /// <returns>void</returns>
        public static void SetFileElementsForReport(XmlNodeList reports, string filename)
        {
            int counter = 0;
            foreach (XmlNode report in reports)
            {
                if (filename.ToString(CultureInfo.InvariantCulture).Contains(report.ChildNodes.Item(0).InnerText))
                {
                    var xmlElementName = report.ChildNodes.Item(0).InnerText;
                    var xmlElementPath = report.ChildNodes.Item(1).InnerText;
                    var xmlElementBase = report.ChildNodes.Item(2).InnerText;
                    var xmlElementArchivePath = report.ChildNodes.Item(3).InnerText;
                    var xmlElementIsArchived = report.ChildNodes.Item(4).InnerText;

                    if (xmlElementBase.ToUpper() == "TRUE")
                    {
                        IsBaseFile = true;
                    }
                    else
                    {
                        IsBaseFile = false;
                    }

                    if (xmlElementIsArchived.ToUpper() == "TRUE")
                    {
                        IsArchived = true;
                    }
                    else
                    {
                        IsArchived = false;
                    }

                    if (xmlElementName != null) Name = xmlElementName;
                    if (xmlElementPath != null) Path = xmlElementPath;
                    if (xmlElementArchivePath != null) ArchivePath = xmlElementArchivePath;

                    counter++;
                    if (counter > 1)
                    {
                        throw new Exception(
                            "More than one report matches the file name. Verify that file name and report name match");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the total number of files to distribute
        /// </summary>
        /// <returns>An integer with the total number of files to distribute</returns>
        public static int GetTotalNumFilesToDistribute()
        {
            int totalNumFilesToDistribute = 0;
            foreach (string file in File.Files)
            {
                if (IsFileAReportNode(File.Reports, file))
                {
                    totalNumFilesToDistribute++;
                }
            }
            TotalNumFilesToDistribute = totalNumFilesToDistribute;
            return totalNumFilesToDistribute;
        }

        /// <summary>
        /// Returns the total number of reports to archive using the ReportConfiguration.xml with 'isArchived' child node
        /// </summary>
        /// <returns>integer number of total reports to archive</returns>
        public static int GetTotalNumReportsToArchive()
        {
            SetChildElementsForReportNode();
            TotalNumReportsToArchive = (from XmlNode report in Reports
                                        select report.ChildNodes.Item(4)).Count(xmlNode => xmlNode != null && xmlNode.InnerText == "TRUE");
            return TotalNumReportsToArchive;
        }

        /// <summary>
        /// Determines if a given file is one of the reports listed in the ReportConfiguration.xml file
        /// </summary>
        /// <param name="reports">The report nodes to check against</param>
        /// <param name="filename">The file to check to see if it is a report node</param>
        /// <returns>True if file is a report in the node list; false otherwise</returns>
        public static bool IsFileAReportNode (XmlNodeList reports, string filename)
        {
            foreach (XmlNode report in reports)
            {
                var xmlNode = report.ChildNodes.Item(0);
                if (xmlNode != null && filename.ToString(CultureInfo.InvariantCulture).Contains(xmlNode.InnerText))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Moves the given file to the 'Distributed files' folder
        /// </summary>
        /// <param name="filename">The file to move</param>
        public static void MoveFileToDistributedFolder(string filename)
        {
            if (
               System.IO.File.Exists(filename))
            {
                if (_distributedDirectory == null)
                {
                    _distributedDirectory = System.IO.Path.GetDirectoryName(filename) + "\\Distributed Files" + "\\" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                    Directory.CreateDirectory(_distributedDirectory);
                }
                System.IO.File.Move(filename, _distributedDirectory + "\\" + System.IO.Path.GetFileName(filename));
            }
        }
    }
}