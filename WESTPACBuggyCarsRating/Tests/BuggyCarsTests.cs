using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using BuggyCarsRating.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuggyCarsRating.BaseTests;
using BuggyCarsRating.Pages;

namespace BuggyCarsRating.Tests
{
    [TestClass]
    public class BuggyCarsTests : BaseTest
    {
        /// <summary>
        /// Test case to register a user
        /// </summary>
        [TestMethod]
        public void A_UserRegistration()
        {
            Assert.IsTrue(Page.Registration(BuggyCarsTestData.loginID, BuggyCarsTestData.firstName, BuggyCarsTestData.lastName, BuggyCarsTestData.password));
        }



        /// <summary>
        /// Test case to very the user can login with valid credentials
        /// Also, verifies user cannot login with invalid credentials
        /// </summary>
        [TestMethod]
        public void B_LoginUser()
        {
            Assert.IsTrue(Page.LoginUser(BuggyCarsTestData.loginID, BuggyCarsTestData.password, BuggyCarsTestData.firstName, "ValidUser", true));
            Assert.IsTrue(Page.LoginUser("InvalidUser", "InvalidPassword", "", "InValidUser", false));
        }



        /// <summary>
        /// Test case to verify that user can update the profile
        /// </summary>
        [TestMethod]
        public void C_UserProfileUpdate()
        {
            string firstName, gender, age, address, phone, hobby;
            firstName = BuggyCarsTestData.updatedFirstName;
            gender = BuggyCarsTestData.gender;
            age = BuggyCarsTestData.age;
            address = BuggyCarsTestData.address;
            phone = BuggyCarsTestData.phone;
            hobby = BuggyCarsTestData.hobby;
            Assert.IsTrue(Page.LoginUser(BuggyCarsTestData.loginID, BuggyCarsTestData.password, BuggyCarsTestData.firstName, "ValidUser", false));
            Page.NavigateToProfileUpdatePage();
            Assert.IsTrue(Page.UpdateProfile(firstName, gender, age, address, phone, hobby));
        }



        /// <summary>
        /// Test case to verify user can reset password, login with the new password set
        /// Also, sets the password to original
        /// </summary>
        [TestMethod]
        public void D_ResetPassword()
        {
            //login with valid credentials
            Assert.IsTrue(Page.LoginUser(BuggyCarsTestData.loginID, BuggyCarsTestData.password, BuggyCarsTestData.firstName, "ValidUser", false));
            Page.NavigateToProfileUpdatePage();
            //reset password
            Assert.IsTrue(Page.ResetPassword(BuggyCarsTestData.password, BuggyCarsTestData.newPassword, true));
            //validate user can login with new password
            Assert.IsTrue(Page.LoginUser(BuggyCarsTestData.loginID, BuggyCarsTestData.newPassword, BuggyCarsTestData.updatedFirstName, "ValidUser", false));
            // reset back to original password
            Page.NavigateToProfileUpdatePage();
            Assert.IsTrue(Page.ResetPassword(BuggyCarsTestData.newPassword, BuggyCarsTestData.password));
        }

        /// <summary>
        /// Test case to verify user cannot register existing user [Login10]
        /// </summary>
        [TestMethod]
        public void E_RegisterExistingUser()
        {
            Assert.IsTrue(Page.Registration(BuggyCarsTestData.loginID, BuggyCarsTestData.firstName2, BuggyCarsTestData.lastName2, BuggyCarsTestData.password, true));
        }
    }
}
