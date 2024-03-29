﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Core.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }

        public Dictionary<string,double> Ingredients { get; set; } = new Dictionary<string,double>();
    }
}
