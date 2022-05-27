using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table(Name = "address_in_groups")]
    public class GroupConntactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; set; }
        [Column(Name ="id")]
        public string ContactId { get; set; }
    }
}
