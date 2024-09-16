using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class FunctionalTest
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
        public void Dropdown()
        {
            var dropDown = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement s = new SelectElement(dropDown);
            //s.SelectByIndex(1);
            //s.SelectByValue("teach");
            s.SelectByText("Teacher");
        }

        [Test]
        public void RadioButtons()
        {
            var radioBtns = driver.FindElements(By.CssSelector("input[type='radio']"));
            foreach (var btn in radioBtns)
            {
                if (btn.GetAttribute("value").Equals("user"))
                {
                    btn.Click();
                }
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));

            driver.FindElement(By.Id("okayBtn")).Click();
            var isRadioBtnSelected = driver.FindElement(By.Id("usertype")).Selected;
            //Assert.That(isRadioBtnSelected, Is.True);

        }
    }
}
