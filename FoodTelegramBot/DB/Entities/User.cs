namespace FoodTelegramBot.DB.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public long ChatId { get; set; }
        
        public  string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Cart Cart { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}