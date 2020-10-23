namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public class ChickenFillet : BaseIngredient
    {
        public override string Name { get; set; } = "Куриное филе";
        public override decimal Const { get; set; } = 19;
    }
}
