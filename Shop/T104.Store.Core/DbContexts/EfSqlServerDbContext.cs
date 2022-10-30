using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T104.Store.Engine.DbContexts
{
    public class EfSqlServerDbContext : DbContext
    {
        public string DbPath { get; private set; }

        public string Guid { get; private set; }

        public EfSqlServerDbContext()
        {
            DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyOtusObjects.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=u1325524_mssql;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Server=37.140.192.97;Database=u1325524_mssql;User ID=u1325524_root;Password=_9Hjb73i;");
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
