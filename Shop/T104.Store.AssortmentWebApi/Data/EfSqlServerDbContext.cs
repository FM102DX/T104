using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Assortment
{ 

    public class EfSqlServerDbContext : DbContext
    {
        public string DbPath { get; private set; }

        public string Guid { get; private set; }

        public EfSqlServerDbContext()
        {
           // DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyOtusObjects.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=u1325524_AuthDb;Initial Catalog=FerryIs4;Persist Security Info=True;User ID=u1325524_AuthDbAdmin;Password=9y1U?z1t;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*
             * 
             * modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.FullName).HasMaxLength(30);
            });

            */
          //  modelBuilder.Entity<Employee>();
           // modelBuilder.Entity<Role>();

            base.OnModelCreating(modelBuilder);
        }


    }
}
