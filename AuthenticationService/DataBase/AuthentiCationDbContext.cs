using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.DataBase
{
    public class AuthentiCationDbContext:DbContext
    {
        public AuthentiCationDbContext(DbContextOptions<AuthentiCationDbContext> dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
        .HasKey(e => new { e.UserId, e.RoleId });

            //m:m
            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.User)
              .WithMany(u => u.UserRoles)
              .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
