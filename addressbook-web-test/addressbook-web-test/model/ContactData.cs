using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string phoneAll = null;
        private string emailAll = null;
        private string detailsData = null;

        public ContactData() {}
        public ContactData(string lastname, string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;
        }
        public ContactData(string firstname, string middlename, string lastname )
        {
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
        }
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; } = null;
        public string Lastname { get; set; }
        public string Nickname { get; set; } = null;
        public string Photo { get; set; } = null;
        public string Title { get; set; } = null;
        public string Company { get; set; } = null;
        public string Address { get; set; } = null;
        public string PhoneHome { get; set; } = null;
        public string PhoneMobile { get; set; } = null;
        public string PhoneWork { get; set; } = null;
        public string PhoneFax { get; set; } = null;
        public string Email { get; set; } = null;
        public string Email2 { get; set; } = null;
        public string Email3 { get; set; } = null;
        public string Homepage { get; set; } = null;
        public string Bday { get; set; } = null;
        public string Bmonth { get; set; } = null;
        public string Byear { get; set; } = null;
        public string Aday { get; set; } = null;
        public string Amonth { get; set; } = null;
        public string Ayear { get; set; } = null;
        public string NewGroup { get; set; } = null;
        public string Address2 { get; set; } = null;
        public string Phone2 { get; set; } = null;
        public string Notes { get; set; } = null;
        public string EmailAll 
        {
            get
            {
                if (emailAll != null)
                {
                    return emailAll;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                emailAll = value;
            }
        }
        public string PhoneAll 
        {
            get
            {
                if(phoneAll != null)
                {
                    return phoneAll;
                }
                else
                {
                    return (CleanUp(PhoneHome) + CleanUp(PhoneMobile) + CleanUp(PhoneWork)).Trim();
                }
            }
            set
            {
                phoneAll = value;
            }
        }
        public string DetailsData
        {
            get
            {
                if (detailsData != null)
                {
                    return CleanUpDeteils(detailsData).Trim();
                }
                else
                {   
                    
                    string birthday = $"{ Bday }. {Bmonth} {Byear}";
                    string anniversary = $"{ Aday }. {Amonth} {Ayear}";
                    return
                        /* Для потомков
                        $"{ Firstname } { Middlename } { Lastname }\r\n" +
                        $"{ Nickname }\r\n" +
                        $"{ Address }\r\n\r\n" +
                        $"H: { PhoneHome }\r\n" +
                        $"M: { PhoneMobile }\r\n" +
                        $"W: { PhoneWork }\r\n" +
                        $"F: { PhoneFax }\r\n\r\n" +
                        $"{ Email }\r\n" +
                        $"{ Email2 }\r\n" +
                        $"{ Email3 }\r\n\r\n" +
                        $"Birthday { birthday } ({ YearsDiff(birthday, "birthday") })\r\n" +
                        $"Anniversary { anniversary } ({ YearsDiff(anniversary) })"
                        ;*/(
                        CleanUpDeteils(Firstname) +
                        CleanUpDeteils(Middlename) +
                        CleanUpDeteils(Lastname) +
                        CleanUpDeteils(Nickname) +
                        CleanUpDeteils(Address) +
                        CleanUpDeteils(PhoneHome) +
                        CleanUpDeteils(PhoneMobile) +
                        CleanUpDeteils(PhoneWork) +
                        CleanUpDeteils(PhoneFax) +
                        CleanUpDeteils(Email) +
                        CleanUpDeteils(Email2) +
                        CleanUpDeteils(Email3) +
                        CleanUpDeteils($"Birthday { birthday } ({ YearsDiff(birthday, "birthday") })") +
                        CleanUpDeteils($"Anniversary { anniversary } ({ YearsDiff(anniversary) })")).Trim();
                }
            }
            set
            {
                detailsData = value;
            }
        }
        private string YearsDiff(string date, string type=null)
        {
            if(date != null || date != ".  ")
            {
                string Now = DateTime.Now.ToString("dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                var dateNow = DateTime.ParseExact(Now, "dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                var dateShift = DateTime.ParseExact(date, "dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                var timeSpan = (dateNow - dateShift).Duration();
                double days = timeSpan.Days;
                if (type == "birthday")
                {
                    return Math.Floor(days / 365).ToString();
                }
                else
                {
                    return Math.Ceiling(days / 365).ToString();
                }
            }
            return null;
        }
        private string CleanUpDeteils(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            return Regex.Replace(value, "[\\^H:$\\^M:$\\^W:$\\^F:$ \\r\\n]", "");
        }
        private string CleanUp(string value)
        {
            if(value == null || value == "")
            {
                return "";
            }
            return  Regex.Replace(value, "[ ()-]", "") + "\r\n";
        }

        public bool Equals(ContactData other)
        {
            // dotnet_style_prefer_is_null_check_over_reference_equality_method = false
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Firstname == other.Firstname 
                && this.Lastname == other.Lastname;
        }
        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }
        public override string ToString()
        {
            return $"\n" +
                   $"Firstname = { Firstname }" +
                   $"\n" +
                   $"Middlename = { Middlename }" +
                   $"\n" +
                   $"Lastname = { Lastname }"
                   ;
        }
        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname) != 0 ? Firstname.CompareTo(other.Firstname) : Lastname.CompareTo(other.Lastname);
        }
    }
}
