using System;
using IM.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Media;

namespace IM.Views
{
    public sealed partial class RecipePage : Page
    {
        public RecipeViewModel ViewModel { get; set; } = new RecipeViewModel(new Core.Models.RecipeList());

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
            Frame.Navigate(typeof(RecipeForm), ViewModel);
        }
    }
}
