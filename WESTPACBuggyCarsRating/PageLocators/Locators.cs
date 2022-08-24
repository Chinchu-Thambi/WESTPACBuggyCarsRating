using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuggyCarsRating.PageLocators
{
    #region Locators
    public class Locators
    {
        //Registration page
        public static string LoginID = "username";
        public static string FirstName = "firstName";
        public static string LastName = "lastName";
        public static string Password = "password";
        public static string ConfirmPassword = "confirmPassword";
        public static string RegisterButton = "//button[text()='Register']";
        public static string RegistrationSuccessMsgHolder = "//div[contains(text(),'Registration is successful')]";
        public static string RegistrationUserExistsErrorMsg = "//div[contains(text(),'UsernameExistsException: User already exists')]";
        public static string Cancel = "//a[text()='Cancel']";

        //Sign In section
        public static string LoginTextbox = "login";
        public static string PasswordTextbox = "password";
        public static string LoginButton = "//button[text()='Login']";
        public static string Register = "//a[text()='Register']";
        public static string LoggedInUserHolder = "//div[@class='container']//span";
        public static string ProfileLink = "//a[text()='Profile']";
        public static string Logout = "//a[text()='Logout']";
        public static string InvalidUsernamePswdMsgHolder = "//span[text()='Invalid username/password']";

        //update profile section
        public static string GenderTextbox = "gender";
        public static string AgeTextbox = "age";
        public static string AddressTextbox = "address";
        public static string PhoneTextbox = "phone";
        public static string HobbyDropdown = "hobby";
        public static string HobbyValue = "//*[@id='hobby']/option[text()='{0}']";
        public static string SaveButton = "//button[text()='Save']";
        public static string ProfileSavedMsg = "//div[contains(text(),'The profile has been saved successful')]";

        //reset password
        public static string CurrentPswd = "currentPassword";
        public static string NewPswd = "newPassword";
        public static string NewConfirmPswd = "newPasswordConfirmation";
    }
    #endregion Locators
}
