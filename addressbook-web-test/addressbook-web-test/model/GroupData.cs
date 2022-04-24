﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData
    {
        private string name;
        private string header = null;
        private string footer = null;

        public GroupData(string name)
        {
            this.name = name;
        }
        public string Name { get { return name; } set { name = value; } }
        public string Header { get { return header; } set { header = value; } }
        public string Footer {get { return footer; } set { footer = value; } }
    }
}
