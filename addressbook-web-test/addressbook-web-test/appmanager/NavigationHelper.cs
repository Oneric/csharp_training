using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private readonly string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }
        /// <summary>
        /// Переходим на главную страницу
        /// </summary>
        /// <returns></returns>
        public NavigationHelper GoToHomePage()
        {
            if(driver.Url == baseURL)
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL);
            return this;
        }
        /// <summary>
        /// Нажимаем на ссылку add new для создания нового контакта
        /// </summary>
        /// <returns></returns>
        public NavigationHelper InitNewContactCreation()
        {
            if(driver.Url == baseURL + "/edit.php" && driver.FindElement(By.XPath("//div[@id=\"content\"]/h1")).Text.Equals("Edit / add address book entry"))
            {
                return this;
            }  
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        /// <summary>
        /// Переходим на страницу groups
        /// </summary>
        /// <returns></returns>
        public NavigationHelper GoToGroupsPage()
        {
            if (driver.FindElement(By.XPath("//title")).Text.Contains("Groups") && IsElementPresent(By.Name("new")))
            {
                return this;
            }
            driver.FindElement(By.LinkText("groups")).Click();
            return this;
        }
    }
}
