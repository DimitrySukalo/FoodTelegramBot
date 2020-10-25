using System.Collections.Generic;

namespace FoodTelegramBot.DB.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
