using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string container { get; set; }
        public int num { get; set; }
        public int needs { get; set; }
        public int price { get; set; }

        public string OneLineSummary
        {
            get
            {
                return $"{this.id} {this.name} {this.container} {this.num} {this.needs} {this.price}\n";
            }
        }
    }

    public class ItemViewModel
    {
        MysqlHelper mysqlHelper;

        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items { get { return this.items; } }
        public ItemViewModel()
        {
            mysqlHelper = MysqlHelper.GetInstance();
            items = mysqlHelper.GetTotalItems();
        }
    }
}
