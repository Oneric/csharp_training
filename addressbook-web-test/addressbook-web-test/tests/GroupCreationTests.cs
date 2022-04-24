using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void CreateNewGroupTest()
        {
            GroupData group = new GroupData("Group name")
            {
                Header = "Group header",
                Footer = "Group footer"
            };

            app.Groups.Create(group);

        }
        [Test]
        public void CreateNewGroupWithEmptyFieldsTest()
        {
            GroupData group = new GroupData("")
            {
                Header = "",
                Footer = ""
            };

            app.Groups.Create(group);
        }
    }
}
