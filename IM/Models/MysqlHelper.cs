using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;

namespace IM
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

        public void InsertItem(in string name, in int num, in string container, in int needs, in int price)
        {
            try
            {
                curTable.Insert("name", "num", "container", "needs", "price").Values(name, num, container, needs, price).Execute();
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

        public void ModifyItem(in string name, in int num, in string container)
        {
            try
            {
                curTable.Update().Set("name", name).Set("num", num).Set("container", container).Where("name like :name").Bind("name", name).Execute();
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
        public void ModifyItem(in string name, in int num)
        {
            try
            {
                curTable.Update().Set("num", num).Where("name like :name").Bind("name", name).Execute();
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
        public void ModifyItem(in string name, in string container)
        {
            try
            {
                curTable.Update().Set("container", container).Where("name like :name").Bind("name", name).Execute();
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
        public void ModifyItem(in string name, in int num, in string container, in int needs, in int price)
        {
            try
            {
                curTable.Update().Set("name", name).Set("num", num).Set("container", container).Set("needs",needs).Set("price",price).Where("name like :name").Bind("name", name).Execute();
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

                if(row != null)
                {
                    item = new Item
                    {
                        Id = Int32.Parse(row.GetString("id")),
                        Name = row.GetString("name"),
                        Container = row.GetString("container"),
                        Num = Int32.Parse(row.GetString("num")),
                        Needs = Int32.Parse(row.GetString("needs")),
                        Price = Int32.Parse(row.GetString("price"))
                    };
                }
                
            } catch (NullReferenceException e)
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
                while(result.Next())
                {
                    itemList.Add(new Item
                    {
                        Id = Int32.Parse(result.Current.GetString("id")),
                        Name = result.Current.GetString("name"),
                        Container = result.Current.GetString("container"),
                        Num = Int32.Parse(result.Current.GetString("num")),
                        Needs = Int32.Parse(result.Current.GetString("needs")),
                        Price = Int32.Parse(result.Current.GetString("price"))
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
                        result.Current.GetString("container") + " " +
                        result.Current.GetString("num") + " " +
                        result.Current.GetString("needs") + " " +
                        result.Current.GetString("price"));
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception in PrintItems():: " + e.Message);
            }
        }
    }
}
