using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCoreTests.Pages
{
    class LoginPage
    {
        public LoginPage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public IWebDriver Driver { get; }
        IWebElement txtEmail => Driver.FindElement(By.Name("Email"));
        IWebElement txtPassword => Driver.FindElement(By.Name("Password"));
        IWebElement btnLogin => Driver.FindElement(By.XPath("//input[@value='Submit']"));

        public void Login(string email, string password)
        {
            txtEmail.SendKeys(email);
            txtPassword.SendKeys(password);
            btnLogin.Submit();
        }
    }
}