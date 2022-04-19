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
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected AuthHelper authHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupsHelper;
        protected ContactHelper contactsHelper;

        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://addressbook.course.ru/";

            authHelper = new AuthHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
            groupsHelper = new GroupHelper(this);
            contactsHelper = new ContactHelper(this);
        }
        public IWebDriver Driver
        {
            get { return driver; }
        }
        
        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

        }

        public AuthHelper Auth { get { return authHelper; } }
        public NavigationHelper Navigation { get { return navigationHelper; } }
        public GroupHelper Groups { get { return groupsHelper; } }
        public ContactHelper Contacts { get { return contactsHelper; } }
    }
}
