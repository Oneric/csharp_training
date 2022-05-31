using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void RemoveGroupTest()
        {
            if (!app.Groups.IsExistsGroup(0))
            {
                app.Groups.Create(new GroupData("Removing group"));
            }

            List<GroupData> beforeTest = GroupData.GetAll();
            GroupData toBeRemoved = beforeTest[0];

            app.Groups.Remove(toBeRemoved);

            Assert.AreEqual(beforeTest.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> afterTest = GroupData.GetAll();

            beforeTest.RemoveAt(0);
            beforeTest.Sort();
            afterTest.Sort();

            Assert.AreEqual(beforeTest, afterTest);

            foreach (GroupData group in afterTest)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}


