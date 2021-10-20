using System;
using System.Collections.Generic;
using System.Text;

using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using IM.Core.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

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
            curTable = sess.GetCurrentSchema().GetTable(MysqlConnectionInfo.TABLE_PRODUCT);
            Debug.WriteLine("Debug in curTable::" + curTable.Name);
        }

        public void SelectTable(string TableName)
        {
            try
            {
                curTable = sess.GetCurrentSchema().GetTable(TableName);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in SelectTable():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in InsertItem():: " + e.Message);
            }
        }

        public void InsertItem(in string name, in double quantity, in double quantity_of_needs, in double price_per_piece, in double quantity_per_piece)
        {
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_PRODUCT);
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
                SelectTable(MysqlConnectionInfo.TABLE_PRODUCT);
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
                SelectTable(MysqlConnectionInfo.TABLE_PRODUCT);
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
                SelectTable(MysqlConnectionInfo.TABLE_PRODUCT);
                RowResult result = curTable.Select("*").Where("name like :name").Bind("name", name).Execute();
                Row row = result.FetchOne();

                if (row != null)
                {
                    item = new Item
                    {
                        Name = result.Current.GetString("name"),
                        Quantity = double.Parse(result.Current.GetString("quantity")),
                        QuantityOfNeeds = double.Parse(result.Current.GetString("quantity_of_needs")),
                        PricePerPiece = double.Parse(result.Current.GetString("price_per_piece")),
                        QuantityPerPiece = double.Parse(result.Current.GetString("quantity_per_piece"))
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
                SelectTable(MysqlConnectionInfo.TABLE_PRODUCT);
                
                RowResult result = curTable.Select("*").Execute();
                while (result.Next())
                {
                    itemList.Add(new Item
                    {
                        Name = result.Current.GetString("name"),
                        Quantity = double.Parse(result.Current.GetString("quantity")),
                        QuantityOfNeeds = double.Parse(result.Current.GetString("quantity_of_needs")),
                        PricePerPiece = double.Parse(result.Current.GetString("price_per_piece")),
                        QuantityPerPiece = double.Parse(result.Current.GetString("quantity_per_piece"))
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
                SelectTable(MysqlConnectionInfo.TABLE_PRODUCT);
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

        public void InsertRecipe(string name, string size, double price, Dictionary<string, double> ingredients)
        {
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_MENU);
                curTable.Insert("name", "size", "price", "ingredients").Values(name, size, price, JsonConvert.SerializeObject(ingredients) ).Execute();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in InsertRecipe():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in InsertRecipe():: " + e.Message);
            }
        }

        public void DeleteRecipe(in string name, in string size)
        {
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_MENU);
                curTable.Delete().Where("name like :name and size like :size").Bind("name", name).Bind("size", size).Execute();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in DeleteRecipe():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in DeleteRecipe():: " + e.Message);
            }
        }

        public void ModifyRecipe(in string name, in string size, in double price, in Dictionary<string, double> ingredients)
        {
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_MENU);
                curTable.Update().Set("name", name).Set("size", size).Set("price", price).Set("ingredients", JsonConvert.SerializeObject(ingredients)).Where("name like :name and size like :size").Bind("name", name).Bind("size", size).Execute();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in ModifyRecipe():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in ModifyRecipe():: " + e.Message);
            }
        }

        public Recipe FindRecipe(in string name, in string size)
        {
            Recipe recipe = null;
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_MENU);
                RowResult result = curTable.Select("*").Where("name like :name and size like :size").Bind("name", name).Bind("size", size).Execute();
                Row row = result.FetchOne();

                if (row != null)
                {
                    recipe = new Recipe
                    {
                        Name = result.Current.GetString("name"),
                        Size = result.Current.GetString("size"),
                        Price = double.Parse(result.Current.GetString("price")),
                        Ingredients = JsonConvert.DeserializeObject<Dictionary<string, double>>(result.Current.GetString("ingredients"))
                    };
                }

            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException in FindRecipe():: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException in FindRecipe():: " + e.Message);
            }

            return recipe;
        }

        public ObservableCollection<Recipe> GetTotalRecipes()
        {
            ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_MENU);

                //RowResult result = curTable.Select("*").Execute();
                SqlResult result = sess.SQL("SELECT name, size, price, CONVERT(ingredients USING utf8mb4) as ingredients FROM inventory.menu;").Execute();

                while (result.Next())
                {
                    Debug.WriteLine(result.Current.GetString("ingredients"));
                    
                    recipes.Add(new Recipe
                    {
                        Name = result.Current.GetString("name"),
                        Size = result.Current.GetString("size"),
                        Price = double.Parse(result.Current.GetString("price")),
                        Ingredients = JsonConvert.DeserializeObject<Dictionary<string, double>>(result.Current.GetString("ingredients"))
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in GetTotalRecipes():: " + e.Message);
            }

            return recipes;
        }

        public void PrintRecipes()
        {
            try
            {
                SelectTable(MysqlConnectionInfo.TABLE_MENU);
                //RowResult result = curTable.Select("*").Execute();
                SqlResult result = sess.SQL("SELECT name, size, price, CONVERT(ingredients USING utf8mb4) as ingredients FROM inventory.menu;").Execute();

                while (result.Next())
                {
                    Debug.WriteLine(
                        result.Current.GetString("name") + " " +
                        result.Current.GetString("size") + " " +
                        result.Current.GetString("price") + " " +
                        result.Current.GetString("ingredients"));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in PrintRecipes():: " + e.Message);
            }
        }
    }
}
