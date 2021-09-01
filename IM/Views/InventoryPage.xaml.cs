using System;
using System.Diagnostics;
using IM.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace IM.Views
{
    public sealed partial class InventoryPage : Page
    {
        public InventoryViewModel ViewModel { get; } = new InventoryViewModel(new Core.Models.Inventory());

        public InventoryPage()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBlock newItemAdd = (TextBlock) FindName("NewItemAdd");
            TextBox newItemName = (TextBox)FindName("NewItemName");
            TextBox newItemQuantity = (TextBox)FindName("NewItemQuantity");
            TextBox newItemQON = (TextBox)FindName("NewItemQON");
            TextBox newItemPPP = (TextBox)FindName("NewItemPPP");
            TextBox newItemQPP = (TextBox)FindName("NewItemQPP");
            StackPanel newItemBtns = (StackPanel)FindName("NewItemBtns");
            
            if(newItemAdd.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                newItemName.Text = "";
                newItemQuantity.Text = "";
                newItemQON.Text = "";
                newItemPPP.Text = "";
                newItemQPP.Text = "";

                newItemAdd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemQON.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemPPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemQPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemBtns.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                newItemAdd.Visibility = Windows.UI.Xaml.Visibility.Visible;
                newItemName.Visibility = Windows.UI.Xaml.Visibility.Visible;
                newItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Visible;
                newItemQON.Visibility = Windows.UI.Xaml.Visibility.Visible;
                newItemPPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
                newItemQPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
                newItemBtns.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private async void BtnAddAccept_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBlock newItemAdd = (TextBlock)FindName("NewItemAdd");
            TextBox newItemName = (TextBox)FindName("NewItemName");
            TextBox newItemQuantity = (TextBox)FindName("NewItemQuantity");
            TextBox newItemQON = (TextBox)FindName("NewItemQON");
            TextBox newItemPPP = (TextBox)FindName("NewItemPPP");
            TextBox newItemQPP = (TextBox)FindName("NewItemQPP");
            StackPanel newItemBtns = (StackPanel)FindName("NewItemBtns");

            if (newItemName.Text == "" || newItemQuantity.Text == "" || newItemQON.Text == "" || newItemPPP.Text == "" || newItemQPP.Text == "")
            {
                await new MessageDialog("빈 칸을 채워주세요.", "오류").ShowAsync();
            }
            else
            {
                ViewModel.AddItem(newItemName.Text, double.Parse(newItemQuantity.Text), double.Parse(newItemQON.Text), double.Parse(newItemPPP.Text), double.Parse(newItemQPP.Text));

                newItemName.Text = "";
                newItemQuantity.Text = "";
                newItemQON.Text = "";
                newItemPPP.Text = "";
                newItemQPP.Text = "";

                newItemAdd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemQON.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemPPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemQPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                newItemBtns.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void BtnAddCancel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBlock newItemAdd = (TextBlock)FindName("NewItemAdd");
            TextBox newItemName = (TextBox)FindName("NewItemName");
            TextBox newItemQuantity = (TextBox)FindName("NewItemQuantity");
            TextBox newItemQON = (TextBox)FindName("NewItemQON");
            TextBox newItemPPP = (TextBox)FindName("NewItemPPP");
            TextBox newItemQPP = (TextBox)FindName("NewItemQPP");
            StackPanel newItemBtns = (StackPanel)FindName("NewItemBtns");

            newItemName.Text = "";
            newItemQuantity.Text = "";
            newItemQON.Text = "";
            newItemPPP.Text = "";
            newItemQPP.Text = "";

            newItemAdd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            newItemName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            newItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            newItemQON.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            newItemPPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            newItemQPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            newItemBtns.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void BtnDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button DeleteBtn = (Button)sender;
            StackPanel sp = (StackPanel)DeleteBtn.Parent;
            Grid grid = (Grid)sp.Parent;
            TextBlock itemName = (TextBlock)grid.FindName("ItemName");

            ViewModel.DeleteItem(itemName.Text);
        }

        private void BtnModify_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button BtnModify = (Button)sender;
            StackPanel sp = (StackPanel)BtnModify.Parent;
            Grid grid = (Grid)sp.Parent;

            TextBlock ItemQuantity = (TextBlock)grid.FindName("ItemQuantity");
            TextBlock ItemQON = (TextBlock)grid.FindName("ItemQON");
            TextBlock ItemPPP = (TextBlock)grid.FindName("ItemPPP");
            TextBlock ItemQPP = (TextBlock)grid.FindName("ItemQPP");

            TextBox ChangedQuantity = (TextBox)grid.FindName("ChangedQuantity");
            TextBox ChangedQON = (TextBox)grid.FindName("ChangedQON");
            TextBox ChangedPPP = (TextBox)grid.FindName("ChangedPPP");
            TextBox ChangedQPP = (TextBox)grid.FindName("ChangedQPP");
            Button BtnSave = (Button)sp.FindName("BtnSave");
            Button BtnDelete = (Button)sp.FindName("BtnDelete");

            if( BtnModify.Content.Equals("수정") )
            {
                BtnModify.Content = "취소";

                ItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ItemQON .Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ItemPPP .Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ItemQPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                BtnDelete.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                ChangedQuantity.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ChangedQON.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ChangedPPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ChangedQPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
                BtnSave.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                BtnModify.Content = "수정";
                ItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ItemQON.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ItemPPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ItemQPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
                BtnDelete.Visibility = Windows.UI.Xaml.Visibility.Visible;

                ChangedQuantity.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ChangedQON.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ChangedPPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ChangedQPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                BtnSave.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void BtnSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button BtnSave = (Button)sender;
            StackPanel sp = (StackPanel)BtnSave.Parent;
            Grid grid = (Grid)sp.Parent;

            TextBlock ItemQuantity = (TextBlock)grid.FindName("ItemQuantity");
            TextBlock ItemQON = (TextBlock)grid.FindName("ItemQON");
            TextBlock ItemPPP = (TextBlock)grid.FindName("ItemPPP");
            TextBlock ItemQPP = (TextBlock)grid.FindName("ItemQPP");

            TextBlock ItemName = (TextBlock)grid.FindName("ItemName");
            TextBox ChangedQuantity = (TextBox)grid.FindName("ChangedQuantity");
            TextBox ChangedQON = (TextBox)grid.FindName("ChangedQON");
            TextBox ChangedPPP = (TextBox)grid.FindName("ChangedPPP");
            TextBox ChangedQPP = (TextBox)grid.FindName("ChangedQPP");
            Button BtnDelete = (Button)sp.FindName("BtnDelete");
            Button BtnModify = (Button)sp.FindName("BtnModify");

            string name = ItemName.Text;
            double quantity = ChangedQuantity.Text.Equals("") ? double.Parse(ChangedQuantity.PlaceholderText) : double.Parse(ChangedQuantity.Text);
            double qon = ChangedQON.Text.Equals("") ? double.Parse(ChangedQON.PlaceholderText) : double.Parse(ChangedQON.Text);
            double ppp = ChangedPPP.Text.Equals("") ? double.Parse(ChangedPPP.PlaceholderText) : double.Parse(ChangedPPP.Text);
            double qpp = ChangedQPP.Text.Equals("") ? double.Parse(ChangedQPP.PlaceholderText) : double.Parse(ChangedQPP.Text);

            ViewModel.ModifyItem(name, quantity, qon, ppp, qpp);

            BtnModify.Content = "수정";
            ItemQuantity.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ItemQON.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ItemPPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ItemQPP.Visibility = Windows.UI.Xaml.Visibility.Visible;
            BtnDelete.Visibility = Windows.UI.Xaml.Visibility.Visible;

            ChangedQuantity.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ChangedQON.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ChangedPPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ChangedQPP.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            BtnSave.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
