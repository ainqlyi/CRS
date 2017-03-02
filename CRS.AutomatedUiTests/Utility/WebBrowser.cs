using System.Configuration;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using TechTalk.SpecFlow;

namespace CRS.AutomatedUiTests.Utility
{
    /// <summary>
    /// Contains methods and code related to the webDriver
    /// </summary>
    [Binding]
    public class WebBrowser
    {
        /// <summary>
        /// Initializes an instance of a webdriver into SpecFlow's context injection mechanism called ScenarioContext.Current.
        /// Determines browser based on the app.config "browser" key
        /// </summary>
        public static IWebDriver Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    switch (ConfigurationManager.AppSettings["browser"].ToString(CultureInfo.InvariantCulture).ToLower())
                    {
                        case "firefox":
                            ScenarioContext.Current["browser"] = new FirefoxDriver();
                            break;
                        case "ie":
                            ScenarioContext.Current["browser"] = new InternetExplorerDriver();
                            break;
                        case "chrome":
                            ScenarioContext.Current["browser"] = new ChromeDriver();
                            break;
                        case "safari":
                            ScenarioContext.Current["browser"] = new SafariDriver();
                            break;
                        default:
                            ScenarioContext.Current["browser"] = new FirefoxDriver();
                            break;
                    }
                }
                return (IWebDriver)ScenarioContext.Current["browser"];
            }
        }

        /// <summary>
        /// Uses the [AfterScenario] SpecFlow C# attribute to close the browser after each scenario
        /// </summary>
        [AfterScenario]
        public static void Close()
        {
            if (ScenarioContext.Current.ContainsKey("browser"))
            {
                Current.Dispose();
            }
        }
    }
}