using System;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using IM.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

namespace IM.ViewModels
{
    public class IngredientViewModel : ObservableObject
    {
        private readonly Ingredients ingredientList;
        public IngredientViewModel(Ingredients ingredientList) => this.ingredientList = ingredientList;

        public ObservableCollection<Ingredient> Ingredients
        {
            get => ingredientList.ingredients;
            set => SetProperty(ref ingredientList.ingredients, value);
        }

        public void AddEditIngredient(string name = "", double quantity = 0)
        {
            ingredientList.ingredients.Add(new Ingredient {Name = name, Quantity = quantity, IsAccept = false });
        }

        public void EditIngredient(int idx, string name, double quantity)
        {
            var Removed = ingredientList.ingredients[idx];
            ingredientList.ingredients.Remove(Removed);
            ingredientList.ingredients.Insert(idx, new Ingredient { Name = name, Quantity = quantity, IsAccept = false });
        }

        public void RemoveEmptyIngredient(int idx)
        {
            var Removed = ingredientList.ingredients[idx];
            ingredientList.ingredients.Remove(Removed);
        }

        public void AddIngredient(int idx, string name, double quantity)
        {
            var Removed = ingredientList.ingredients[idx];
            ingredientList.ingredients.Remove(Removed);
            ingredientList.ingredients.Insert(idx, new Ingredient { Name = name, Quantity = quantity, IsAccept = true });
        }

        public async void RemoveIngredient(int idx)
        {
            MessageDialog msg = new MessageDialog("항목을 삭제하시겠습니까?", "확인");

            UICommand BtnOK = new UICommand("확인");
            //BtnOK.Invoked = BtnOK_Click;
            msg.Commands.Add(BtnOK);

            UICommand BtnCancel = new UICommand("취소");
            //BtnCancel.Invoked = BtnCancel_Click;
            msg.Commands.Add(BtnCancel);

            var command = await msg.ShowAsync();
            if(command == BtnOK)
            {
                RemoveEmptyIngredient(idx);
            }
        }

        private void BtnCancel_Click(IUICommand command)
        {
            
        }

        private void BtnOK_Click(IUICommand command)
        {
            
        }
    }
}
