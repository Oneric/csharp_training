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

            List<GroupData> beforeTest = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            List<GroupData> afterTest = app.Groups.GetGroupList();

            beforeTest.RemoveAt(0);

            Assert.AreEqual(beforeTest, afterTest);
        }
    }
}


