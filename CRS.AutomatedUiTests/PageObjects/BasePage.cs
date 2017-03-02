using CRS.AutomatedUiTests.Utility;
using OpenQA.Selenium.Support.PageObjects;

namespace CRS.AutomatedUiTests.PageObjects
{
    /// <summary>
    /// The base page object class that initializes a WebDriver and page objects
    /// </summary>
    public class BasePage
    {
        protected BasePage()
        {
            var driver = WebBrowser.Current;
            PageFactory.InitElements(driver, this);
        }
    }
}