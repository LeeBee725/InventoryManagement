using System;
using IM.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Media;
using System.Linq;

namespace IM.Views
{
    public sealed partial class RecipeForm : Page
    {
        public IngredientViewModel ViewModel { get; } = new IngredientViewModel(new Core.Models.Ingredients());

        public RecipeForm()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecipePage));
        }

        private void BtnAddNewIngredients_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddEmptyIngredient();
        }

        private void BtnIngredientCancel_Click(object sender, RoutedEventArgs e)
        {
            GridView NewMenuIngredients = (GridView)FindName("NewMenuIngredients");

            var Selected = sender as DependencyObject;
            while (!(Selected is GridViewItem))
            {
                Selected = VisualTreeHelper.GetParent(Selected);
            }
            int Index = NewMenuIngredients.Items.IndexOf(NewMenuIngredients.ItemFromContainer(Selected));

            ViewModel.RemoveEmptyIngredient(Index);
        }

        private void BtnIngredientAccept_Click(object sender, RoutedEventArgs e)
        {
            GridView NewMenuIngredients = (GridView)FindName("NewMenuIngredients");

            var Accepted = sender as DependencyObject;
            while (!(Accepted is GridViewItem))
            {
                Accepted = VisualTreeHelper.GetParent(Accepted);
            }

        }
    }
}
