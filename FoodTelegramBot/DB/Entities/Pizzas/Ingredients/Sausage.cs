namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public class Sausage : BaseIngredient
    {
        public override string Name { get; set; } = "Колбаса";
        public override decimal Const { get; set; } = 15;
    }
}
