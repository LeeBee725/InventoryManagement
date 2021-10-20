using System;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using IM.Core.Models;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using System.Collections.Generic;

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

        public async void AddRecipe(string name, string size, double price, Dictionary<string, double> ingredients)
        {
            if (name != "" && price != 0 && recipeList.AddRecipe(name, size, price, ingredients))
            {
                await new MessageDialog("항목 추가 중 오류 발생", "오류").ShowAsync();
            }
        }

        public async void ModifyRecipe(string name, string size, double price, Dictionary<string, double> ingredients)
        {
            if (recipeList.ModifyRecipe(name, size, price, ingredients))
            {
                await new MessageDialog("항목 추가 중 오류 발생", "오류").ShowAsync();
            }
        }

        public async void DeleteRecipe(int selectedIndex)
        {
            if (recipeList.DeleteRecipe(Recipes[selectedIndex].Name, Recipes[selectedIndex].Size))
            {
                await new MessageDialog("항목 추가 중 오류 발생", "오류").ShowAsync();
            }
        }
    }
}
