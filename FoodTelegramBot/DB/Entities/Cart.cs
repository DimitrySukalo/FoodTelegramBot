using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public virtual ICollection<PizzaName> PizzaNames { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public decimal Price { get; set; }

        public bool IsOrdered { get; set; }
    }
}
