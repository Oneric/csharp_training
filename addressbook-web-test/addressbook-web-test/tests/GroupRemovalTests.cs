using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void RemoveGroupTest()
        {
            // Переходим на страницу групп
            app.Navigation.GoToGroupsPage();

            if (!app.Groups.IsExistsGroup(1))
            {
                app.Groups.Create(new GroupData("Removing group"));
            }

            app.Groups.Remove(1);
        }
    }
}


