using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        public void SetupAuth()
        {
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
        }
    }
}
