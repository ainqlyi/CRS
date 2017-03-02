using CRS.AutomatedUiTests.PageObjects;
using CRS.AutomatedUiTests.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace CRS.AutomatedUiTests.Steps
{
    /// <summary>
    /// Contains the SpecFlow step methods that bind to the LogIn.feature file
    /// </summary>
    [Binding]
    public class LoginSteps
    {
        readonly LoginPage _loginPage = new LoginPage();

        /// <summary>
        /// Navigates to the url provided
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        [Given(@"I navigate to '(.*)'")]
        public void GivenINavigateTo(string url)
        {
            WebBrowser.Current.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Logs in as an administrator
        /// </summary>
        [When(@"I login as an administrator")]
        public void WhenILoginAsAnAdministrator()
        {
            _loginPage.LoginAsAdministrator();
        }

        /// <summary>
        /// Logs in to the CRS Portal as the given username and password
        /// </summary>
        /// <param name="username">The username to use</param>
        /// <param name="password">The password to use</param>
        [When(@"I login as '(.*)' and '(.*)'")]
        public void WhenILoginAsAnd(string username, string password)
        {
            _loginPage.LoginAs(username, password);
        }

        /// <summary>
        /// Verifies that user is logged into the CRS Website
        /// </summary>
        [Then(@"I am logged into the CRS website")]
        public void ThenIAmLoggedIntoTheCrsWebsite()
        {
            Assert.IsTrue(!Utility.Utility.IsElementPresent(By.Id("dnn_ctr437_View_tbUsername")));
        }

        /// <summary>
        /// Verifies that user is not logged into the CRS website
        /// </summary>
        [Then(@"I am not logged into the CRS website")]
        public void ThenIAmNotLoggedIntoTheCrsWebsite()
        {
            Assert.IsTrue(Utility.Utility.IsElementPresent(By.Id("dnn_ctr437_View_tbUsername")));
        }
    }
}