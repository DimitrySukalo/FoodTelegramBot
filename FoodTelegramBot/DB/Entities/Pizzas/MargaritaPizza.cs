using FoodTelegramBot.DB.Entities.Pizzas.Ingredients;
using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities.Pizzas
{
    public class MargaritaPizza : PizzaBase
    {
        public override string Name { get; set; } = "Маргарита";
        public override List<BaseIngredient> Ingredients { get; set; }

        public MargaritaPizza()
        {
            Ingredients = new List<BaseIngredient>()
            {
                new Cheese(),
                new Tomato(),
                new TomatoSauce()
            };
        }

        public override decimal GetCost()
        {
            decimal ingredientsCost = 0;

            foreach(var ingredient in Ingredients)
            {
                ingredientsCost += ingredient.Const;
            }

            return base.GetCost() + ingredientsCost;
        }
    }
}
