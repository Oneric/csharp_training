using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData modyfiedContact = new ContactData("Обновлен", "Тестович", "Тестов")
            {
                Nickname = "Modyfied",
                Email = "Modyfied@test.ru",
                PhoneMobile = "7 (852) 751-25-16",
                Address = "Moscow, st.Mira 21, ap. 15",
                Bday = "15",
                Bmonth = "July",
                Byear = "1989",
                Aday = "-",
                Amonth = "-",
                Ayear = "",
            };
            if (!app.Contacts.IsExistsContact(0))
            {
                app.Contacts.Create(new ContactData("New1", "Never1", "Newerr1"));
            }

            List<ContactData> beforeTest = app.Contacts.GetContactList();

            app.Contacts.Modify(0, modyfiedContact);

            List<ContactData> afterTest = app.Contacts.GetContactList();

            beforeTest[0].Firstname = modyfiedContact.Firstname;
            beforeTest[0].Lastname = modyfiedContact.Lastname;
            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
    }
}
