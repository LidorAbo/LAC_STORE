using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using System.Data.Entity;
namespace FinalProject.Dal
{
    public class OrdersDal : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }

}