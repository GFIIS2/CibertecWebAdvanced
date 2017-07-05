using OpenQA.Selenium.Chrome;

namespace Cibertec.Automation
{
    public class SimpleTest
    {
        public void Navigate()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.google.com");
            driver.Close();
            driver.Quit();
            driver = null;
        }
    }
}
