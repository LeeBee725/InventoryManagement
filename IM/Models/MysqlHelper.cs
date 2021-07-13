using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;

namespace IM.Models
{
    class MysqlHelper
    {
        private static MysqlHelper mysqlHelper;
        private readonly Session sess;
        private Table curTable;

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

        public void InsertItem(in string name, in int num, in string container)
        {
            try
            {
                curTable.Insert("name", "num", "container").Values(name, num, container).Execute();
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

        public void PrintItems()
        {
            try
            {
                RowResult result = curTable.Select("*").Execute();
                
                while (result.Next())
                {
                    Debug.WriteLine(result.Current.GetString("id") + " " + result.Current.GetString("name") + " " + result.Current.GetString("container") + " " + result.Current.GetString("num"));
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception in PrintItems():: " + e.Message  + "\n" + e.StackTrace + "\n" + e.Source + "\n" + e.TargetSite);
            }
        }
    }
}
