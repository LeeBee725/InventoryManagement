using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IM.Core.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public bool IsAccept { get; set; } = false;

    }

    public class Ingredients
    {
        public ObservableCollection<Ingredient> ingredients;

        public Ingredients()
        {
            ingredients = new ObservableCollection<Ingredient>();
        }
    }
}
