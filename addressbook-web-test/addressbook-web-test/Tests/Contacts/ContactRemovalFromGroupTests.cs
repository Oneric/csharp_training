using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class ContactRemovalFromGroupTests : ContactTestBase
    {
        [Test]
        public void ContactRemoveFromGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Intersect(oldList).First();

            app.Contacts.RemoveFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);

            /*foreach (ContactData c in newList)
            {
                Assert.AreNotEqual(c.Id, contact.Id);
            }*/
        }
    }
}
