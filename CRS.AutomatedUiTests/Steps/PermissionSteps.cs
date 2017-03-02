using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using CRS.AutomatedUiTests.Utility;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using System.Data;
using Actions = OpenQA.Selenium.Interactions.Actions;

namespace CRS.AutomatedUiTests.Steps
{
    /// <summary>
    /// Contains the SpecFlow step methods relating to Permissions for the CRS Portal
    /// </summary>
    [Binding]
    public class PermissionSteps
    {
        private static readonly Dictionary<string, string> AllDistinctHospitalAndSystemFolders =
            new Dictionary<string, string>();

        private readonly Dictionary<string, string> _hospitalAndStatewideMapping = new Dictionary<string, string>();
        private readonly List<string> _userPermittedHospitalFolders = new List<string>();
        private readonly List<string> _userPermittedSystemFolders = new List<string>();
        private readonly List<string> _userNonPermittedHospitalFolders = new List<string>();
        private readonly List<string> _userNonPermittedSystemFolders = new List<string>();

        private readonly Dictionary<string, bool> _userPermittedHospitalAndCasemixMapping =
            new Dictionary<string, bool>();

        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public DataTable UserInfoTable =
            Utility.Utility.GetDataTableFromCsvFile(
                ConfigurationManager.AppSettings["userInfoPath"].ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Verifies that a user can see folders for his permission level
        /// </summary>
        /// <param name="username">The user to verify against</param>
        [Then(@"username '(.*)' can see the folders for his permissions")]
        public void ThenUsernameCanSeeTheFoldersForHisPermissions(string username)
        {
            PreparePermissionDictionariesAndLists(username);
            CheckEachPermittedSystemAndHospitalFolderForUser(username);
        }

        /// <summary>
        /// Verifies that a user cannot see folders outside his level of permission
        /// </summary>
        /// <param name="username">The user to verify against</param>
        [Then(@"username '(.*)' cannot see the folders outside his permission")]
        public void ThenUsernameCannotSeeTheFoldersOutsideHisPermission(string username)
        {
            UpdateListsOfUserNonPermittedHospitalAndSystemFolders();
            if (AllDistinctHospitalAndSystemFolders.Count < 1)
            {
                UpdateDictionaryOfDistinctHospitalAndSystemFolders();
            }

            string systemText = "";

            // User cannot see a system folder 
            foreach (string hospital in _userNonPermittedHospitalFolders)
            {
                systemText = AllDistinctHospitalAndSystemFolders[hospital];
                IWebElement currentSystemElement =
                    Utility.Utility.GetElement(By.XPath(String.Format("//div[contains(text(), '{0}')]", systemText)));

                string firstWordOfSystem = systemText.Substring(0,
                    systemText.IndexOf(" ", System.StringComparison.Ordinal));
                if (!_userPermittedSystemFolders.Contains(firstWordOfSystem))
                {
                    Assert.IsTrue(currentSystemElement == null);
                    Console.WriteLine("{0} cannot see folder for {1}", username, systemText);
                }
            }
        }

        /// <summary>
        /// A user can see casemix folders for his permissions
        /// </summary>
        /// <param name="username">The user to check</param>
        [Then(@"username '(.*)' can see casemix folders for his permissions")]
        public void ThenUsernameCanSeeCasemixFoldersForHisPermissions(string username)
        {
            PreparePermissionDictionariesAndLists(username);
            CheckEachPermittedSystemAndHospitalAndCasemixFolderForUser(username);
        }

        /// <summary>
        /// A user cannot see casemix folders outside his permission
        /// </summary>
        /// <param name="username">The user to check</param>
        [Then(@"username '(.*)' cannot see casemix folders outside his permission")]
        public void ThenUsernameCannotSeeCasemixFoldersOutsideHisPermission(string username)
        {
        }

        #region Helper Methods

        /// <summary>
        /// Verifies that user can see each system and hospital folder for his permissions
        /// </summary>
        /// <param name="username">The username to check for</param>
        private void CheckEachPermittedSystemAndHospitalFolderForUser(string username)
        {
            string systemText = "";

            // Check each permitted system and hospital folder for the user
            if (_userPermittedHospitalFolders != null)
                foreach (string hospital in _userPermittedHospitalFolders)
                {
                    // Check system element (if the hospital for this system is statewide) and double click on it if this is the first time we've checked for it. 
                    // Collapsing the system folder is needed to see the hospital folders
                    if (_hospitalAndStatewideMapping != null && (_hospitalAndStatewideMapping[hospital] == "1"))
                    {
                        // Update systemText for the current hospital
                        if (AllDistinctHospitalAndSystemFolders != null)
                        {
                            systemText = AllDistinctHospitalAndSystemFolders[hospital];
                            FindSystemAndDoubleClickOnIt(systemText, username, hospital);
                        }
                    }

                    // Verify that hospital is present if it is a statewide hospital
                    if (_hospitalAndStatewideMapping != null && _hospitalAndStatewideMapping[hospital] == "1")
                    {
                        Assert.IsTrue(
                            Utility.Utility.IsElementPresent(
                                By.XPath(String.Format("//div[contains(., '{0}')]", hospital))));
                        Console.WriteLine("{0} can see {1} with hospital {2}", username, systemText,
                            hospital);
                    }
                }
        }

        /// <summary>
        /// Verifies that user can see every system, hospital and casemix folder for his permissions
        /// Also verifies that if casemix = false in CSV file, user cannot see a casemix folder
        /// </summary>
        /// <param name="username">The user to check for</param>
        private void CheckEachPermittedSystemAndHospitalAndCasemixFolderForUser(string username)
        {
            string systemText = "";
            GetListOfUserPermittedCasemixFolders(username);

            // Check each permitted system and hospital folder for the user
            if (_userPermittedHospitalFolders != null)
                foreach (string hospital in _userPermittedHospitalFolders)
                {
                    // Check system element (if the hospital for this system is statewide) and double click on it if this is the first time we've checked for it. 
                    // Collapsing the system folder is needed to see the hospital folders
                    if (_hospitalAndStatewideMapping != null && (_hospitalAndStatewideMapping[hospital] == "1"))
                    {
                        // Update systemText for the current hospital
                        if (AllDistinctHospitalAndSystemFolders != null)
                        {
                            systemText = AllDistinctHospitalAndSystemFolders[hospital];
                            FindSystemAndDoubleClickOnIt(systemText, username, hospital);
                        }
                    }

                    // Verify that hospital is present if it is a statewide hospital
                    if (_hospitalAndStatewideMapping != null && _hospitalAndStatewideMapping[hospital] == "1")
                    {
                        Assert.IsTrue(
                            Utility.Utility.IsElementPresent(
                                By.XPath(String.Format("//div[contains(., '{0}')]", hospital))));
                        Console.WriteLine("{0} can see {1} with hospital {2}", username, systemText,
                            hospital);
                    }

                    // Verify that casemix folder is present if user has casemix permission
                    if (_userPermittedHospitalAndCasemixMapping != null)
                    {
                        if (_userPermittedHospitalAndCasemixMapping[hospital])
                        {
                            IWebElement hospitalElement =
                                Utility.Utility.GetElement(By.XPath(String.Format("//div[contains(., '{0}')]", hospital)));
                            Actions action = new Actions(WebBrowser.Current);
                            action.DoubleClick(hospitalElement);
                            action.Perform();
                            Assert.IsTrue(
                                Utility.Utility.IsElementPresent(
                                    By.XPath(String.Format("//div[contains(., '{0}')]", "Patient Level Detail"))));
                            Console.WriteLine(String.Format("{0} can see patient level detail for system {1} and hospital {2}", username, systemText, hospital));
                        }
                        else
                        {
                            IWebElement hospitalElement =
                                 Utility.Utility.GetElement(By.XPath(String.Format("//div[contains(., '{0}')]", hospital)));
                            Actions action = new Actions(WebBrowser.Current);
                            action.DoubleClick(hospitalElement);
                            action.Perform();
                            Assert.IsFalse(
                                Utility.Utility.IsElementPresent(
                                    By.XPath(String.Format("//div[contains(., '{0}')]", "Patient Level Detail"))));
                            Console.WriteLine(String.Format("{0} cannot see patient level detail for system {1} and hospital {2}", username, systemText, hospital));
                        }
                    }
                }
        }

        /// <summary>
        /// Find a system for a given user and hospital and double click on it
        /// </summary>
        /// <param name="systemText">The system to click on</param>
        /// <param name="username">The user that is performing the action</param>
        /// <param name="hospital">The hospital associated with the system</param>
        private void FindSystemAndDoubleClickOnIt(string systemText, string username, string hospital)
        {
            WebBrowser.Current.Navigate().GoToUrl("crs.crisphealth.org/reports");
            IWebElement currentSystemElement =
                Utility.Utility.GetElement(
                    By.XPath(String.Format("//div[contains(text(), '{0}')]",
                        systemText.Substring(0, systemText.IndexOf(" ", System.StringComparison.Ordinal)))));
            if (currentSystemElement == null)
            {
                throw new Exception(String.Format("Cannot find system element on page for {0}",
                    systemText));
            }
            try
            {
                currentSystemElement.Click();
            }
            catch (ElementNotVisibleException e)
            {
                throw new Exception(String.Format("Element is not visible by browser for {0} {1} {2}",
                    username, systemText, hospital));
            }
            Actions action = new Actions(WebBrowser.Current);
            action.DoubleClick(currentSystemElement);
            action.Perform();
        }

        /// <summary>
        /// Gets all necessary dictionaries and lists for permissions testing
        /// </summary>
        /// <param name="username"></param>
        private void PreparePermissionDictionariesAndLists(string username)
        {
            // Make sure all dictionaries and lists are populated for the user
            if (_hospitalAndStatewideMapping.Count < 1)
            {
                UpdateDictionaryOfHospitalAndStatewideMapping();
            }

            if (AllDistinctHospitalAndSystemFolders.Count < 1)
            {
                UpdateDictionaryOfDistinctHospitalAndSystemFolders();
            }

            if (_userPermittedHospitalFolders.Count < 1 || _userPermittedSystemFolders.Count < 1)
            {
                GetListOfUserPermittedHospitalAndSystemFolders(username);
            }
        }

        /// <summary>
        /// Gets a list of permitted casemix folders for the user
        /// </summary>
        private void GetListOfUserPermittedCasemixFolders(string username)
        {
            string getUserRow = "WebsiteUsername='" + username + "'";

            DataRow[] rows = UserInfoTable.Select(getUserRow);

            if (!rows.Any())
            {
                throw new Exception(String.Format("WebsiteUsername={0} does not exist in spreadsheet", username));
            }

            foreach (DataRow o in UserInfoTable.Select(getUserRow))
            {
                if (o["Casemix"].ToString().ToLower() == "yes")
                {
                    _userPermittedHospitalAndCasemixMapping.Add(o["Provider"].ToString(), true);
                }
                else
                {
                    _userPermittedHospitalAndCasemixMapping.Add(o["Provider"].ToString(), false);
                }
            }
        }

        /// <summary>
        /// Updates two lists: 
        /// the first has the user's non-permitted hospitals; 
        /// the second has the user's non-permitted systems
        /// </summary>
        private void UpdateListsOfUserNonPermittedHospitalAndSystemFolders()
        {
            if (_userNonPermittedHospitalFolders.Count > 0)
            {
                _userNonPermittedHospitalFolders.Clear();
            }

            if (_userNonPermittedSystemFolders.Count > 0)
            {
                _userNonPermittedSystemFolders.Clear();
            }

            foreach (KeyValuePair<string, string> hospital in AllDistinctHospitalAndSystemFolders)
            {
                if (!_userPermittedHospitalFolders.Contains(hospital.Key))
                {
                    _userNonPermittedHospitalFolders.Add(hospital.Key);
                    if (!_userNonPermittedSystemFolders.Contains(hospital.Value))
                    {
                        _userNonPermittedSystemFolders.Add(hospital.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Updates a list with the system folders that the user is allowed to see
        /// </summary>
        /// <param name="username">The username to use for updating the list of system folders</param>
        private void GetListOfUserPermittedHospitalAndSystemFolders(string username)
        {
            string getUserRow = "WebsiteUsername='" + username + "'";

            if (AllDistinctHospitalAndSystemFolders.Count < 1)
            {
                UpdateDictionaryOfDistinctHospitalAndSystemFolders();
            }

            if (_userPermittedSystemFolders.Count > 0)
            {
                _userPermittedSystemFolders.Clear();
            }

            DataRow[] rows = UserInfoTable.Select(getUserRow);

            if (!rows.Any())
            {
                throw new Exception(String.Format("WebsiteUsername={0} does not exist in spreadsheet", username));
            }

            foreach (DataRow o in UserInfoTable.Select(getUserRow))
            {
                // The general case where System does not equal 'All'
                if (o["System"] != null && o["System"].ToString().ToLower() != "all")
                {
                    // Update System folder list
                    if (!_userPermittedSystemFolders.Contains(o["System"].ToString()))
                    {
                        _userPermittedSystemFolders.Add(o["System"].ToString());
                    }

                    // Update hospital folder list
                    if (o["Provider"] != null)
                    {
                        if (!_userPermittedHospitalFolders.Contains(o["Provider"].ToString()))
                        {
                            _userPermittedHospitalFolders.Add(o["Provider"].ToString());
                        }
                    }
                }
                    // The case for CRISP users where System equals 'All'
                else if (o["System"] != null && o["System"].ToString().ToLower() == "all")
                {
                    foreach (KeyValuePair<string, string> hospital in AllDistinctHospitalAndSystemFolders)
                    {
                        // Update hospital folder list
                        if (!_userPermittedHospitalFolders.Contains(hospital.Key))
                        {
                            _userPermittedHospitalFolders.Add(hospital.Key);
                            // Update system folder list
                            if (!_userPermittedSystemFolders.Contains(hospital.Value))
                            {
                                _userPermittedSystemFolders.Add(hospital.Value);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a sourceId is part of statewide by checking in the Lookup.Hospital table
        /// </summary>
        /// <param name="sourceId">The sourceId to check</param>
        /// <returns>true or false</returns>
        private bool IsPartOfStatewide(string sourceId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(
                    String.Format("SELECT isPartOfStatewide FROM Lookup.Hospital WHERE SourceID='{0}'", sourceId),
                    connection))
                {
                    connection.Open();
                    string sqlResult = command.ExecuteScalar().ToString();
                    if (sqlResult == "True")
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// Fills a dictionary with hospital-statewide key-value pairs
        /// </summary>
        private void UpdateDictionaryOfHospitalAndStatewideMapping()
        {
            foreach (DataRow o in UserInfoTable.Select().Where
                (o => o["Provider"].ToString().ToLower() != "all").
                Where(o => !_hospitalAndStatewideMapping.ContainsKey(o["Provider"].ToString())))
            {
                _hospitalAndStatewideMapping.Add(o["Provider"].ToString(),
                    IsPartOfStatewide(o["Provider"].ToString()) ? "1" : "0");
            }
        }

        /// <summary>
        /// Fills a dictionary with hospital-system key-value pairs
        /// </summary>
        private void UpdateDictionaryOfDistinctHospitalAndSystemFolders()
        {
            foreach (DataRow o in UserInfoTable.Select())
            {
                // Currently hospital 210904 is not part of our hospital reports. I followed up with Alice regarding this.
                if (!AllDistinctHospitalAndSystemFolders.ContainsKey(o["Provider"].ToString()) &&
                    o["Provider"].ToString().ToLower() != "all" && o["Provider"].ToString() != "210904")
                {
                    AllDistinctHospitalAndSystemFolders.Add(o["Provider"].ToString(), o["System"] + " System");
                }
            }
        }

        #endregion
    }
}