namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public class Tomato : BaseIngredient
    {
        public override string Name { get; set; } = "Помидора";
        public override decimal Const { get; set; } = 8;
    }
}
