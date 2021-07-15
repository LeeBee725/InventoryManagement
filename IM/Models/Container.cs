using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM
{
    public class Container
    {
        public static List<Item> items = new List<Item>()
        {
            new Item() {id = 1, name = "명품불고기", num = 2, needs = 3, price = 5000},
            new Item() {id = 2, name = "명품갈릭디핑소스", num = 50, needs = 100, price = 500}
        };
    }
}
