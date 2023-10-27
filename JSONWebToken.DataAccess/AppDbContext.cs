using JSONWebToken.Entities.Enums;
using JSONWebToken.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSONWebToken.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role()
                    {
                        RoleId = 1,
                        RoleName = "Admin",
                        Key = ERole.admin.ToString(),
                    },
                      new Role()
                      {
                          RoleId = 2,
                          RoleName = "User",
                          Key = ERole.user.ToString(),
                      },
                        new Role()
                        {
                            RoleId = 3,
                            RoleName = "SuperAdmin",
                            Key = ERole.superadmin.ToString(),
                        }
                    );
        }
    }
}