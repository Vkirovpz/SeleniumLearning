using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class WindowHandlers
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void WindowHandle()
        {
            driver.FindElement(By.CssSelector(".blinkingText")).Click();
            Assert.That(driver.WindowHandles.Count, Is.EqualTo(3));

            var childWindowName = driver.WindowHandles[2];
            driver.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
        }

        [Test]
        public void ExtractEmailFromChildWindow()
        {
            var expectedEmail = "mentor@rahulshettyacademy.com";
            var mainWindow = driver.CurrentWindowHandle;

            driver.FindElement(By.CssSelector(".blinkingText")).Click();
            Assert.That(driver.WindowHandles.Count, Is.EqualTo(3));

            var childWindowName = driver.WindowHandles[2];
            driver.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            var extractedEmail = driver.FindElement(By.CssSelector(".red a")).Text;

            Assert.That(extractedEmail, Is.EqualTo(expectedEmail));

            driver.SwitchTo().Window(mainWindow);
            driver.FindElement(By.Id("username")).SendKeys(extractedEmail);
        }
    }
}
