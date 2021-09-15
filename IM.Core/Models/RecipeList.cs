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
        }
    }
}
