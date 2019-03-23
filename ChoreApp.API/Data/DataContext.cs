using ChoreApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos {get; set; }
        public DbSet<Network> Networks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Network>().HasKey(k => new {k.NetworkerId, k.NetworkeeId});

            builder.Entity<Network>().HasOne(u => u.Networkee).WithMany(u => u.Networkers).HasForeignKey(u => u.NetworkeeId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Network>().HasOne(u => u.Networker).WithMany(u => u.Networkees).HasForeignKey(u => u.NetworkerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}