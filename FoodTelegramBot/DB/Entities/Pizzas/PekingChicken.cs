using FoodTelegramBot.DB.Entities.Pizzas.Ingredients;
using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities.Pizzas
{
    public class PekingChicken : PizzaBase
    {
        public override string Name => "Пекинский ципленок";

        public override List<BaseIngredient> Ingredients { get; }

        public override string PhotoPath { get; }

        public override double Weight { get; set; }

        public PekingChicken()
        {
            Ingredients = new List<BaseIngredient>()
            {
                new Cheese(),
                new TomatoSauce(),
                new ChickenFillet(),
                new Chille()
            };

            PhotoPath = @"img\pekin.png";
            Weight = 260;
        }

        public override decimal GetCost()
        {
            decimal ingredientsCost = 0;

            foreach (var ingredient in Ingredients)
            {
                ingredientsCost += ingredient.Const;
            }

            return Cost + ingredientsCost;
        }
    }
}
