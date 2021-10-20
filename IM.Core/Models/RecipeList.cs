using System;
using System.Collections.Generic;
using System.Text;

using IM.Core.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace IM.Core.Models
{
    public class RecipeList
    {
        private readonly MysqlHelper mysqlHelper;
        public ObservableCollection<Recipe> recipes;

        public RecipeList()
        {
            mysqlHelper = MysqlHelper.GetInstance();
            recipes = mysqlHelper.GetTotalRecipes();
        }

        public bool AddRecipe(string name, string size, double price, Dictionary<string, double> ingredients)
        {
            mysqlHelper.InsertRecipe(name, size, price, ingredients);

            Recipe newRecipe = mysqlHelper.FindRecipe(name, size);
            if (newRecipe != null)
            {
                recipes.Add(newRecipe);
                return false;
            }
            else
            {
                Debug.WriteLine("RecipeList::AddRecipe():Error DB에서 레시피 찾기 실패");
                return true;
            }
        }

        public bool ModifyRecipe(string name, string size, double price, Dictionary<string, double> ingredients)
        {
            mysqlHelper.ModifyRecipe(name, size, price, ingredients);

            Recipe Editted = recipes.FirstOrDefault(x => x.Name == name && x.Size == size);

            Recipe Modified = new Recipe
            {
                Name = name,
                Size = size,
                Price = price,
                Ingredients = ingredients 
            };
            recipes[recipes.IndexOf(Editted)] = Modified;

            return false;
        }

        public bool DeleteRecipe(string name, string size)
        {
            Recipe Removed = recipes.FirstOrDefault(x => x.Name == name && x.Size == size);

            if (Removed != null)
            {
                recipes.Remove(Removed);

                mysqlHelper.DeleteRecipe(name, size);

                return false;
            }
            else
            {
                Debug.WriteLine("Inventory::DeleteItem():Error DB에서 아이템 찾기 실패");
                return true;
            }
        }
    }
}
