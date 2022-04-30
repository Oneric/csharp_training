using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [SetUp]
        public void GoToGroupPageSetUp()
        {
            app.Navigation.GoToGroupsPage();
        }
    }
}
