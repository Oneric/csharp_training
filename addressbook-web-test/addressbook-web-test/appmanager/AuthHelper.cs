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
        /// <summary>
        /// Набор шагов для авторизации
        /// </summary>
        /// <param name="account">Объект класса AccountData c данными авторизации</param>
        /// <returns></returns>
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
        /// <summary>
        /// Проверяем авторизацию
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {
           return IsElementPresent(By.LinkText("Logout"));
        }
        /// <summary>
        /// Проверяем авторизацию и соответствие имени авторизованного пользователя
        /// </summary>
        /// <param name="account">Объект класса AccountData c данными авторизации</param>
        /// <returns></returns>
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetAutenticatedUserName() == account.Username;
        }
        /// <summary>
        /// Получаем имя авторизованного пользователя
        /// </summary>
        /// <returns></returns>
        public string GetAutenticatedUserName()
        {
            string userName = driver.FindElement(By.XPath("//form[@name=\"logout\"]/b")).Text;

            return userName.Substring(1, userName.Length - 2);
        }
        /// <summary>
        /// Выполяем выход из приложения
        /// </summary>
        /// <returns></returns>
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
