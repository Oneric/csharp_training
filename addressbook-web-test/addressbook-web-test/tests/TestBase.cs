using System;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            AccountData account = new AccountData("admin", "secret");

            app = new ApplicationManager();
            app.Navigation.GoToHomePage();
            app.Auth.Login(account);
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Auth.Logout();
            app.Stop();
            
        }
    }
}
