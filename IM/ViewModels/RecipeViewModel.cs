using System;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using IM.Core.Models;
using System.Collections.ObjectModel;

namespace IM.ViewModels
{
    public class RecipeViewModel : ObservableObject
    {
        private readonly RecipeList recipeList;
        public RecipeViewModel(RecipeList recipeList) => this.recipeList = recipeList;
        public ObservableCollection<Recipe> Recipes
        {
            get => recipeList.recipes;
            set => SetProperty(ref recipeList.recipes, value);
        }
    }
}
