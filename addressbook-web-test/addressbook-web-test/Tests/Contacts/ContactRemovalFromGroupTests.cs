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
            if (GroupData.IsEmptyList())
            {
                app.Groups.Create(new GroupData("New group"));
            }
            if (ContactData.IsEmptyList())
            {
                app.Contacts.Create(new ContactData()
                {
                    Firstname = "NewFirstName",
                    Lastname = "NewLastName",
                    Middlename = "NewMiddleName"
                });
            }
            GroupData group = GroupData.GetAll().FirstOrDefault(gr => gr.GetContacts().Count() > 0);
            if (group == null)
            {
                group =  GroupData.GetAll().First();
                app.Contacts.AddContactToGroup(ContactData.GetAll().First(), group);
            }
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Intersect(oldList).First();

            app.Contacts.RemoveFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);

            foreach (ContactData c in newList)
            {
                Assert.AreNotEqual(c.Id, contact.Id);
            }
        }
    }
}
