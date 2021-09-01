using System;

using IM.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IM.Views
{
    public sealed partial class RecipePage : Page
    {
        public RecipeViewModel ViewModel { get; } = new RecipeViewModel();

        public RecipePage()
        {
            InitializeComponent();
        }
    }
}
