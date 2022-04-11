using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class ContactData
    {
        private string firstname;
        private string middlename;
        private string lastname;
        private string nickname;
        private string photo = null;
        private string title = "";
        private string company = "";
        private string address = "";
        private string phoneHome = "";
        private string phoneMobile = "";
        private string phoneWork = "";
        private string phoneFax = "";
        private string email = "";
        private string email2 = "";
        private string email3 = "";
        private string homepage = "";
        private string bday = "";
        private string bmonth = "-";
        private string byear = "";
        private string aday = "";
        private string amonth = "-";
        private string ayear = "";
        private string newGroup = "[none]";
        private string address2 = "";
        private string phone2 = "";
        private string notes = "";

        public ContactData(string firstname, string middlename, string lastname )
        {
            this.firstname = firstname;
            this.middlename = middlename;
            this.lastname = lastname;
        }

        public string Firstname { get { return firstname; } set { firstname = value; } }
        public string Middlename { get { return middlename; } set { middlename = value; } }
        public string Lastname { get { return lastname; } set { lastname = value; } }
        public string Nickname { get { return nickname; } set { nickname = value; } }
        public string Photo { get { return photo; } set { photo = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Company { get { return company; } set { company = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string PhoneHome { get { return phoneHome; } set { phoneHome = value; } }
        public string PhoneMobile { get { return phoneMobile; } set { phoneMobile = value; } }
        public string PhoneWork { get { return phoneWork; } set { phoneWork = value; } }
        public string PhoneFax { get { return phoneFax; } set { phoneFax = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Email2 { get { return email2; } set { email2 = value; } }
        public string Email3 { get { return email3; } set { email3 = value; } }
        public string Homepage { get { return homepage; } set { homepage = value; } }
        public string Bday { get { return bday; } set { bday = value; } }
        public string Bmonth { get { return bmonth; } set { bmonth = value; } }
        public string Byear { get { return byear; } set { byear = value; } }
        public string Aday { get { return aday; } set { aday = value; } }
        public string Amonth { get { return amonth; } set { amonth = value; } }
        public string Ayear { get { return ayear; } set { ayear = value; } }
        public string NewGroup { get { return newGroup; } set { newGroup = value; } }
        public string Address2 { get { return address2; } set { address2 = value; } }
        public string Phone2 { get { return phone2; } set { phone2 = value; } }
        public string Notes { get { return notes; } set { notes = value; } }

    }
}
