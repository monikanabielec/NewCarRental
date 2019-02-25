using NUnit.Framework;




namespace NewCarRentalSeleniumTests
{
    [TestFixture]
    public class LogInTest : MainTest
    {
       
        private readonly string _login = "anna";
        private readonly string _password = "anna";
        [Test]
        public void LoginTest()
        {
            // UWAGA!!! nalezy zmienic adres localhost'a !! 
            _driver.Url = "http://localhost:50283/Login";
            var homePage = new HomePage(_driver);
            homePage.MyAccount.Click();
            var loginPage = new LoginPage(_driver);
            loginPage.UserName.SendKeys(_login);
            loginPage.Password.SendKeys(_password);
            loginPage.Submit.Submit();
            Assert.IsTrue(homePage.HeaderText.Displayed);
            Assert.AreEqual("Your Account", homePage.HeaderText.Text);
        }
    }
}
