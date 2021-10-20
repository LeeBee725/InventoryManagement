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
        private bool isEdit { get; set; } = false;

        public RecipeForm()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is RecipeViewModel)
            {
                isEdit = false;
                RecipeViewModel = e.Parameter as RecipeViewModel;
            }
            else
            {
                isEdit = true;

                var param = e.Parameter as Tuple<RecipeViewModel,int>;
                RecipeViewModel = param.Item1;

                var Selected = RecipeViewModel.Recipes[param.Item2];

                TextBox NewMenuName = (TextBox)FindName("NewMenuName");
                Microsoft.UI.Xaml.Controls.RadioButtons NewMenuSize = (Microsoft.UI.Xaml.Controls.RadioButtons)FindName("NewMenuSize");
                TextBox NewMenuPrice = (TextBox)FindName("NewMenuPrice");
                Dictionary<string, int> Size = new Dictionary<string, int>();
                Size["M"] = 0;
                Size["L"] = 1;
                Size["XXXL"] = 2;
                NewMenuName.Text = Selected.Name;
                NewMenuSize.SelectedIndex = Size[Selected.Size];
                NewMenuPrice.Text = Selected.Price.ToString();

                ViewModel.SetIngredients(Selected.Ingredients);

                NewMenuName.IsEnabled = false;
                //NewMenuSize.IsEnabled = false;
            }

            base.OnNavigatedTo(e);
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

        private void BtnNewRecipeAdd_Click(object sender, RoutedEventArgs e)
        {
            TextBox NewMenuName = (TextBox)FindName("NewMenuName");
            Microsoft.UI.Xaml.Controls.RadioButtons NewMenuSize = (Microsoft.UI.Xaml.Controls.RadioButtons)FindName("NewMenuSize");
            TextBox NewMenuPrice = (TextBox)FindName("NewMenuPrice");

            Dictionary<string, double> NewMenuIngredients = ViewModel.GetIngredientsValuePairs();// 여기까지

            if (NewMenuName == null || NewMenuSize == null || NewMenuPrice == null)
            {
                Debug.WriteLine("There is NULL reference!!");
            }
            else
            {
                Debug.Write("Name: " + NewMenuName.Text + ", Size: " + NewMenuSize.SelectedItem.ToString() + ", Price: " + NewMenuPrice.Text + ", Ingredient: { ");
                int i = 0;
                foreach (var item in NewMenuIngredients)
                {
                    i++;
                    Debug.Write(item.Key + ":" + item.Value.ToString());
                    if(i != NewMenuIngredients.Count)
                    {
                        Debug.Write(", "); 
                    }
                }
                Debug.WriteLine(" }");
            }

            string name = NewMenuName.Text;
            string size = NewMenuSize.SelectedItem.ToString();
            double price = NewMenuPrice.Text != "" ? double.Parse(NewMenuPrice.Text) : 0.0;
            if(!isEdit)
            {
                RecipeViewModel.AddRecipe(name, size, price, NewMenuIngredients);
            }
            else
            {
                RecipeViewModel.ModifyRecipe(name, size, price, NewMenuIngredients);
            }

            Frame.Navigate(typeof(RecipePage),RecipeViewModel);
        }

        private void BtnNewRecipeCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecipePage));
        }
    }
}
