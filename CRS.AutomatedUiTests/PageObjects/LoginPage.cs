using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CRS.AutomatedUiTests.PageObjects
{
    /// <summary>
    /// Contains page objects for the CRS login page
    /// </summary>
    public class LoginPage : BasePage
    {
        /// <summary>
        /// The username textbox on the CRS login page
        /// </summary>
        [FindsBy(How = How.Id, Using = "dnn_ctr437_View_tbUsername")] 
        public IWebElement UsernameTextbox;

        /// <summary>
        /// The password textbox on the CRS login page
        /// </summary>
        [FindsBy(How = How.Id, Using = "dnn_ctr437_View_tbPassword")] 
        public IWebElement PasswordTextbox;

        /// <summary>
        /// The login button on the CRS login page
        /// </summary>
        [FindsBy(How = How.Id, Using = "dnn_ctr437_View_cmdLogin")] 
        public IWebElement LoginButton;

        #region Helper Methods

        /// <summary>
        /// Logs in as the default administrator as specified in the app.config file
        /// </summary>
        public void LoginAsAdministrator()
        {
            if (!String.IsNullOrEmpty(UsernameTextbox.ToString()))
            {
                UsernameTextbox.Clear();
            }
            
            UsernameTextbox.SendKeys(ConfigurationManager.AppSettings["username"]);

            if (!String.IsNullOrEmpty(PasswordTextbox.ToString()))
            {
                PasswordTextbox.Clear(); 
            }
            
            PasswordTextbox.SendKeys(ConfigurationManager.AppSettings["password"]);

            LoginButton.Click();
        }

        /// <summary>
        /// Logs in as the specified username and password
        /// </summary>
        /// <param name="username">username for login page</param>
        /// <param name="password">password for login page</param>
        public void LoginAs(string username, string password)
        {
            if (!String.IsNullOrEmpty(UsernameTextbox.ToString()))
            {
                UsernameTextbox.Clear();
            }

            UsernameTextbox.SendKeys(username);

            if (!String.IsNullOrEmpty(PasswordTextbox.ToString()))
            {
                PasswordTextbox.Clear();
            }

            PasswordTextbox.SendKeys(password);

            LoginButton.Click();
        }
        #endregion
    }
}