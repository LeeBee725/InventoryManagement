using System;

using IM.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IM.Views
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel { get; } = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
            ViewModel.Initialize(webView);
        }
    }
}
