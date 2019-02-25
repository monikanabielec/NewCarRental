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
        [FindsBy(How = How.Id, Using = "Login")]
        public IWebElement UserName { get; set; }
        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }
        [FindsBy(How = How.Id, Using = "btn btn-default")]
        public IWebElement Submit { get; set; }
    }
}
