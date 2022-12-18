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
            optionsBuilder.UseSqlServer("Password=Numsey#2022;Persist Security Info=True;User ID=SA;Initial Catalog=TesteEventoApp;Data Source=sqldata");
        }
    }
}
