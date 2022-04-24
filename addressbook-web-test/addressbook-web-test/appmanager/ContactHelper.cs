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
            if(!IsExistsContact(v))
            {
                Create(new ContactData("New1", "Never1", "Newerr1"));
            }
            InitContactModification(v);
            FillContactForm(modyfiedContact);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }
        public ContactHelper Remove(int v)
        {
            if(!IsExistsContact(v))
            {
                Create(new ContactData("New", "For", "Delete"));
            }
            SelectContact(v);
            RemoveSelectedContacts();
            acceptNextAlert = true;
            CloseAlertAndGetItsText();
            manager.Navigation.GoToHomePage();

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
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("(//img[@title=\"Edit\"])[" + v + "]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//input[@name=\"update\"][2]")).Click();

            return this;
        }
        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath("//tr[@name=\"entry\"][" + v + "]//input[@type=\"checkbox\"]")).Click();

            return this;
        }
        public ContactHelper RemoveSelectedContacts()
        {
            driver.FindElement(By.XPath("//input[@value=\"Delete\"]")).Click();

            return this;
        }
        public bool IsExistsContact(int v)
        {
            return IsElementPresent(By.XPath("//tr[@name=\"entry\"][" + v + "]//input[@type=\"checkbox\"]"));
        }
    }
}
