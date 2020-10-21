using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities.Pizzas
{
    public static class PizzaList
    {
        public static IEnumerable<PizzaBase> Pizzas { get; } = new List<PizzaBase>()
        {
            new MargaritaPizza()
        };
    }
}
