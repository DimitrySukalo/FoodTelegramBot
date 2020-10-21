using FoodTelegramBot.DB.Entities.Pizzas.Ingredients;
using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities.Pizzas
{
    public class MargaritaPizza : PizzaBase
    {
        public override string Name { get; }
        public override List<BaseIngredient> Ingredients { get; }
        public override string PhotoPath { get; }
        public override double Weight { get; }

        public override SizeOfPizza SizeOfPizza { get; }

        public MargaritaPizza()
        {
            Ingredients = new List<BaseIngredient>()
            {
                new Cheese(),
                new Tomato(),
                new TomatoSauce()
            };

            Name = "Маргарита";
            PhotoPath = @"img\margarita.png";
            Weight = 250;
            SizeOfPizza = SizeOfPizza.Small;
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
