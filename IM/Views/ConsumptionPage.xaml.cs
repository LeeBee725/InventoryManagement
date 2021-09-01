using System;

using IM.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IM.Views
{
    public sealed partial class ConsumptionPage : Page
    {
        public ConsumptionViewModel ViewModel { get; } = new ConsumptionViewModel();

        public ConsumptionPage()
        {
            InitializeComponent();
        }
    }
}
