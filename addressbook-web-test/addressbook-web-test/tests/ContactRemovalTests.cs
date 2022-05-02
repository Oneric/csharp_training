using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacts.IsExistsContact(0))
            {
                app.Contacts.Create(new ContactData("New", "For", "Delete"));
            }

            List<ContactData> beforeTest = app.Contacts.GetContactList();

            app.Contacts.Remove(0);
            Thread.Sleep(3000);

            List<ContactData> afterTest = app.Contacts.GetContactList();

            beforeTest.RemoveAt(0);

            Assert.AreEqual(beforeTest, afterTest);
        }
    }
}
