using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDemo
{
    public class Container
    {
        public static List<Item> items = new List<Item>()
        {
            new Item() {Id = 1, Name = "명품불고기", Num = 2, Needs = 3, Price = 5000},
            new Item() {Id = 2, Name = "명품갈릭디핑소스", Num = 50, Needs = 100, Price = 500}
        };
    }
}
