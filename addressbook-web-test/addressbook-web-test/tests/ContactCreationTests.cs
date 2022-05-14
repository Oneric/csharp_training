using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(15), GenerateRandomString(15), GenerateRandomString(15))
                {
                    Nickname = GenerateRandomString(15),
                    Address = GenerateRandomString(30),
                    Email = $"{GenerateRandomString(5)}@{GenerateRandomString(5)}.{GenerateRandomString(2)}"
                });
            }
            return contacts;
        }
        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> beforeTest = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(beforeTest.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> afterTest = app.Contacts.GetContactList();
            beforeTest.Add(contact);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
        [Test]
        public void ContactCreationWithoutAniversaryTest()
        {
            ContactData contact = new ContactData("Тест1", "Тестович1", "Тестов1")
            {
                Nickname = "WithoutAniversaryContact",
                Email = "test@test.ru",
                PhoneMobile = "7 (852) 751-25-15",
                Address = "Moscow, st.Mira 25, ap. 12",
                Bday = "14",
                Bmonth = "July",
                Byear = "1988",
            };
            List<ContactData> beforeTest = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(beforeTest.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> afterTest = app.Contacts.GetContactList();
            beforeTest.Add(contact);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
        [Test]
        public void ContactCreationWithoBadFieldsDataTest()
        {
            ContactData contact = new ContactData("Тест'", "Тестович'", "Тестов'")
            {
                Nickname = "'Contact",
                Email = "test'@test'.ru",
                PhoneMobile = "7 (852) 751-25-15'",
                Address = "Moscow', st.Mira 25, ap. 12",
                Bday = "14",
                Bmonth = "July",
                Byear = "1988",
            };
            List<ContactData> beforeTest = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(beforeTest.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> afterTest = app.Contacts.GetContactList();
            beforeTest.Add(contact);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
    }
}
