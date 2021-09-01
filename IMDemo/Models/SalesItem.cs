using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDemo
{
    public class SalesItem
    {
        public string Name { get; set; }
        public int Num { get; set; }
        public int Price { get; set; }
    }

    public class SalesItemViewModel
    {
        private readonly ObservableCollection<SalesItem> items;
        public ObservableCollection<SalesItem> Items { get { return this.items; } }
        public SalesItemViewModel()
        {
            items = new ObservableCollection<SalesItem>();
            Items.Add(new SalesItem { Name = "명품불고기피자", Num = 3, Price = 18900 });
            Items.Add(new SalesItem { Name = "비프갈릭피자", Num = 2, Price = 18900 });
            Items.Add(new SalesItem { Name = "명품불고기피자", Num = 3, Price = 18900 });
            Items.Add(new SalesItem { Name = "비프갈릭피자", Num = 2, Price = 18900 });
            Items.Add(new SalesItem { Name = "명품불고기피자", Num = 3, Price = 18900 });
            Items.Add(new SalesItem { Name = "비프갈릭피자", Num = 2, Price = 18900 });
            Items.Add(new SalesItem { Name = "명품불고기피자", Num = 3, Price = 18900 });
            Items.Add(new SalesItem { Name = "비프갈릭피자", Num = 2, Price = 18900 });
            Items.Add(new SalesItem { Name = "명품불고기피자", Num = 3, Price = 18900 });
            Items.Add(new SalesItem { Name = "비프갈릭피자", Num = 2, Price = 18900 });

        }
    }
}
