using System;
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
        [Test]
        public void CreateNewGroupTest()
        {
            GroupData group = new GroupData("Group name")
            {
                Header = "Group header",
                Footer = "Group footer"
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
        [Test]
        public void CreateNewGroupWithEmptyFieldsTest()
        {
            GroupData group = new GroupData("")
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
