namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public class Cheese : BaseIngredient
    {
        public override string Name { get; set; } = "Сыр";
        public override decimal Const { get; set; } = 10;
    }
}
