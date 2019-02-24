using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NewCarRentalSeleniumTests
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "log")]
        public IWebElement UserName { get; set; }
        [FindsBy(How = How.Id, Using = "pwd")]
        public IWebElement Password { get; set; }
        [FindsBy(How = How.Id, Using = "login")]
        public IWebElement Submit { get; set; }
    }
}
