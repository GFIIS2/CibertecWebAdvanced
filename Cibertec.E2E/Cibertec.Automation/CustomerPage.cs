using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Cibertec.Automation
{
    public class CustomerPage
    {
        private readonly IWebDriver _driver;
        #region Page Elements
        [FindsBy(How = How.CssSelector, Using = "a[href*='Customer']")]
        private IWebElement customerLink = null;

        [FindsBy(How = How.CssSelector, Using = "table.table>tbody>tr")]
        private IList<IWebElement> customerList = null;
        #endregion
        public CustomerPage()
        {
            _driver = Driver.Instance;
            PageFactory.InitElements(_driver, this);
        }
        public void Go()
        {
            Driver.Instance.Navigate().GoToUrl("http://localhost/CibertecWebMvc");
        }

        public void GotToIndex()
        {
            customerLink.Click();
        }

        public int GetList()
        {
            return customerList.Count;
        }
    }
    
}
