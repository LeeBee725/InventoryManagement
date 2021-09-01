using System;
using System.Collections.Generic;
using System.Text;

using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using IM.Core.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace IM.Core.Helpers
{
    class MysqlHelper
    {
        private static MysqlHelper mysqlHelper;
        private readonly Session sess;
        private Table curTable;

        // Singleton 패턴 사용
        public static MysqlHelper GetInstance()
        {
            if (mysqlHelper == null) mysqlHelper = new MysqlHelper();
            return mysqlHelper;
        }
        private MysqlHelper()
        {
            MySqlXConnectionStringBuilder stringBuilder =
                new MySqlXConnectionStringBuilder
                {
                    Server = MysqlConnectionInfo.SERVER,
                    UserID = MysqlConnectionInfo.USER,
                    Password = MysqlConnectionInfo.PASSWORD,
                    Database = MysqlConnectionInfo.DATABASE
                };

            sess = MySQLX.GetSession(stringBuilder.ToString());
            Debug.WriteLine("Debug in sess.GetCurrentSchema()::" + sess.GetCurrentSchema().Name);
            curTable = sess.GetCurrentSchema().GetTable(MysqlConnectionInfo.TABLE);
            Debug.WriteLine("Debug in curTable::" + curTable.Name);
        }

        public void InsertItem(in string name, in double quantity, in double quantity_of_needs, in double price_per_piece, in double quantity_per_piece)
        {
            try
            {
                curTable.Insert("name", "quantity", "quantity_of_needs", "price_per_piece", "quantity_per_piece").Values(name, quantity, quantity_of_needs, price_per_piece, quantity_per_piece).Execute();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in InsertItem():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in InsertItem():: " + e.Message);
            }

        }

        public void DeleteItem(in string name)
        {
            try
            {
                curTable.Delete().Where("name like :name").Bind("name", name).Execute();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in DeleteItem():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in DeleteItem():: " + e.Message);
            }
        }

        public void ModifyItem(in string name, in double quantity, in double quantity_of_needs, in double price_per_piece, in double quantity_per_piece)
        {
            try
            {
                curTable.Update().Set("name", name).Set("quantity", quantity).Set("quantity_of_needs", quantity_of_needs).Set("price_per_piece", price_per_piece).Set("quantity_per_piece", quantity_per_piece).Where("name like :name").Bind("name", name).Execute();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in ModifyItem():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in ModifyItem():: " + e.Message);
            }
        }

        public Item FindItem(in string name)
        {
            Item item = null;
            try
            {
                RowResult result = curTable.Select("*").Where("name like :name").Bind("name", name).Execute();
                Row row = result.FetchOne();

                if (row != null)
                {
                    item = new Item
                    {
                        Name = result.Current.GetString("name"),
                        Quantity = Double.Parse(result.Current.GetString("quantity")),
                        QuantityOfNeeds = Double.Parse(result.Current.GetString("quantity_of_needs")),
                        PricePerPiece = Double.Parse(result.Current.GetString("price_per_piece")),
                        QuantityPerPiece = Double.Parse(result.Current.GetString("quantity_per_piece"))
                    };
                }

            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in FindItem():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in FindItem():: " + e.Message);
            }

            return item;
        }

        public ObservableCollection<Item> GetTotalItems()
        {
            ObservableCollection<Item> itemList = new ObservableCollection<Item>();
            try
            {
                //ObservableCollection<Item> itemList = new ObservableCollection<Item>();
                RowResult result = curTable.Select("*").Execute();
                while (result.Next())
                {
                    itemList.Add(new Item
                    {
                        Name = result.Current.GetString("name"),
                        Quantity = Double.Parse(result.Current.GetString("quantity")),
                        QuantityOfNeeds = Double.Parse(result.Current.GetString("quantity_of_needs")),
                        PricePerPiece = Double.Parse(result.Current.GetString("price_per_piece")),
                        QuantityPerPiece = Double.Parse(result.Current.GetString("quantity_per_piece"))
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in GetTotalItems():: " + e.Message);
            }

            return itemList;
        }

        public void PrintItems()
        {
            try
            {
                RowResult result = curTable.Select("*").Execute();

                while (result.Next())
                {
                    Debug.WriteLine(
                        result.Current.GetString("id") + " " +
                        result.Current.GetString("name") + " " +
                        result.Current.GetString("quantity") + " " +
                        result.Current.GetString("quantity_of_needs") + " " +
                        result.Current.GetString("price_per_piece") + " " +
                        result.Current.GetString("quantity_per_piece"));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in PrintItems():: " + e.Message);
            }
        }
    }
}
