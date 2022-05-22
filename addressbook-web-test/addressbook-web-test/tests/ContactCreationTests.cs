using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
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
        public static IEnumerable<ContactData> ReadContactDataFromCSVFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string line in lines)
            {
                string[] colls = line.Split(',');
                contacts.Add(new ContactData()
                {
                    Firstname = colls[0],
                    Middlename = colls[1],
                    Lastname = colls[2],
                    Nickname = colls[3],
                    Address = colls[4],
                    PhoneHome = colls[5],
                    PhoneMobile = colls[6],
                    PhoneWork = colls[7],
                    PhoneFax = colls[8],
                    Email = colls[9],
                    Email2 = colls[10],
                    Email3 = colls[11],
                });
            }
            return contacts;
        }
        public static IEnumerable<ContactData> ReadContactDataFromXMLFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }
        public static IEnumerable<ContactData> ReadContactDataFromJSONFile()
        {

            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }
        public static IEnumerable<ContactData> ReadContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;

            Excel.Range range = sheet.UsedRange;
            for (int i = 2; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = range.Cells[i, 1].Value,
                    Middlename = range.Cells[i, 2].Value,
                    Lastname = range.Cells[i, 3].Value,
                    Nickname = range.Cells[i, 4].Value,
                    Address = range.Cells[i, 5].Value,
                    PhoneHome = range.Cells[i, 6].Value,
                    PhoneMobile = range.Cells[i, 7].Value,
                    PhoneWork = range.Cells[i, 8].Value,
                    PhoneFax = range.Cells[i, 9].Value,
                    Email = range.Cells[i, 10].Value,
                    Email2 = range.Cells[i, 11].Value,
                    Email3 = range.Cells[i, 12].Value,
                });
            }
            wb.Close();
            app.Quit();

            return contacts;
        }
        [Test, TestCaseSource("ReadContactDataFromJSONFile")]
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
