using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Coo.Components
{
    partial class Ingredient
    {
        public string PriceColor
        {
            get
            {
                if (Cost > 100)
                    return "#FFFDD0";
                else
                    return "#98FF98";

            }
        }
       

            
        
    }
}
