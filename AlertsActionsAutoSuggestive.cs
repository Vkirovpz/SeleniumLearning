using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace SeleniumLearning
{
    public class AlertsActionsAutoSuggestive
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }

        [Test]
        public void test_Frames()
        {
            var frameScroll = driver.FindElement(By.Id("courses-iframe"));
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);
            //id, name, index
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();

            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            driver.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        }

        [Test]
        public void test_Alert()
        {
            var name = "valeto";

            driver.FindElement(By.CssSelector("#name")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();

            var alertMsg = driver.SwitchTo().Alert().Text;
            //Negative(click cancel)
            //driver.SwitchTo().Alert().Dismiss();

            //Positive
            driver.SwitchTo().Alert().Accept();

            StringAssert.Contains(name, alertMsg);
        }

        [Test]
        public void test_AutoSuggestiveDropDowns()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            Thread.Sleep(3000);

            var countries = driver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach (var country in countries)
            {
                if (country.Text.Equals("India"))
                {
                    country.Click();
                }
            }
            //To get the value on runtime .Text does not work, only GetAttribute(value) ! 
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }

        [Test]
        public void test_Actions()
        {
            driver.Url = "https://rahulshettyacademy.com";
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")).Click();
            //a.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"))).Click().Perform();
        }

        [Test]
        public void test_DragAndDrop()
        {
            driver.Url = "https://demoqa.com/droppable/";
            Actions a = new Actions(driver);
            a.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("droppable"))).Perform();
        }
    }
}
