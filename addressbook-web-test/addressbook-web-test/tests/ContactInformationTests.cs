using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : ContactTestBase
    {
        [Test]
        public void ContactMainPageInfoTest()
        {
            ContactData contactTableData = app.Contacts.GetContactDataFromTable(0);
            ContactData contactFormData = app.Contacts.GetContactDataFromEditForm(0);

            //verifications
            Assert.AreEqual(contactTableData, contactFormData);
            Assert.AreEqual(contactTableData.Address, contactFormData.Address);
            Assert.AreEqual(contactTableData.EmailAll, contactFormData.EmailAll);
            Assert.AreEqual(contactTableData.PhoneAll, contactFormData.PhoneAll);
        }
        [Test]
        public void ContactDetailsInfoTest()
        {
            ContactData contactDetailsData = app.Contacts.GetContactDataFromDetailsPage(0);
            app.Navigation.GoToHomePage();
            ContactData contactFormData = app.Contacts.GetContactDataFromEditForm(0);

            //verifications
            Assert.AreEqual(contactDetailsData.DetailsData, contactFormData.DetailsData);
        }
    }
}
