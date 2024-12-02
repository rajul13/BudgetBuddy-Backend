using BudgetBuddy.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy.DAL.DBContext
{
    public class BudgetBuddyDbContext : DbContext
    {
        public BudgetBuddyDbContext(DbContextOptions<BudgetBuddyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("public");
             // modelBuilder.Entity<UserEntity>().HasKey(e => new { e.userid });
            // modelBuilder.Entity<UsersListFunc>().HasNoKey();
        }
         public DbSet<UserEntity> User { get; set; }
    }
}
