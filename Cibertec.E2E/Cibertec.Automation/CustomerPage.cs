using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Cibertec.Automation
{
    public class CustomerPage
    {
        const string url = "http://localhost/CibertecWebMVC";

        private readonly IWebDriver _driver;

        #region Customer Page Elements
        [FindsBy(How = How.CssSelector, Using = "a[href*='/Customer']")]
        private IWebElement customerLink = null;

        [FindsBy(How = How.CssSelector, Using = "table.table>tbody>tr")]
        private IList<IWebElement> customerList= null;
        #endregion

        public CustomerPage()
        {
            _driver = Driver.Instance;
            PageFactory.InitElements(_driver, this);
        }

        public void GoToUrl()
        {
            Driver.Instance.Navigate().GoToUrl(url);
        }

        public void GoToIndex()
        {
            customerLink.Click();
        }

        public int GetListCount()
        {
            return customerList.Count;
        }
    }
    
}
