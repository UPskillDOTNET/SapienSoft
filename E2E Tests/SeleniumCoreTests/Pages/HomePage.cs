using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCoreTests.Pages
{
    //Home Page
    class HomePage
    {
        public HomePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public IWebDriver Driver { get; }
        public IWebElement lnkLogin => Driver.FindElement(By.LinkText("Login"));
        public IWebElement lnkProfileDetails => Driver.FindElement(By.PartialLinkText("Welcome"));

        public bool IsProfileDetailsExist() => lnkProfileDetails.Displayed;

        public void ClickLogin() => lnkLogin.Click();
        

    }
}
