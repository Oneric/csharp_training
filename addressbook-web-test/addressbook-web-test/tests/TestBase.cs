using System;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }
        public static Random random = new Random();
        public static string GenerateRandomString(int max)
        {
            
            int l = Convert.ToInt32(random.NextDouble() * max);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                  sb.Append(Convert.ToChar(32 + Convert.ToInt32(random.NextDouble() * 223)));
            }
            return sb.ToString();
        }
    }
}
