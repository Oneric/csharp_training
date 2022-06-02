using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactAddingToGroupTests : ContactTestBase
    {
        [Test]
        public void AddContactToGroupTest()
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
            GroupData group = GroupData.GetAll().FirstOrDefault(gr => ContactData.GetAll().Except(gr.GetContacts()).Count() > 0);
            if (group == null)
            {
                app.Contacts.Create(new ContactData()
                {
                    Firstname = "NewFirstName",
                    Lastname = "NewLastName",
                    Middlename = "NewMiddleName"
                });
                group = GroupData.GetAll()[0];
            }
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
