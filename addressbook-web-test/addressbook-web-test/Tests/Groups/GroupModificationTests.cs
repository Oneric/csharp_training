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
            GroupData modifiedData = new GroupData("Modyfied group name")
            {
                Header = null,
                Footer = null
            };

            if (!app.Groups.IsExistsGroup(0))
            {
                app.Groups.Create(new GroupData("New Group"));
            }

            List<GroupData> beforeTest = GroupData.GetAll();
            GroupData toBeModified = beforeTest[0];

            app.Groups.Modify(toBeModified, modifiedData);

            Assert.AreEqual(beforeTest.Count, app.Groups.GetGroupCount());

            List<GroupData> afterTest = GroupData.GetAll();
            beforeTest[0].Name = modifiedData.Name;

            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);

            foreach(GroupData group in afterTest)
            {
                if(group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(modifiedData.Name, group.Name);
                }
            }
        }
    }
}
