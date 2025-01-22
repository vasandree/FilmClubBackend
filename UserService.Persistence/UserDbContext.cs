using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Persistence;

public class UserDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<AdminEntity> Admins { get; set; }
    public DbSet<ManagerEntity> Managers { get; set; }
    public DbSet<GroupAdminEntity> GroupAdmins { get; set; }
    public DbSet<GroupManagerEntity> GroupManagers { get; set; }
    public DbSet<GroupMemberEntity> GroupMembers { get; set; }
    public DbSet<DiscussionAdminEntity> DiscussionAdmins { get; set; }
    public DbSet<DiscussionManagerEntity> DiscussionManagers { get; set; }
    public DbSet<DiscussionMemberEntity> DiscussionMembers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ConfirmCode> ConfirmCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(rt => rt.Token)
                .IsUnique();
        });


        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.UserName).IsUnique();
        });


        modelBuilder.Entity<AdminEntity>(entity =>
        {
            entity.HasKey(x => x.UserId);

            entity.HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<AdminEntity>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<ManagerEntity>(entity =>
        {
            entity.HasKey(x => x.UserId);

            entity.HasOne(a => a.User)
                .WithOne(u => u.Manager)
                .HasForeignKey<ManagerEntity>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<GroupAdminEntity>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.ResourceId });

            entity.HasOne(a => a.User)
                .WithMany(u => u.GroupAdmin)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<GroupMemberEntity>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.ResourceId });

            entity.HasOne(a => a.User)
                .WithMany(u => u.GroupMember)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<GroupManagerEntity>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.ResourceId });

            entity.HasOne(a => a.User)
                .WithMany(u => u.GroupManager)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<DiscussionManagerEntity>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.ResourceId });

            entity.HasOne(a => a.User)
                .WithMany(u => u.DiscussionManager)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DiscussionMemberEntity>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.ResourceId });

            entity.HasOne(a => a.User)
                .WithMany(u => u.DiscussionMember)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DiscussionAdminEntity>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.ResourceId });

            entity.HasOne(a => a.User)
                .WithMany(u => u.DiscussionAdmin)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ConfirmCode>()
            .HasKey(x => new { x.UserId, x.Code });
    }
}