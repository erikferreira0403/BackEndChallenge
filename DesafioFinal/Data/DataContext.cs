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
            optionsBuilder.UseSqlServer("Password=Numsey#2022;Persist Security Info=True;User ID=SA;Initial Catalog=TesteEventoApp;Data Source=sqldata");//aqui vou passar a string de conexão

        }

        /* 
         * sa123456789@
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
