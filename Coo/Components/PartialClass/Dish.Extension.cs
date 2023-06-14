using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coo.Components
{
    partial class Dish
    {
        public double ServingPrice
        {
            get
            {
                var allIngredients = CookingStage.SelectMany(x => x.IngredientOfStage).ToList();
                double totalSum = allIngredients.Sum(x => x.Quantity * x.Ingredient.CostForCount);
                double price = totalSum / ServingQuantity;
                return price;
            }
        }
        public string PhotoFullPath => $"/WpfAppTrueSkills_Recipes;component/Resources/{PhotoPath}";
        public int? Time
        {
            get
            {
                return this.CookingStage.Sum(x => x.TimeInMinutes);
            }
        }
        public float? Quantity
        {
            get
            {
                return this.CookingStage.Sum(x => x.IngredientOfStage.Count);
            }
        }
        public IEnumerable<Ingredient> Ingredients => CookingStage.SelectMany(c => c.IngredientOfStage.Select(i => i.Ingredient)).ToList();
        public IEnumerable<IngredientOfStage> IngredientOfStage => CookingStage.SelectMany(c => c.IngredientOfStage).ToList();
        public double TotalSumDish => CookingStage.Sum(c => c.IngredientOfStage.Sum(i => (double)i.Quantity *  (double)i.Ingredient.CostForCount)) / ServingQuantity;
    }
}
    
        

