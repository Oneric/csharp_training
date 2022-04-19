using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();
            ContactData contact = new ContactData("Тест", "Тестович", "Тестов");
            contact.Nickname = "TestContact";
            contact.Email = "test@test.ru";
            contact.PhoneMobile = "7 (852) 751-25-15";
            contact.Address = "Moscow, st.Mira 25, ap. 12";
            contact.Bday = "14";
            contact.Bmonth = "July";
            contact.Byear = "1988";
            contact.Bday = "14";
            contact.Bmonth = "July";
            contact.Byear = "2018";
            FillContactCreationForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            Logout();
        }
    }
}
