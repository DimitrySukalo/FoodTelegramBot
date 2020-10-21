using FoodTelegramBot.DB.Entities.Pizzas.Ingredients;
using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities.Pizzas
{
    public abstract class PizzaBase
    {
        public abstract string Name { get; }

        public decimal Cost { get; } = 50;

        public abstract List<BaseIngredient> Ingredients { get; }

        public abstract string PhotoPath { get; }

        public abstract double Weight { get; set; }

        public abstract SizeOfPizza SizeOfPizza { get; set; }

        public virtual decimal GetCost()
        {
            return Cost;
        }

        public override string ToString()
        {
            return Name + Cost;
        }
    }
}
