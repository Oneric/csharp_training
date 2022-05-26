using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData modifiedContact = new ContactData("Обновлен", "Тестович", "Тестов")
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

            List<ContactData> beforeTest = ContactData.GetAll();
            ContactData toBeModified = beforeTest[0];

            app.Contacts.Modify(toBeModified, modifiedContact);

            Assert.AreEqual(beforeTest.Count, app.Contacts.GetContactCount());

            List<ContactData> afterTest = ContactData.GetAll();

            beforeTest[0].Firstname = modifiedContact.Firstname;
            beforeTest[0].Lastname = modifiedContact.Lastname;
            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
            foreach (ContactData contact in afterTest)
            {
                if(contact.Id == toBeModified.Id)
                {
                    Assert.AreEqual(modifiedContact.Firstname, contact.Firstname); 
                    Assert.AreEqual(modifiedContact.Lastname, contact.Lastname);
                }
            }
        }
    }
}
