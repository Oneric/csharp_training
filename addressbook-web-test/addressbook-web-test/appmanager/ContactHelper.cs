using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigation.InitNewContactCreation();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();

            return this;
        }
        public ContactHelper Modify(int v, ContactData modyfiedContact)
        {
            InitContactModification(v);
            FillContactForm(modyfiedContact);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }
        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveSelectedContacts();
            acceptNextAlert = true;
            CloseAlertAndGetItsText();
            ReturnToHomePage();

            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            FeelingTextInput(By.Name("firstname"), contact.Firstname);
            FeelingTextInput(By.Name("middlename"), contact.Middlename);
            FeelingTextInput(By.Name("lastname"), contact.Lastname);
            FeelingTextInput(By.Name("nickname"), contact.Nickname);
            FeelingTextInput(By.Name("photo"), contact.Photo);
            FeelingTextInput(By.Name("title"), contact.Title);
            FeelingTextInput(By.Name("company"), contact.Company);
            FeelingTextInput(By.Name("address"), contact.Address);
            FeelingTextInput(By.Name("home"), contact.PhoneHome);
            FeelingTextInput(By.Name("mobile"), contact.PhoneMobile);
            FeelingTextInput(By.Name("work"), contact.PhoneWork);
            FeelingTextInput(By.Name("fax"), contact.PhoneFax);
            FeelingTextInput(By.Name("email"), contact.Email);
            FeelingTextInput(By.Name("email2"), contact.Email2);
            FeelingTextInput(By.Name("email3"), contact.Email3);
            FeelingTextInput(By.Name("homepage"), contact.Homepage);
            SetSelectByText(By.Name("bday"), contact.Bday);
            SetSelectByText(By.Name("bmonth"), contact.Bmonth);
            FeelingTextInput(By.Name("byear"), contact.Byear);
            SetSelectByText(By.Name("aday"), contact.Aday);
            SetSelectByText(By.Name("amonth"), contact.Amonth);
            FeelingTextInput(By.Name("ayear"), contact.Ayear);
            SetSelectByText(By.Name("new_group"), contact.NewGroup);
            FeelingTextInput(By.Name("address2"), contact.Address2);
            FeelingTextInput(By.Name("phone2"), contact.Phone2);
            FeelingTextInput(By.Name("notes"), contact.Notes);

            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//form/input[@value='Enter'][2]")).Click();
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("(//img[@title=\"Edit\"])[" + (v + 1) + "]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//input[@name=\"update\"][2]")).Click();

            return this;
        }
        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//tr[@name=\"entry\"][" + (v + 1) + "]//input[@type=\"checkbox\"]")).Click();

            return this;
        }
        public ContactHelper RemoveSelectedContacts()
        {
            driver.FindElement(By.XPath("//input[@value=\"Delete\"]")).Click();

            return this;
        }
        public bool IsExistsContact(int v)
        {
            return IsElementPresent(By.XPath("//tr[@name=\"entry\"][" + (v + 1) + "]//input[@type=\"checkbox\"]"));
        }
        public List<ContactData> GetContactList()
        {
            List<ContactData> contactList = new List<ContactData>();

            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name=\"entry\"]"));
            foreach (IWebElement element in elements)
            {
                contactList.Add(new ContactData(element.FindElement(By.XPath("./td[2]")).Text, element.FindElement(By.XPath("./td[3]")).Text)
                {
                    Address = element.FindElement(By.XPath("./td[3]")).Text,
                    Email = element.FindElement(By.XPath("./td[4]")).Text,
                    PhoneMobile = element.FindElement(By.XPath("./td[5]")).Text,
                });
            }
            return contactList;
        }
    }
}
