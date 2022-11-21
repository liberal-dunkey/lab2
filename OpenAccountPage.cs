using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras.PageObjects;
using NUnit.Framework;

namespace Lab_2
{
    public class OpenAccountPage
    {
        public IWebDriver Driver;
        public static WebDriverWait? wait;

        public OpenAccountPage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[1]/div[2]/button")]
        private IWebElement BankManagerLoginBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[1]/button[2]")]
        private IWebElement OpenAccountBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[2]/div/div/form/div[1]/select")]
        private IWebElement customerName { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[2]/div/div/form/div[2]/select")]
        private IWebElement currency { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[2]/div/div/form/button")]
        private IWebElement processBtn { get; set; }

        private string alertText;
        public void CheckThatAlertContainsText(string message)
        {
            Assert.That(alertText.Contains(message));
        }

        public OpenAccountPage OpenANewAccount()
        {
            BankManagerLoginBtn.Click();
            OpenAccountBtn.Click();

            var selectFirstDropDown = new SelectElement(customerName);
            selectFirstDropDown.SelectByText("Hermoine Granger");

            var selectSecondDropDown = new SelectElement(currency);
            selectSecondDropDown.SelectByText("Rupee");

            processBtn.Click();

            IAlert alertWindow = wait.Until(ExpectedConditions.AlertIsPresent());
            alertText = alertWindow.Text;
            alertWindow.Accept();

            return new OpenAccountPage(Driver);
        }
    }
}
