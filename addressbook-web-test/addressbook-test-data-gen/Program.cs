using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.Text.RegularExpressions;

namespace addressbook_test_data_gen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string outputFile = args[1];
            string dataType = args[2];
            string format = Regex.Split(outputFile, "^.*\\.(xml|json|csv|xls|xlsx)$")[1];
            if(dataType.ToLower() == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();
                    for( int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        Firstname = TestBase.GenerateRandomString(30),
                        Middlename = TestBase.GenerateRandomString(30),
                        Lastname = TestBase.GenerateRandomString(30),
                        Nickname = TestBase.GenerateRandomString(30),
                        Address = TestBase.GenerateRandomString(30),
                        PhoneHome = TestBase.GenerateRandomString(10),
                        PhoneMobile = TestBase.GenerateRandomString(10),
                        PhoneWork = TestBase.GenerateRandomString(10),
                        PhoneFax = TestBase.GenerateRandomString(10),
                        Email = $"{TestBase.GenerateRandomString(5)}@{TestBase.GenerateRandomString(5)}.ru",
                        Email2 = $"{TestBase.GenerateRandomString(5)}@{TestBase.GenerateRandomString(5)}.ru",
                        Email3 = $"{TestBase.GenerateRandomString(5)}@{TestBase.GenerateRandomString(5)}.ru",
                    });
                }
                if (format.ToLower() == "xls" || format.ToLower() == "xlsx")
                {
                    WriteContactsToFileXLS(contacts, outputFile);
                }
                else
                {
                    StreamWriter stream = new StreamWriter(outputFile);
                    if (format.ToLower() == "csv")
                    {
                        WriteContactsToFileCSV(contacts, stream);
                    }
                    else if (format.ToLower() == "xml")
                    {
                        WriteContactsToFileXML(contacts, stream);
                    }
                    else if (format.ToLower() == "json")
                    {
                        WriteContactsToFileJSON(contacts, stream);
                    }
                    else
                    {
                        Console.Out.WriteLine($"Unrecognized format {format}");
                    }
                    stream.Close();
                }
            } else if(dataType.ToLower() == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(30))
                    {
                        Header = TestBase.GenerateRandomString(50),
                        Footer = TestBase.GenerateRandomString(50)
                    });
                }
                if (format.ToLower() == "xls" || format.ToLower() == "xlsx")
                {
                    WriteGroupsToFileXLS(groups, outputFile);
                }
                else
                {
                    StreamWriter stream = new StreamWriter(outputFile);
                    if (format.ToLower() == "csv")
                    {
                        WriteGroupsToFileCSV(groups, stream);
                    }
                    else if (format.ToLower() == "xml")
                    {
                        WriteGroupsToFileXML(groups, stream);
                    }
                    else if (format.ToLower() == "json")
                    {
                        WriteGroupsToFileJSON(groups, stream);
                    }
                    else
                    {
                        Console.Out.WriteLine($"Unrecognized format {format}");
                    }
                    stream.Close();
                }
            }
        }

        static void WriteGroupsToFileXLS(List<GroupData> groups, string outputFile)
        {
            try
            {
                Excel.Application app = new Excel.Application();
                app.Visible = true;
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet sheet = wb.ActiveSheet;

                sheet.Cells[1, 1] = "Name";
                sheet.Cells[1, 2] = "Header";
                sheet.Cells[1, 3] = "Footer";

                int row = 2;
                foreach (GroupData group in groups)
                {
                    sheet.Cells[row, 1] = group.Name;
                    sheet.Cells[row, 2] = group.Header;
                    sheet.Cells[row, 3] = group.Footer;
                    
                    row++;
                }

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), outputFile);
                if (File.Exists(fullPath))
                {
                    
                    Console.Out.WriteLine($"Delete File on: { fullPath }");
                    File.Delete(fullPath);
                    Console.Out.WriteLine($"Save File to: { fullPath }");
                    wb.SaveAs(fullPath);
                    wb.Close();
                    app.Quit();
                }
                else
                {
                    Console.Out.WriteLine($"Save File to: { fullPath }");
                    wb.SaveAs(fullPath);
                    wb.Close();
                    app.Quit();
                }
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: " +
                    $"\r\nMessage:" +
                    $"\r\n{ex.Message}" +
                    $"\r\nTrace:" +
                    $"\r\n{ex.StackTrace}");
            }
        }
        static void WriteGroupsToFileCSV(List<GroupData> groups, StreamWriter stream)
        {
            try
            {
                foreach (GroupData group in groups)
                {
                    stream.WriteLine($"{group.Name},{group.Header},{group.Footer}");
                }
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: \r\n{ex.Message}");
            }

        }
        static void WriteGroupsToFileXML(List<GroupData> groups, StreamWriter stream)
        {
            try
            {
                new XmlSerializer(typeof(List<GroupData>)).Serialize(stream, groups);
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: \r\n{ex.Message}");
            }
        }
        static void WriteGroupsToFileJSON(List<GroupData> groups, StreamWriter stream)
        {
            try
            {
                stream.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: \r\n{ex.Message}");
            }
        }
        static void WriteContactsToFileXLS(List<ContactData> contacts, string outputFile)
        {
            try
            {
                Excel.Application app = new Excel.Application();
                app.Visible = true;
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet sheet = wb.ActiveSheet;
                sheet.Cells[1, 1] = "Firstname";
                sheet.Cells[1, 2] = "Middlename";
                sheet.Cells[1, 3] = "Lastname";
                sheet.Cells[1, 4] = "Nickname";
                sheet.Cells[1, 5] = "Address";
                sheet.Cells[1, 6] = "PhoneHome";
                sheet.Cells[1, 7] = "PhoneMobile";
                sheet.Cells[1, 8] = "PhoneWork";
                sheet.Cells[1, 9] = "PhoneFax";
                sheet.Cells[1, 10] = "Email";
                sheet.Cells[1, 11] = "Email2";
                sheet.Cells[1, 12] = "Email3";

                int row = 2;
                foreach (ContactData contact in contacts)
                {
                    sheet.Cells[row, 1] = contact.Firstname;
                    sheet.Cells[row, 2] = contact.Middlename;
                    sheet.Cells[row, 3] = contact.Lastname;
                    sheet.Cells[row, 4] = contact.Nickname;
                    sheet.Cells[row, 5] = contact.Address;
                    sheet.Cells[row, 6] = contact.PhoneHome;
                    sheet.Cells[row, 7] = contact.PhoneMobile;
                    sheet.Cells[row, 8] = contact.PhoneWork;
                    sheet.Cells[row, 9] = contact.PhoneFax;
                    sheet.Cells[row, 10] = contact.Email;
                    sheet.Cells[row, 11] = contact.Email2;
                    sheet.Cells[row, 12] = contact.Email3;

                    row++;
                }

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), outputFile);
                if (File.Exists(fullPath))
                {

                    Console.Out.WriteLine($"Delete File on: { fullPath }");
                    File.Delete(fullPath);
                    Console.Out.WriteLine($"Save File to: { fullPath }");
                    wb.SaveAs(fullPath);
                    wb.Close();
                    app.Quit();
                }
                else
                {
                    Console.Out.WriteLine($"Save File to: { fullPath }");
                    wb.SaveAs(fullPath);
                    wb.Close();
                    app.Quit();
                }
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: " +
                    $"\r\nMessage:" +
                    $"\r\n{ex.Message}" +
                    $"\r\nTrace:" +
                    $"\r\n{ex.StackTrace}");
            }
        }
        static void WriteContactsToFileCSV(List<ContactData> contacts, StreamWriter stream)
        {
            try
            {
                foreach (ContactData contact in contacts)
                {
                    stream.WriteLine($"{contact.Firstname}," +
                        $"{contact.Middlename}," +
                        $"{contact.Lastname}," +
                        $"{contact.Nickname}," +
                        $"{contact.Address}," +
                        $"{contact.PhoneHome}," +
                        $"{contact.PhoneMobile}," +
                        $"{contact.PhoneWork}," +
                        $"{contact.PhoneFax}," +
                        $"{contact.Email}," +
                        $"{contact.Email2}," +
                        $"{contact.Email3}");
                }
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: \r\n{ex.Message}");
            }

        }
        static void WriteContactsToFileXML(List<ContactData> contacts, StreamWriter stream)
        {
            try
            {
                new XmlSerializer(typeof(List<ContactData>)).Serialize(stream, contacts);
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: \r\n{ex.Message}");
            }
        }
        static void WriteContactsToFileJSON(List<ContactData> contacts, StreamWriter stream)
        {
            try
            {
                stream.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
                Console.Out.WriteLine($"Файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"При создании файла произошла ошибка: \r\n{ex.Message}");
            }
        }
    }
}
