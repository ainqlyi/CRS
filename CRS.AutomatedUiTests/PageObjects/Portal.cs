using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CRS.AutomatedUiTests.PageObjects
{
    /// <summary>
    /// Contains page objects for the main CRS Portal
    /// </summary>
    public class Portal : BasePage
    {
        /// <summary>
        /// Contains the header text for the main CRS Portal
        /// </summary>
        [FindsBy(How = How.Id, Using = "dnn_ctr443_HtmlModule_lblContent")]
        public IWebElement Header;

        /// <summary>
        /// CRS Reports folder
        /// </summary>
        [FindsBy(How = How.Id, Using = "dnn_ctr442_Dispatch_ajaxtwopanel_TreeView1_item_0_cell")]
        public IWebElement CrsReportsFolder;

        /// <summary>
        /// 
        /// </summary>
        [FindsBy(How = How.Id, Using = "dnn_ctr442_Dispatch_ajaxtwopanel_TreeView1_item_1_cell")]
        public IWebElement StatewideFolder;
    }
}