using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using BuggyCarsRating.TestData;
using BuggyCarsRating.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuggyCarsRating.BaseTests
{
    [TestClass]
    public class BaseTest
    {
        public IWebDriver driver;
        [TestInitialize]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(BuggyCarsTestData.url);
        }

        public RegistrationAndLoginPage Page
        {
            get
            {
                return new RegistrationAndLoginPage(driver);
            }
        }

        [TestCleanup]
        public void CloseBrowser() => driver.Dispose();
    }
}

