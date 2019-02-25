using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NewCarRentalSeleniumTests
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = ".//a[text()= 'Log in']")]
        public IWebElement MyAccount { get; set; }
        [FindsBy(How = How.CssSelector, Using = "h2")]
        public IWebElement HeaderText { get; set; }
        //public void GoToPage()
        //{
        //    driver.Navigate().GoToUrl("https://google.pl");
        //}
    }
}
