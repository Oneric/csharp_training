using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(15))
                {
                    Header = GenerateRandomString(30),
                    Footer = GenerateRandomString(30)
                });
            }
            return groups;
        }
        public static IEnumerable<GroupData> ReadGroupDataFromCSVFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string line in lines)
            {
                string[] colls = line.Split(',');
                groups.Add(new GroupData(colls[0])
                {
                    Header = colls[1],
                    Footer = colls[2]
                });
            }
            return groups;
        }
        public static IEnumerable<GroupData> ReadGroupDataFromXMLFile()
        {
            return (List<GroupData>) new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(@"groups.xml"));
        }
        public static IEnumerable<GroupData> ReadGroupDataFromJSONFile()
        {

            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));
        }
        public static IEnumerable<GroupData> ReadGroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;

            Excel.Range range = sheet.UsedRange;
            for (int i = 2; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Quit();

            return groups;
        }
        [Test, TestCaseSource("ReadGroupDataFromExcelFile")]
        public void CreateNewGroupTest(GroupData group)
        {
            List<GroupData> beforeTest = GroupData.GetAll();

            app.Groups.Create(group);

            Assert.AreEqual(beforeTest.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> afterTest = GroupData.GetAll();
            beforeTest.Add(group);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
        [Test]
        public void CreateNewGroupWithValidData()
        {
            GroupData group = new GroupData("Test group name")
            {
                Header = "Test group header",
                Footer = "Test group footer"
            };
            List<GroupData> beforeTest = GroupData.GetAll();

            app.Groups.Create(group);

            Assert.AreEqual(beforeTest.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> afterTest = GroupData.GetAll();
            beforeTest.Add(group);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
        [Test]
        public void TestDbConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            DateTime end = DateTime.Now;
            double fUiTime = end.Subtract(start).TotalMilliseconds;
            Console.Out.WriteLine($"fromUi: {fUiTime}");

            start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList();
            end = DateTime.Now;
            double fDbTime = end.Subtract(start).TotalMilliseconds;
            Console.Out.WriteLine($"fromDb: {fDbTime}");
            if(fDbTime < fUiTime)
            {
                Console.Out.WriteLine($"Database faster");
            }
            else
            {
                Console.Out.WriteLine($"Selenium faster");
            }
        }
        [Test]
        public void TestGCR()
        {
            foreach(ContactData contact in GroupData.GetAll()[0].GetContacts())
            {
                Console.Out.WriteLine(contact);
            }
        }
    }
}
