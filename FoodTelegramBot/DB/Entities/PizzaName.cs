namespace FoodTelegramBot.DB.Entities
{
    public class PizzaName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
