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
            ContactData contact = new ContactData("Тест", "Тестович", "Тестов")
            {
                Nickname = "TestContact",
                Email = "test@test.ru",
                PhoneMobile = "7 (852) 751-25-15",
                Address = "Moscow, st.Mira 25, ap. 12",
                Bday = "14",
                Bmonth = "July",
                Byear = "1988",
                Aday = "14",
                Amonth = "July",
                Ayear = "2018",
            };
            app.Contacts
                .InitNewContactCreation()
                .FillContactCreationForm(contact)
                .SubmitContactCreation()
                .ReturnToHomePage();
        }
    }
}
