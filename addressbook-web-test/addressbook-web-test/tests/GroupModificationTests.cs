using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData modyfiedData = new GroupData("Modyfied group name")
            {
                Header = null,
                Footer = null
            };

            if (!app.Groups.IsExistsGroup(0))
            {
                app.Groups.Create(new GroupData("New Group"));
            }

            List<GroupData> beforeTest = app.Groups.GetGroupList();

            app.Groups.Modify(0, modyfiedData);

            List<GroupData> afterTest = app.Groups.GetGroupList();
            beforeTest[0].Name = modyfiedData.Name;

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);
        }
    }
}
