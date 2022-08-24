using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuggyCarsRating.PageLocators;
using BuggyCarsRating.TestData;
using BuggyCarsRating.BaseTests;

namespace BuggyCarsRating.Pages
{
    public class RegistrationAndLoginPage
    {
        IWebDriver driver;
        public RegistrationAndLoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Method to register new user
        /// Method to validate registration of an existing user is not allowed
        /// </summary>
        /// <param name="login"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="password"></param>
        /// <param name="confirmpswd"></param>
        /// <returns></returns>
        [TestMethod]
        public bool Registration(string login, string firstname, string lastname, string password, bool isSameLoginID = false)
        {
            bool result = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int32.Parse("60")));
            wait.Until(driver => driver.FindElement(By.XPath(Locators.Register)));
            driver.FindElement(By.XPath(Locators.Register)).Click();
            if (!isSameLoginID)
            {
                login = "Login" + RandomString(4);
            }

            driver.FindElement(By.Id(Locators.LoginID)).Click();
            driver.FindElement(By.Id(Locators.LoginID)).SendKeys(login);
            driver.FindElement(By.Id(Locators.FirstName)).SendKeys(firstname);
            driver.FindElement(By.Id(Locators.LastName)).SendKeys(lastname);
            driver.FindElement(By.Id(Locators.Password)).SendKeys(password);
            driver.FindElement(By.Id(Locators.ConfirmPassword)).SendKeys(password);
            //reusablemethods.CaptureScreenShot(driver, _testContext, "Registration");
            driver.FindElement(By.XPath(Locators.RegisterButton)).Click();
            Console.WriteLine("User Registered with Login: {0}, Firstname: {1}, Lastname:{2}", login, firstname, lastname);
            if (!isSameLoginID)
            {
                wait.Until(driver => driver.FindElement(By.XPath(Locators.RegistrationSuccessMsgHolder)));
                var successMsg = driver.FindElement(By.XPath(Locators.RegistrationSuccessMsgHolder));
                if (successMsg.Displayed)
                {
                    Console.WriteLine("User: {0} registered successfully", firstname);
                    result = true;
                }
                else
                {
                    Console.WriteLine("User is not registered successfully. TEST FAILED");
                    result = false;
                }
                driver.FindElement(By.XPath(Locators.Cancel)).Click();
                driver.FindElement(By.XPath(Locators.Logout)).Click();
            }
            else
            {
                wait.Until(driver => driver.FindElement(By.XPath(Locators.RegistrationUserExistsErrorMsg)));
                var errorMsg = driver.FindElement(By.XPath(Locators.RegistrationUserExistsErrorMsg));
                if (errorMsg.Displayed)
                {
                    Console.WriteLine("User already Exists");
                    result = true;
                }
                else
                {
                    Console.WriteLine("Validation failed for existing user registeration. TEST FAILED");
                    result = false;
                }
                driver.FindElement(By.XPath(Locators.Cancel)).Click();
            }
            return result;
        }

        /// <summary>
        /// Method to validate the user login with valid credentials can login
        /// Method to validate the user login with invalid credentials cannot login
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="checks"></param>
        /// <param name="isLogout"></param>
        /// <returns></returns>
        [TestMethod]
        public bool LoginUser(string loginId, string password, string firstName, string checks, bool isLogout = false)
        {
            bool result = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int32.Parse("60")));
            wait.Until(driver => driver.FindElement(By.Name(Locators.LoginTextbox)));
            driver.FindElement(By.Name(Locators.LoginTextbox)).Click();
            driver.FindElement(By.Name(Locators.LoginTextbox)).SendKeys(loginId);
            driver.FindElement(By.Name(Locators.PasswordTextbox)).SendKeys(password);
            driver.FindElement(By.XPath(Locators.LoginButton)).Click();

            switch (checks)
            {
                case "ValidUser":
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    wait.Until(driver => driver.FindElement(By.XPath(Locators.LoggedInUserHolder)));
                    string LoggedInUser = driver.FindElement(By.XPath(Locators.LoggedInUserHolder)).Text;
                    if (LoggedInUser.Contains(firstName))
                    {
                        Console.WriteLine("Login is successful for the user with valid credentials: {0}, {1}", loginId, password);
                        result = true;
                    }
                    else
                    {
                        result = false;
                        Console.WriteLine("Login unsuccesful for the user: {0}. TEST FAILED", loginId);
                    }
                    break;
                case "InValidUser":
                    wait.Until(driver => driver.FindElement(By.XPath(Locators.InvalidUsernamePswdMsgHolder)));
                    IWebElement InvalidError = driver.FindElement(By.XPath(Locators.InvalidUsernamePswdMsgHolder));
                    if (InvalidError.Displayed)
                    {
                        Console.WriteLine("Invalid username/password error message is displayed as expected for the user with invalid credentials: {0}, {1}", loginId, password);
                        result = true;
                    }
                    else
                    {
                        result = false;
                        Console.WriteLine("Invalid username/password error message is not displayed. TEST FAILED ");
                    }
                    break;
            }
            if (isLogout)
            {
                driver.FindElement(By.XPath(Locators.Logout)).Click();
            }

            return result;
        }

        /// <summary>
        /// Helper method to scroll down to a particular element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="welement"></param>
        /// <param name="yPosition"></param>
        /// <returns></returns>
        [TestMethod]
        public static bool ScrollDownToElement(IWebDriver driver, IWebElement welement, int yPosition = 0)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("window.scroll(" + welement.Location.X + "," + (welement.Location.Y - yPosition) + ");");
            return true;
        }

        /// <summary>
        /// Method to navigate to Profile update page
        /// </summary>
        [TestMethod]
        public void NavigateToProfileUpdatePage()
        {
            driver.FindElement(By.XPath(Locators.ProfileLink)).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int32.Parse("60")));
            ScrollDownToElement(driver, driver.FindElement(By.Id(Locators.FirstName)), 100);
            wait.Until(driver => driver.FindElement(By.Id(Locators.FirstName)));
            if (driver.FindElement(By.Id(Locators.FirstName)).Displayed)
            {
                Console.WriteLine("Navigated to Profile Update Page");
            }
        }

        public static string RandomString(int length)
        {
            string chars = chars = "0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }


        /// <summary>
        /// Method to update the profile
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="gender"></param>
        /// <param name="age"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="hobby"></param>
        /// <returns></returns>
        /// [TestMethod]
        public bool UpdateProfile(string firstname, string gender, string age, string address, string phone, string hobby)
        {
            bool result = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int32.Parse("60")));
            driver.FindElement(By.Id(Locators.FirstName)).Clear();
            driver.FindElement(By.Id(Locators.FirstName)).SendKeys(firstname);
            driver.FindElement(By.Id(Locators.GenderTextbox)).Clear();
            driver.FindElement(By.Id(Locators.GenderTextbox)).SendKeys(gender);
            driver.FindElement(By.Id(Locators.AgeTextbox)).Clear();
            driver.FindElement(By.Id(Locators.AgeTextbox)).SendKeys(age);
            driver.FindElement(By.Id(Locators.AddressTextbox)).Clear();
            driver.FindElement(By.Id(Locators.AddressTextbox)).SendKeys(address);
            driver.FindElement(By.Id(Locators.PhoneTextbox)).Clear();
            driver.FindElement(By.Id(Locators.PhoneTextbox)).SendKeys(phone);
            driver.FindElement(By.Id(Locators.HobbyDropdown)).Click();
            driver.FindElement(By.XPath(Locators.HobbyValue.Replace("{0}", hobby))).Click();
            driver.FindElement(By.XPath(Locators.SaveButton)).Click();
            IWebElement profSaveMsg = driver.FindElement(By.XPath(Locators.ProfileSavedMsg));
            if (profSaveMsg.Displayed)
            {
                result = true;
                Console.WriteLine("Profile Updated successfully");
                Console.WriteLine("FirstName:{0}, Gender:{1}, Age:{2}, Address{3}, Phone{4}, Hobby{5} ", firstname, gender, age, address, phone, hobby);
            }
            else
            {
                result = false;
                Console.WriteLine("Profile is not Updated successfully. TEST FAILED");
            }
            driver.FindElement(By.XPath(Locators.Cancel)).Click();
            wait.Until(driver => driver.FindElement(By.XPath(Locators.Logout)));
            driver.FindElement(By.XPath(Locators.Logout)).Click();
            return result;
        }

        /// <summary>
        /// Method to reset the password
        /// </summary>
        /// <param name="curPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="isLogOut"></param>
        /// <returns></returns>
        [TestMethod]
        public bool ResetPassword(string curPassword, string newPassword, bool isLogOut = true)
        {
            bool result = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int32.Parse("60")));
            driver.FindElement(By.Id(Locators.CurrentPswd)).SendKeys(curPassword);
            driver.FindElement(By.Id(Locators.NewPswd)).SendKeys(newPassword);
            driver.FindElement(By.Id(Locators.NewConfirmPswd)).SendKeys(newPassword);
            driver.FindElement(By.XPath(Locators.SaveButton)).Click();
            IWebElement profSaveMsg = driver.FindElement(By.XPath(Locators.ProfileSavedMsg));
            if (profSaveMsg.Displayed)
            {
                result = true;
                Console.WriteLine("Profile Updated successfully");
            }
            else
            {
                result = false;
                Console.WriteLine("Profile is not Updated successfully. TEST FAILED");
            }
            driver.FindElement(By.XPath(Locators.Cancel)).Click();
            if (isLogOut)
            {
                wait.Until(driver => driver.FindElement(By.XPath(Locators.Logout)));
                driver.FindElement(By.XPath(Locators.Logout)).Click();
            }

            return result;
        }


    }
}
