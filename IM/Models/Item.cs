using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int curNum { get; set; }
        public int needs { get; set; }
        public int price { get; set; }
    }
}
