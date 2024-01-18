
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DAL
{
    public class MyDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Data Source=Localhost\SQLEXPRESS;Database=GYMAPP;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
        }
        public override int SaveChanges()
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
                    {
                        var identityColumn = entry.Metadata.FindPrimaryKey().Properties
                            .FirstOrDefault(p => p.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd);

                        if (identityColumn != null)
                        {
                            // Enable IDENTITY_INSERT for the table
                            Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {entry.Metadata.GetTableName()} ON");
                            break;  // Break out of the loop after the first entity with identity column is found
                        }
                    }

                    var result = base.SaveChanges();

                    // Disable IDENTITY_INSERT after the insert operation
                    foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
                    {
                        var identityColumn = entry.Metadata.FindPrimaryKey().Properties
                            .FirstOrDefault(p => p.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd);

                        if (identityColumn != null)
                        {
                            // Disable IDENTITY_INSERT for the table
                            Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {entry.Metadata.GetTableName()} OFF");
                            break;  // Break out of the loop after the first entity with identity column is found
                        }
                    }

                    transaction.Commit();

                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Rethrow the exception after rolling back the transaction
                }
            }
        }
    }
}
