﻿using FoodTelegramBot.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodTelegramBot.DB
{
    public class TelegramBotContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<PizzaName> PizzaNames { get; set; }
        
        public TelegramBotContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DIMITRY\\SQLEXPRESS01;Database=botDb;Trusted_Connection=True;");
        }
    }
}