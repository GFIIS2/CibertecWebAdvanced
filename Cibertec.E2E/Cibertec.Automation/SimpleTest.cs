using OpenQA.Selenium.Chrome;

namespace Cibertec.Automation
{
    public static class SimpleTest
    {
        public static void Navigate()
        {
            //NOTA: La URL inicia con HTTP://
            var driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("http://www.google.com.pe");
            driver.Navigate().GoToUrl("http://localhost:5000/Customer"); 

            //NOTA: Con este comando cierro sino lo dejo abierto
            //driver.Close();
        }
    }
}
