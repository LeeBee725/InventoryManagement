using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using IM.Core.Helpers;
using System.Diagnostics;
using System.Linq;

namespace IM.Core.Models
{
    public class Inventory
    {
        private readonly MysqlHelper mysqlHelper;
        public ObservableCollection<Item> items;

        public Inventory()
        {
            mysqlHelper = MysqlHelper.GetInstance();
            items = mysqlHelper.GetTotalItems();
            
        }

        public int AddItem(in string name, in double quantity, in double quantity_of_needs, in double price_per_piece, in double quantity_per_piece)
        {
            mysqlHelper.InsertItem(name, quantity, quantity_of_needs, price_per_piece, quantity_per_piece);

            Item newItem = mysqlHelper.FindItem(name);
            if (newItem != null)
            {
                items.Add(newItem);
                return 0;
            }
            else
            {
                Debug.WriteLine("Inventory::AddItem():Error DB에서 아이템 찾기 실패");
                return 1;
            }
        }

        public int ModifyItem(string name, in double quantity, in double quantity_of_needs, in double price_per_piece, in double quantity_per_piece)
        {
            mysqlHelper.ModifyItem(name, quantity, quantity_of_needs, price_per_piece, quantity_per_piece);

            Item Editted = items.FirstOrDefault(x => x.Name == name);

            Item Modified = new Item
            {
                Name = name,
                Quantity = quantity,
                QuantityOfNeeds = quantity_of_needs,
                PricePerPiece = price_per_piece,
                QuantityPerPiece = quantity_per_piece
            };
            items[items.IndexOf(Editted)] = Modified;

            return 0;
        }
        
        public int DeleteItem(string name)
        {
            Item Removed = items.FirstOrDefault(x => x.Name == name);

            if (Removed != null)
            {
                items.Remove(Removed);

                mysqlHelper.DeleteItem(name);

                return 0;
            }
            else
            {
                Debug.WriteLine("Inventory::DeleteItem():Error DB에서 아이템 찾기 실패");
                return 1;
            }
        }

        public void SortItem()
        {
            items = new ObservableCollection<Item>(items.OrderByDescending(i => i.QuantityOfNeeds - i.Quantity));
        }
    }
}
