using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData modyfiedData = new GroupData("Modyfied group name")
            {
                Header = null,
                Footer = null
            };
            if (!app.Groups.IsExistsGroup(1))
            {
                app.Groups.Create(new GroupData("New Group"));
            }
            app.Groups.Modify(1, modyfiedData);
        }
    }
}
