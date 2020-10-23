namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public class Onion : BaseIngredient
    {
        public override string Name { get; set; } = "Лук";
        public override decimal Const { get; set; } = 10;
    }
}
