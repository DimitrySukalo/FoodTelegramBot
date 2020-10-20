namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public class TomatoSauce : BaseIngredient
    {
        public override string Name { get; set; } = "Томатный соус";
        public override decimal Const { get; set; } = 17;
    }
}
