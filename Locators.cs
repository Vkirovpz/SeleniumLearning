using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class Locators
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            //Implicit Timeout
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void LocatorsIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("vkirov");

            //driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("123456");

            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            var btnSIgnIn = driver.FindElement(By.Id("signInBtn"));
            btnSIgnIn.Click();
            //driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            //var btnSIgnIn = driver.FindElement(By.CssSelector("input[value='Sign In']"));

            //Explicit Timeout
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(btnSIgnIn, "Sign In"));

            string errorMsg = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMsg);
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));

            string hrefAttribute = link.GetAttribute("href");
            string expectedUrl = "https://rahulshettyacademy.com/documents-request";

            Assert.AreEqual(expectedUrl, hrefAttribute);




        }
    }
}
