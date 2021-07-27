using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace IM
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class TotalPage : Page
    {
        public ItemViewModel ViewModel { get; set; }
        MysqlHelper mysqlHelper;
        public TotalPage()
        {
            this.InitializeComponent();
            mysqlHelper = MysqlHelper.GetInstance();
            this.ViewModel = new ItemViewModel();
        }

        private void ToggleNewItemTemplate(object sender, RoutedEventArgs e)
        {
            if (NewItemTemplate.Visibility == Visibility.Collapsed)
            {
                NewItemTemplate.Visibility = Visibility.Visible;
            }
            else
            {
                NewItemTemplate.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnNewItemAdd_Click(object sender, RoutedEventArgs e)
        {
            mysqlHelper.InsertItem(NewItemName.Text, Int32.Parse(NewItemNum.Text), NewItemContainer.Text, Int32.Parse(NewItemNeeds.Text),Int32.Parse(NewItemPrice.Text));

            Item NewItem = mysqlHelper.FindItem(NewItemName.Text);
            NewItemTemplate.Visibility = Visibility.Collapsed;
            if(NewItem != null) ViewModel.Items.Add(NewItem);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button DeleteBtn = (Button)sender;
            StackPanel stackPanel = (StackPanel)DeleteBtn.Parent;
            Grid grid = (Grid)stackPanel.Parent;
            TextBlock NameTextBlock = (TextBlock)grid.Children[1];
            
            Item Selected = ViewModel.Items.Where(x => x.Name == NameTextBlock.Text).FirstOrDefault();
            mysqlHelper.DeleteItem(NameTextBlock.Text);
            Item Removed = mysqlHelper.FindItem(NameTextBlock.Text);
            if (Removed == null && Selected != null) ViewModel.Items.Remove(Selected);
        }

        private async void BtnNumUp_Click(object sender, RoutedEventArgs e)
        {
            RepeatButton BtnNumUp = (RepeatButton)sender;
            StackPanel stackPanel = (StackPanel)BtnNumUp.Parent;
            Grid grid = (Grid)stackPanel.Parent;
            TextBlock NameTextBlock = (TextBlock)grid.Children[1];
            TextBlock NumTextBlock = (TextBlock)stackPanel.Children[1];

            int num = Int32.Parse(NumTextBlock.Text);
            if(num < 999)
            {
                NumTextBlock.Text = (++num).ToString();
                mysqlHelper.ModifyItem(NameTextBlock.Text, num);
            }
            else
            {
                Debug.WriteLine("BtnNumUp_Click:: it can't be over 999");
                await new MessageDialog("수량이 999보다 클 수 없습니다.", "알림").ShowAsync();
            }
            
        }

        private async void BtnNumDown_Click(object sender, RoutedEventArgs e)
        {
            RepeatButton BtnNumUp = (RepeatButton)sender;
            StackPanel stackPanel = (StackPanel)BtnNumUp.Parent;
            Grid grid = (Grid)stackPanel.Parent;
            TextBlock NameTextBlock = (TextBlock)grid.Children[1];
            TextBlock NumTextBlock = (TextBlock)stackPanel.Children[1];

            int num = Int32.Parse(NumTextBlock.Text);
            if (num > 0)
            {
                NumTextBlock.Text = (--num).ToString();
                mysqlHelper.ModifyItem(NameTextBlock.Text, num);
            }
            else
            {
                Debug.WriteLine("BtnNumUp_Click:: it can't be under 0");
                await new MessageDialog("수량이 0보다 작을 수 없습니다.", "알림").ShowAsync();
            }
            
        }
    }

    
}
