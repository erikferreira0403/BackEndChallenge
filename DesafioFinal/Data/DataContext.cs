using DesafioFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DesafioFinal.Data
{
    public class DataContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<EventHistory> EventHistories { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=sa123456789@;Persist Security Info=True;User ID=sa;Initial Catalog=TesteEventoApp;Data Source=ATLANTICO03958");//aqui vou passar a string de conexão

        }
        
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>().HasData(new List<Status>()
            {
                new Status (1, "Active"),
                new Status (2, "Inactive"),

            }); 
        }
        */
        
    }
}
