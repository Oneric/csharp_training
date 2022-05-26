using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AuthTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //prepare
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Logout();

            //action
            app.Auth.Login(account);

            //verification
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }
        [Test]
        public void LoginWithInvalidCredentials()
        {
            //prepare
            AccountData account = new AccountData("user", "password");
            app.Auth.Logout();
            
            //action
            app.Auth.Login(account);

            //verification
            Assert.IsFalse(app.Auth.IsLoggedIn(account));

        }
    }
}
