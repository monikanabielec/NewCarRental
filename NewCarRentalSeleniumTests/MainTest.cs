using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace NewCarRentalSeleniumTests
{
    [TestFixture]
    public abstract class MainTest
    {
        public IWebDriver _driver { private set; get; }
        [SetUp]
        public void StartBrowser()
        {
            _driver = new FirefoxDriver();
        }
        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}
