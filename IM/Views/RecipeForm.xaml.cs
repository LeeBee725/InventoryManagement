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
using Windows.UI.Xaml.Navigation;

namespace IM.Views
{
    public sealed partial class RecipeForm : Page
    {
        public IngredientViewModel ViewModel { get; } = new IngredientViewModel(new Core.Models.Ingredients());
        public RecipeViewModel RecipeViewModel { get; set; }

        public RecipeForm()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            RecipeViewModel = e.Parameter as RecipeViewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecipePage));
        }

        private void BtnAddNewIngredients_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddEditIngredient();
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

            var Form = sender as DependencyObject;
            while (!(Form is Grid))
            {
                Form = VisualTreeHelper.GetParent(Form);
            }

            TextBox TBName = (TextBox)(Form as Grid).FindName("TBName");
            TextBox TBQuantity = (TextBox)(Form as Grid).FindName("TBQuantity");

            var Accepted = sender as DependencyObject;
            while (!(Accepted is GridViewItem))
            {
                Accepted = VisualTreeHelper.GetParent(Accepted);
            }
            
            int Index = NewMenuIngredients.Items.IndexOf(NewMenuIngredients.ItemFromContainer(Accepted));
            string Name = TBName.Text;
            double Quantity = double.Parse(TBQuantity.Text);

            ViewModel.AddIngredient(Index, Name, Quantity);
        }

        private void BtnIngredientRemove_Click(object sender, RoutedEventArgs e)
        {
            GridView NewMenuIngredients = (GridView)FindName("NewMenuIngredients");

            var Selected = sender as DependencyObject;
            while (!(Selected is GridViewItem))
            {
                Selected = VisualTreeHelper.GetParent(Selected);
            }
            int Index = NewMenuIngredients.Items.IndexOf(NewMenuIngredients.ItemFromContainer(Selected));

            ViewModel.RemoveIngredient(Index);
        }

        private void BtnIngredientEdit_Click(object sender, RoutedEventArgs e)
        {
            GridView NewMenuIngredients = (GridView)FindName("NewMenuIngredients");

            var Form = sender as DependencyObject;
            while (!(Form is Grid))
            {
                Form = VisualTreeHelper.GetParent(Form);
            }

            TextBlock IngredientName = (TextBlock)(Form as Grid).FindName("IngredientName");
            TextBlock IngredientQuantity = (TextBlock)(Form as Grid).FindName("IngredientQuantity");

            var Editted = sender as DependencyObject;
            while (!(Editted is GridViewItem))
            {
                Editted = VisualTreeHelper.GetParent(Editted);
            }

            int Index = NewMenuIngredients.Items.IndexOf(NewMenuIngredients.ItemFromContainer(Editted));
            string Name = IngredientName.Text;
            double Quantity = double.Parse(IngredientQuantity.Text);

            ViewModel.EditIngredient(Index, Name, Quantity);
        }
    }
}
