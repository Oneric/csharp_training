using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
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
        [Test, TestCaseSource("ReadGroupDataFromJSONFile")]
        public void CreateNewGroupTest(GroupData group)
        {
            List<GroupData> beforeTest = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(beforeTest.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> afterTest = app.Groups.GetGroupList();
            beforeTest.Add(group);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
        [Test]
        public void CreateNewGroupWithBadNameTest()
        {
            GroupData group = new GroupData("a'a")
            {
                Header = "",
                Footer = ""
            };
            List<GroupData> beforeTest = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(beforeTest.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> afterTest = app.Groups.GetGroupList();
            beforeTest.Add(group);

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
    }
}
