using System;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using IM.Core.Models;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

namespace IM.ViewModels
{
    public class InventoryViewModel : ObservableObject
    {
        private readonly Inventory inventory;
        public InventoryViewModel(Inventory inventory) => this.inventory = inventory;

        public ObservableCollection<Item> Items
        {
            get => inventory.items;
            set => SetProperty(ref inventory.items, value);
        }

        public async void AddItem(string name, double quantity, double quantity_of_needs, double price_per_piece, double quantity_per_piece)
        {
            if(inventory.AddItem(name, quantity, quantity_of_needs, price_per_piece, quantity_per_piece) != 0)
            {
                await new MessageDialog("항목 추가 중 오류 발생", "오류").ShowAsync();
            }
        }

        public async void ModifyItem(string name, double quantity, double quantity_of_needs, double price_per_piece, double quantity_per_piece)
        {
            if(inventory.ModifyItem(name, quantity, quantity_of_needs, price_per_piece, quantity_per_piece) != 0)
            {
                await new MessageDialog("항목 수정 중 오류 발생", "오류").ShowAsync();
            }
        }

        public async void DeleteItem(string name)
        {
            if (inventory.DeleteItem(name) != 0)
            {
                await new MessageDialog("항목 삭제 중 오류 발생", "오류").ShowAsync();
            }
        }

        public void SortItem()
        {
            inventory.SortItem();
        }
    }
}
