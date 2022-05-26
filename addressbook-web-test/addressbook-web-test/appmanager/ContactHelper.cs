using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        /// <summary>
        /// Набор шагов для создания нового контакта
        /// </summary>
        /// <param name="contact">Объект класса ContactData с данными для создания нового контакта</param>
        /// <returns></returns>
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigation.InitNewContactCreation();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();

            return this;
        }
        /// <summary>
        /// Набор шагов для изменения контакта
        /// </summary>
        /// <param name="v">Индекс изменяемого контакта</param>
        /// <param name="modyfiedContact">Объект класса ContactData с данными для изменения контакта</param>
        /// <returns></returns>
        public ContactHelper Modify(int v, ContactData modyfiedContact)
        {
            InitContactModification(v);
            FillContactForm(modyfiedContact);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }
        /// <summary>
        /// Набор шагов для изменения контакта
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="modyfiedContact"></param>
        /// <returns></returns>
        public ContactHelper Modify(ContactData contact, ContactData modyfiedContact)
        {
            InitContactModification(contact.Id);
            FillContactForm(modyfiedContact);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }
        /// <summary>
        /// Набор шагов для удаления контакта
        /// </summary>
        /// <param name="v">Индекс контакта для удаления</param>
        /// <returns></returns>
        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveSelectedContacts();
            acceptNextAlert = true;
            CloseAlertAndGetItsText();
            ReturnToHomePage();

            return this;
        }
        /// <summary>
        /// Набор шагов для удаления контакта
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveSelectedContacts();
            acceptNextAlert = true;
            CloseAlertAndGetItsText();
            ReturnToHomePage();

            return this;
        }
        /// <summary>
        /// Заполняем форму контакта
        /// </summary>
        /// <param name="contact">Объект класса ContactData</param>
        /// <returns></returns>
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
        /// <summary>
        /// Нажимаем кнопку Enter на форме созданий контакта
        /// </summary>
        /// <returns></returns>
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//form/input[@value='Enter'][2]")).Click();
            contactListCache = null;
            return this;
        }
        /// <summary>
        /// Возврат на главную страницу
        /// </summary>
        /// <returns></returns>
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }
        /// <summary>
        /// Инициирует процесс модификации записи с указанным индексом
        /// </summary>
        /// <param name="v">Индекс записи</param>
        /// <returns></returns>
        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath($"(//img[@title=\"Edit\"])[{ v + 1 }]")).Click();
            return this;
        }
        /// <summary>
        /// Инициирует процесс модификации записи с указанным индексом
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactHelper InitContactModification(String id)
        {
            driver.FindElement(By.XPath($"//tr[@name=\"entry\"]//input[@id=\"{ id }\"]/../..//img[@title=\"Edit\"]")).Click();
            return this;
        }
        /// <summary>
        /// Открывает страниццу детальной информации о контакте с индексом v
        /// </summary>
        /// <param name="v">Индекс записи</param>
        /// <returns></returns>
        public ContactHelper OpenContactDetails(int v)
        {
            driver.FindElement(By.XPath($"(//img[@title=\"Details\"])[{ v + 1 }]")).Click();
            return this;
        }
        /// <summary>
        /// Нажимаем кнопку Update на форме изменения контакта
        /// </summary>
        /// <returns></returns>
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//input[@name=\"update\"][2]")).Click();
            contactListCache = null;

            return this;
        }
        /// <summary>
        /// Активируем чекбокс контакта с индексом
        /// </summary>
        /// <param name="v">Индекс</param>
        /// <returns></returns>
        public ContactHelper SelectContact(int v)
        {
            driver.FindElement(By.XPath($"//tr[@name=\"entry\"][{ v + 1 }]//input[@type=\"checkbox\"]")).Click();

            return this;
        }
        /// <summary>
        /// Активируем чекбокс контакта с индексом
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactHelper SelectContact(String id)
        {
            driver.FindElement(By.XPath($"//tr[@name=\"entry\"]//input[@id=\"{ id }\"]")).Click();

            return this;
        }
        /// <summary>
        /// Удаляем выбрвнный контакт
        /// </summary>
        /// <returns></returns>
        public ContactHelper RemoveSelectedContacts()
        {
            driver.FindElement(By.XPath("//input[@value=\"Delete\"]")).Click();
            contactListCache = null;

            return this;
        }
        /// <summary>
        /// Проверяем   наличие контакта по заданному индексу
        /// </summary>
        /// <param name="v">Индекс</param>
        /// <returns>Is exsist contact return true else return false</returns>
        public bool IsExistsContact(int v)
        {
            return IsElementPresent(By.XPath($"//tr[@name=\"entry\"][{ v + 1 }]//input[@type=\"checkbox\"]"));
        }

        private List<ContactData> contactListCache = null;
        /// <summary>
        /// Получаем список контактов со страницы
        /// </summary>
        /// <returns></returns>
        public List<ContactData> GetContactList()
        {
            if(contactListCache == null)
            {
                contactListCache = new List<ContactData>();

                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name=\"entry\"]"));
                foreach (IWebElement element in elements)
                {
                    contactListCache.Add(
                        new ContactData(element.FindElement(By.XPath("./td[2]")).Text, element.FindElement(By.XPath("./td[3]")).Text)
                        {
                            Id = element.FindElement(By.XPath("./td/input")).GetAttribute("value"),
                            Address = element.FindElement(By.XPath("./td[4]")).Text,
                            Email = element.FindElement(By.XPath("./td[5]")).Text,
                            PhoneMobile = element.FindElement(By.XPath("./td[6]")).Text,
                        }
                    );
                }

            }

            return new List<ContactData>(contactListCache);
        }
        public int GetNumberOfSearchResults()
        {
            string text = driver.FindElement(By.TagName("label")).Text;
            Match value = new Regex(@"\d+").Match(text);
            return int.Parse(value.Value);
        }
        /// <summary>
        /// Получаем количество контактов
        /// </summary>
        /// <returns></returns>
        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name=\"entry\"]")).Count;
        }
        /// <summary>
        /// Получает данные о контакте из таблицы контактов на главной странице по индексу записи
        /// </summary>
        /// <param name="v">Индекс записи</param>
        /// <returns>Возвращает объект класса ContactData</returns>
        public ContactData GetContactDataFromTable(int v)
        {
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[v].FindElements(By.TagName("td"));
            
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string emailAll = cells[4].Text;
            string phoneAll = cells[5].Text;


            return new ContactData(lastName, firstName)
            {
                Address = address,
                EmailAll = emailAll,
                PhoneAll = phoneAll
            };
        }
        /// <summary>
        /// Переходит на страницу редактирования по индексу записи и получает данные из формы редактирования
        /// </summary>
        /// <param name="v">Индекс записи</param>
        /// <returns>Возвращает объект класса ContactData</returns>
        public ContactData GetContactDataFromEditForm(int v)
        {
            InitContactModification(v);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).Text;
            string phoneHome = driver.FindElement(By.Name("home")).GetAttribute("value");
            string phoneMobile = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string phoneWork = driver.FindElement(By.Name("work")).GetAttribute("value");
            string phoneFax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string bday = driver.FindElement(By.XPath("//select[@name=\"bday\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string bmonth = driver.FindElement(By.XPath("//select[@name=\"bmonth\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string aday = driver.FindElement(By.XPath("//select[@name=\"aday\"]/option[@selected=\"selected\"]")).GetAttribute("value");
            string amonth = driver.FindElement(By.XPath("//select[@name=\"amonth\"]/option[@selected=\"selected\"]")).Text;
            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            return new ContactData(lastName, firstName)
            {
                Middlename = middlename,
                Address = address,
                PhoneHome = phoneHome,
                PhoneMobile = phoneMobile,
                PhoneWork = phoneWork,
                PhoneFax = phoneFax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Nickname = nickname,
                Bday = bday,
                Bmonth = bmonth,
                Byear = byear,
                Aday = aday,
                Amonth = amonth,
                Ayear = ayear
            };
        }
        /// <summary>
        /// Получаем данные о контакте на странице Details
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public ContactData GetContactDataFromDetailsPage(int v)
        {
            OpenContactDetails(v);
            IWebElement detailsData = driver.FindElement(By.XPath("//*[@id='content']"));
            return new ContactData()
            {
                DetailsData = detailsData.Text,
            };    
        }
    }
}
