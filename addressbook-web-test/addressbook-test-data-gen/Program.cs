﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
            string output = args[1];
            string format = Regex.Split(output, "^.*(xml|json|csv)$")[1];
            StreamWriter stream = new StreamWriter(output);
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(30))
                {
                    Header = TestBase.GenerateRandomString(50),
                    Footer = TestBase.GenerateRandomString(50)
                });
            }
            if (format.ToLower() == "csv")
            {
                WriteGroupsToFileCSV(groups, stream);
            }
            else if(format.ToLower() == "xml")
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
        static void WriteGroupsToFileCSV(List<GroupData> groups, StreamWriter stream)
        {
            foreach (GroupData group in groups)
            {
                stream.WriteLine($"{group.Name},{group.Header},{group.Footer}");
            }
        }
        static void WriteGroupsToFileXML(List<GroupData> groups, StreamWriter stream)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(stream, groups);
        }
        static void WriteGroupsToFileJSON(List<GroupData> groups, StreamWriter stream)
        {
            stream.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
