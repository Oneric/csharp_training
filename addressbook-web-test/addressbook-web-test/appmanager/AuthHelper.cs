using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class AuthHelper : HelperBase
    {
        public AuthHelper(ApplicationManager manager) : base(manager)
        {
        }
        public AuthHelper Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return this;
                }
                Logout();
            }
            FeelingTextInput(By.Name("user"), account.Username);
            FeelingTextInput(By.Name("pass"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            return this;
        }

        public bool IsLoggedIn()
        {
           return IsElementPresent(By.LinkText("Logout"));
        }
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && driver.FindElement(By.XPath("//form[@name=\"logout\"]/b")).Text == "(" + account.Username + ")";
        }
        public AuthHelper Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
                return this;
            }
            return this;
        }
    }
}
