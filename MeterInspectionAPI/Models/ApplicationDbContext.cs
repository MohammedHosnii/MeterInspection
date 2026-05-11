using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MeterInspectionAPI.Models.Identity;
using MeterInspectionAPI.Models.Identity.Permissions;
using System.Reflection.Emit;
using System.Security;

namespace MeterInspectionAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<PermissionsType> Permissions { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize Identity tables names
            //builder.Entity<ApplicationUser>().ToTable("Users");
            //builder.Entity<IdentityRole>().ToTable("Roles");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            //builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            //builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            //builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // Configure RefreshToken entity
            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(rt => rt.Token);

                // Configure relationship with User
                entity.HasOne(rt => rt.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(rt => rt.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure properties
                entity.Property(rt => rt.Token).IsRequired();
                entity.Property(rt => rt.ExpiryDate).IsRequired();
                entity.Property(rt => rt.IsRevoked).IsRequired();
                entity.Property(rt => rt.UserId).IsRequired();
            });

            // Optional: Configure the table name and column properties
            builder.Entity<PermissionsType>(entity =>
            {
                entity.ToTable("PermissionsTypes");
                entity.Property(p => p.GroupName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.PermissionNameAr)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.PermissionNameEn)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // Configure the UserDepartment table
            builder.Entity<UserDepartment>(entity =>
            {
                entity.ToTable("UserDepartments");

                entity.HasKey(ud => ud.Id); // Primary Key

                entity.Property(ud => ud.DepartmentCode)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(ud => ud.User)
                      .WithMany() // No navigation collection in IdentityUser
                      .HasForeignKey(ud => ud.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Define delete behavior
            });
        }
    }
}
