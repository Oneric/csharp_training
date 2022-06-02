using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
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
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column(Name = "firstname")]
        public string Firstname { get; set; }
        [Column(Name = "middlename")]
        public string Middlename { get; set; } = null;
        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        [Column(Name = "nickname")]
        public string Nickname { get; set; } = null;
        [Column(Name = "photo")]
        public string Photo { get; set; } = null;
        [Column(Name = "title")]
        public string Title { get; set; } = null;
        [Column(Name = "company")]
        public string Company { get; set; } = null;
        [Column(Name = "address")]
        public string Address { get; set; } = null;
        [Column(Name = "home")]
        public string PhoneHome { get; set; } = null;
        [Column(Name = "mobile")]
        public string PhoneMobile { get; set; } = null;
        [Column(Name = "work")]
        public string PhoneWork { get; set; } = null;
        [Column(Name = "fax")]
        public string PhoneFax { get; set; } = null;
        [Column(Name = "email")]
        public string Email { get; set; } = null;
        [Column(Name = "email2")]
        public string Email2 { get; set; } = null;
        [Column(Name = "email3")]
        public string Email3 { get; set; } = null;
        [Column(Name = "homepage")]
        public string Homepage { get; set; } = null;
        [Column(Name = "bday")]
        public string Bday { get; set; } = null;
        [Column(Name = "bmonth")]
        public string Bmonth { get; set; } = null;
        [Column(Name = "byear")]
        public string Byear { get; set; } = null;
        [Column(Name = "aday")]
        public string Aday { get; set; } = null;
        [Column(Name = "amonth")]
        public string Amonth { get; set; } = null;
        [Column(Name = "ayear")]
        public string Ayear { get; set; } = null;
        public string NewGroup { get; set; } = null;
        [Column(Name = "address2")]
        public string Address2 { get; set; } = null;
        [Column(Name = "phone2")]
        public string Phone2 { get; set; } = null;
        [Column(Name = "notes")]
        public string Notes { get; set; } = null;
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; } = null;
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
                        DetailFullNameBlock(Firstname, Middlename, Lastname) +
                        DetailFields(DetailFullNameBlock(Firstname, Middlename, Lastname), Nickname) +
                        DetailFields(Nickname, Address) +
                        DetailPhonesBlock(PhoneHome, PhoneMobile, PhoneWork, PhoneFax) +
                        DetailEmailsBlock(Email, Email2, Email3) +
                        DetailBirthDayAnniversaryBlock(
                            DetailDateProcessing("Birthday", Bday, Bmonth, Byear),
                            DetailDateProcessing("Anniversary", Aday, Amonth, Ayear));
                }
            }
            set
            {
                detailsData = value;
            }
        }
        private string DetailFields(string beforefield, string value)
        {

            if (value == null || value == "")
            {
                return $"";
            }
            else
            {
                if (beforefield == null || beforefield == "")
                {
                    return $"{ value }";
                }
                else
                {
                    return $"\r\n{ value }";
                }

            }
        }
        private string DetailFullNameBlock(string firstname, string middlename, string lastname)
        {
            string fullname = $"";

            if (firstname == null || firstname == "")
            {
                fullname += $"";
            }
            else
            {
                fullname += $"{ firstname }";
            }

            if (middlename == null || middlename == "")
            {
                fullname += $"";
            }
            else
            {
                if (firstname == null || firstname == "")
                {
                    fullname += $"{ middlename }";
                }
                else
                {
                    fullname += $" { middlename }";
                }
            }

            if (lastname == null || lastname == "")
            {
                fullname += $"";
            }
            else
            {
                if ((firstname == null || firstname == "") && (middlename == null || middlename == ""))
                {
                    fullname += $"{ lastname }";
                }
                else
                {
                    fullname += $" { lastname }";
                }
            }
            return fullname;
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
        private string DetailPhonesBlock(string home, string mobile, string work, string fax)
        {
            string phones = "\r\n";
            if (home == null || home == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"\r\nH: { home }";
            } 
            if (mobile == null || mobile == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"\r\nM: { mobile }";
            }
            if (work == null || work == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"\r\nW: { work }";
            }
            if (fax == null || fax == "")
            {
                phones += $"";
            }
            else
            {
                phones += $"\r\nF: { fax }";
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
        private string DetailEmailsBlock(string email, string email2, string email3)
        {
            string emails = "\r\n";
            if (email == null || email == "")
            {
                emails += $"";
            }
            else
            {
                emails += $"\r\n{ email }";
            }
            if (email2 == null || email2 == "")
            {
                emails += $"";
            }
            else
            {
                emails += $"\r\n{ email2 }";
            }
            if (email3 == null || email3 == "")
            {
                emails += $"";
            }
            else
            {
                emails += $"\r\n{ email3 }";
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

        private string DetailDateProcessing(string name, string day, string month, string year)
        {
            string date = $"";
            if (day == null || day == "0")
            {
                date += $"";
            }
            else
            {
                date += $"{ day }.";
            }
            if (month == null || month == "-")
            {
                date += $"";
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
                date += $"";
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
                date += $"";
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
                return $"";
            }
            else
            {
                return $"\r\n{ name } { date }";
            }
        }
        private string DetailBirthDayAnniversaryBlock(string birthday, string annyversary)
        {
            string block = $"\r\n";
            if(birthday == "" && annyversary == "")
            {
                block = $"";
            }
            else
            {
                if (birthday == "")
                {
                    block += $"";
                }
                else
                {
                    block += birthday;
                }
                if (annyversary == "")
                {
                    block += $"";
                }
                else
                {
                    block += annyversary;
                }
            }
            return block;
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
        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
        public static bool IsEmptyList()
        {
            if (GetAll().Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
