using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        [SetUp]
        public void GoToGroupHomePageSetUp()
        {
            app.Navigation.GoToHomePage();
        }
        [TearDown]
        public void CompareContactListUiToDb()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUi = app.Contacts.GetContactList();
                List<ContactData> fromDb = ContactData.GetAll();
                fromUi.Sort();
                fromDb.Sort();

                Assert.AreEqual(fromUi, fromDb);
            }
        }
    }
}
