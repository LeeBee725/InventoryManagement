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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Container { get; set; }
        public int Num { get; set; }
        public int Needs { get; set; }
        public int Price { get; set; }

        public string OneLineSummary
        {
            get
            {
                return $"{this.Id} {this.Name} {this.Container} {this.Num} {this.Needs} {this.Price}\n";
            }
        }
    }

    public class ItemViewModel
    {
        readonly MysqlHelper mysqlHelper;

        private readonly ObservableCollection<Item> items;
        public ObservableCollection<Item> Items { get { return this.items; } }
        public ItemViewModel()
        {
            mysqlHelper = MysqlHelper.GetInstance();
            items = mysqlHelper.GetTotalItems();
            items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Action::{e.Action} old = {e.OldItems} new = {e.NewItems}");
        }
    }
}
