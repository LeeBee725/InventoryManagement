using System;
using System.Collections.Generic;
using System.Text;

using IM.Core.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

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

            foreach (var i in recipes)
            {
                Debug.WriteLine(i.Name);
                int cnt = 0;
                foreach (var j in i.Ingredients)
                {
                    Debug.WriteLine(cnt++);
                    Debug.WriteLine("{0} {1}", j.Key, j.Value);
                }
            }
        }
    }
}
