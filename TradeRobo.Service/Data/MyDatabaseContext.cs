using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace TradeRobo.Service
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Pie> Pie { get; set; }
        public DbSet<PieDetail> PieDetail { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<OrderGroup> OrderGroup { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<UserConfig> UserConfig { get; set; }

        public DbSet<TDAccount> TDAccount { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //    modelBuilder.Entity<UserRole>()
        //        .HasKey(x => new {x.UserId, x.RoleId } );
        //    modelBuilder.Entity<UserRole>()
        //        .HasOne(x => x.User)
        //        .WithMany(m => m.Roles )
        //        .HasForeignKey(x => x.UserId );
        //    modelBuilder.Entity<UserRole>()
        //        .HasOne(x => x.Role)
        //        .WithMany(e => e.Users)
        //        .HasForeignKey(x => x. );
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}
    }
}
