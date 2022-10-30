using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T104.Store.Logging.Models;

namespace T104.Store.Engine.DbContexts
{
    public class LoggerEfSqlServerDbContext : DbContext
    {
        public LoggerEfSqlServerDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=37.140.192.97;Database=u1325524_LoggingDb;User ID=u1325524_LoggingAdmin;Password=3m%4hR0d;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LoggerMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                /*
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.FullName).HasMaxLength(30);
                */
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
