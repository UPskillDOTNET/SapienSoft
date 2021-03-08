using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumCoreTests.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCoreTests.Tests
{
    class LoginTest
    {
        //Browser driver
        IWebDriver webDriver = new FirefoxDriver();

        //Hooks in NUnit
        [SetUp]
        public void Setup()
        {
            //Navigate to site
            webDriver.Navigate().GoToUrl("https://localhost:44305/");
        }

        [Test]
        public void Login()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.ClickLogin();

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.Login("admin@sapiensoft.com", "Admin123!");

            Assert.That(homePage.IsProfileDetailsExist, Is.True);
        }

        [TearDown]
        public void TearDown() => webDriver.Quit();
    }
}
