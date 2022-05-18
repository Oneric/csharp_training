using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;

namespace addressbook_test_data_gen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter stream = new StreamWriter(args[1]);
            for(int i = 0; i < count; i++)
            {
                stream.WriteLine($"{TestBase.GenerateRandomString(10)},{TestBase.GenerateRandomString(30)},{TestBase.GenerateRandomString(30)}");
            }
            stream.Close();
        }
    }
}
