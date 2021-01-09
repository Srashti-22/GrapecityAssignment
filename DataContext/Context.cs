using GrapecityAssignment.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapecityAssignment.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=Blogging;Integrated Security=True");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetailsModel>().ToTable("UserDetails", "dbo").HasMany(c => c.Post).WithOne(e => e.UserDetails);
            modelBuilder.Entity<PostModel>().HasMany(c => c.Comments).WithOne(e => e.Post);
            modelBuilder.Entity<CommentsModel>().ToTable("Comments", "dbo");
        }

        public DbSet<UserDetailsModel> UserDetails { get; set; }
        public DbSet<PostModel> Post { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
    }
}
