namespace FoodTelegramBot.DB.Entities.Pizzas.Ingredients
{
    public abstract class BaseIngredient
    {
        public abstract string Name { get; set; }

        public abstract decimal Const { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
