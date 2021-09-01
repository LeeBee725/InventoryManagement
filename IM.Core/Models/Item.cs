using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Core.Models
{
    public class Item
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double QuantityOfNeeds { get; set; }
        public double PricePerPiece { get; set; }
        public double QuantityPerPiece { get; set; }
    }
}
