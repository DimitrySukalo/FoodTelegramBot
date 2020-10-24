using FoodTelegramBot.DB.Entities.Pizzas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTelegramBot.DB.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        [NotMapped]
        public List<PizzaBase> Pizzas { get; set; }

        public virtual ICollection<PizzaName> PizzaNames { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public decimal Price { get; set; }
    }
}
