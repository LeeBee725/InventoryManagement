using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IM.Core.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IM.Control
{
    public class IngredientsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Accept { get; set; }
        public DataTemplate Edit { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            if((item as Ingredient).IsAccept)
            {
                return Accept;
            }
            else
            {
                return Edit;
            }
        }
    }
}
