using System;
using IM.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Collections.Generic;

namespace IM.Views
{
    public sealed partial class RecipePage : Page
    {
        public RecipeViewModel ViewModel { get; } = new RecipeViewModel(new Core.Models.RecipeList());
        public Dictionary<string, double> NewIngredients;

        public RecipePage()
        {
            InitializeComponent();
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            Button BtnInfo = (Button)sender;
            StackPanel RecipeInfo = (StackPanel)((Grid)BtnInfo.Parent).FindName("RecipeInfo");

            if (RecipeInfo.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                BtnInfo.Content = "닫기";
                RecipeInfo.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                BtnInfo.Content = "상세정보";
                RecipeInfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void BtnRecipeAdd_Click(object sender, RoutedEventArgs e)
        {
            ListView RecipeListView = (ListView)FindName("RecipeListView");
            Grid RecipeForm = (Grid)FindName("RecipeForm");

            if(RecipeListView.Visibility == Visibility.Visible)
            {
                RecipeListView.Visibility = Visibility.Collapsed;
                RecipeForm.Visibility = Visibility.Visible;
            }
            else
            {
                RecipeListView.Visibility = Visibility.Visible;
                RecipeForm.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAddNewIngredients_Click(object sender, RoutedEventArgs e)
        {
            GridView NewMenuIngredients = (GridView)FindName("NewMenuIngredients");

            NewIngredients = new Dictionary<string, double>();

            GridViewItem Form = new GridViewItem();

            TextBox IngredientName = new TextBox();
            TextBox IngredientQuantity = new TextBox();
            AppBarButton BtnSave = new AppBarButton();
            AppBarButton BtnRemove = new AppBarButton();
            

            NewMenuIngredients.Items.Add(Form);
        }

        private void BtnIngredientCancel_Click(object sender, RoutedEventArgs e)
        {
            GridView NewMenuIngredients = (GridView)FindName("NewMenuIngredients");

            NewMenuIngredients.Items.Remove(NewMenuIngredients.SelectedItem);
        }
    }
}
