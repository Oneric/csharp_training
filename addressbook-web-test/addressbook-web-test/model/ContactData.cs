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
                    return detailsData;
                }
                else
                {
                    return
                        DetailFIOFields(Firstname) +
                        DetailFIOFields(Middlename) +
                        DetailFIOFields(Lastname).Trim() +
                        DetailFields(Nickname) +
                        DetailFields(Address) +
                        DetailPhonesFields(PhoneHome, PhoneMobile, PhoneWork, PhoneFax) +
                        DetailEmailsFields(Email, Email2, Email3) +
                        DetailBAFields("Birthday", Bday, Bmonth, Byear) +
                        DetailBAFields("Anniversary", Aday, Amonth, Ayear).Trim();
                }
            }
            set
            {
                detailsData = value;
            }
        }
        private string DayNorm(string value)
        {
            if (Convert.ToInt32(value) < 10)
            {
                return $"0{value}";
            }
            else
            {
                return value;
            }
        }
        private string DetailPhonesFields(string home, string mobile, string work, string fax)
        {
            string phones = "\r\n\r\n";
            if (home == null || home == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"H: { home }\r\n";
            } 
            if (mobile == null || mobile == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"M: { mobile }\r\n";
            }
            if (work == null || work == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"W: { work }\r\n";
            }
            if (fax == null || fax == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"F: { fax }";
            }
            if((home == null || home == "") && (mobile == null || mobile == "") && (work == null || work == "") && (fax == null || fax == ""))
            {
                return $"";
            }
            else
            {
                return phones;
            }
        }
        private string DetailEmailsFields(string email, string email2, string email3)
        {
            string emails = "\r\n\r\n";
            if (email == null || email == "")
            {
                emails += $"";
            }
            else
            {
                emails += $"{ email }\r\n";
            }
            if (email2 == null || email2 == "")
            {
                emails += $"";
            }
            else
            {
                emails += $"{ email2 }\r\n";
            }
            if (email3 == null || email3 == "")
            {
                emails += $"";
            }
            else
            {
                emails += $"{ email3 }";
            }
            if((email == null || email == "") && (email2 == null || email2 == "") && (email3 == null || email3 == ""))
            {
                return $"";
            }
            else
            {
                return emails;
            }
        }
        private string DetailFIOFields(string value)
        {
            if (value == null || value == "")
            {
                return $"";
            }
            else
            {
                return $"{ value } ";
            }
        }
        private string DetailBAFields(string name, string day, string month, string year)
        {
            string date = $"";
            if (day == null || day == "0")
            {
                date += $"".Trim();
            }
            else
            {
                date += $"{ day }.";
            }
            if (month == null || month == "-")
            {
                date += $"".Trim();
            }
            else
            {
                if(day == "0")
                {
                    date += $"{ month }";
                }
                else
                {
                    date += $" { month }";
                }
            }
            if (year == null || year == "")
            {
                date += $"".Trim();
            }
            else
            {
                if(month != "-" || day != "0")
                {
                    date += $" { year }";
                }
                else
                {
                    date += $"{ year }";
                }
            }
            if(year == null || year == "")
            {
                date += $"".Trim();
            }
            else if (day != "0" && month == "-")
            {
                date += $" ({ YearsDiff($"{ DayNorm(day) }. January { year }", name, true) })";
            }
            else if (day == "0" && month != "-")
            {
                date += $" ({ YearsDiff($"01. { month } { year }", name) })";
            }
            else if (day == "0" && month == "-")
            {
                date += $" ({ Convert.ToInt32(YearsDiff($"01. January { year }", name, true)) })";
            }
            else
            {
                date += $" ({ YearsDiff($"{ DayNorm(day) }. { month } { year }", name) })";
            }
            if (day == "0" && month == "-" && (year == "" || year == null))
            {
                return "".Trim();
            }
            else
            {
                return $"\r\n\r\n{ name } { date }\r\n";
            }
        }
        private string DetailFields(string value)
        {
            if (value == null || value == "")
            {
                return $"".Trim();
            }
            else
            {
                return $"\r\n{ value }";
            }
        }
        private string YearsDiff(string date, string type=null, bool forcefloor = false)
        {
            string now = DateTime.Now.ToString("dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            var dateNow = DateTime.ParseExact(now, "dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            var dateShift = DateTime.ParseExact(date, "dd. MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            var timeSpan = (dateNow - dateShift).Duration();
            double days = timeSpan.Days;

            if (type == "Birthday" || forcefloor == true)
            {
                return Math.Floor(days / 365).ToString();
            }
            else if (type == "Anniversary")
            {
                return Math.Ceiling(days / 365).ToString();
            }
            else
            {
                return null;
            }
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
