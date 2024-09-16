using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class SortWebTables
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]
        public void SortTable()
        {
            var initialList = new List<string>();


            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            //Get all products into initial list
            var productsA = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (var product in productsA)
            {
                initialList.Add(product.Text);
            }

            //Sort the initial list
            initialList.Sort();

            //Click the Column to sort in web
            driver.FindElement(By.CssSelector("th[aria-label *= 'fruit name']")).Click();

            //Get all products into sortedListWeb
            var sortedListWeb = new List<string>();
            var productsB = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (var product in productsB)
            {
                sortedListWeb.Add(product.Text);
            }

            //Assert sorted and web sorted lists are equals
            //Assert.AreEqual(initialList, sortedListWeb);
            Assert.That(sortedListWeb, Is.EqualTo(initialList));
        }
    }
}
