using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }

    public DateTime? BirthDate { get; set; }

    public Gender? Gender { get; set; }

    public bool IsBanned { get; set; } = false;

    public bool IsDeleted { get; set; } = false;

    public bool RememberMe { get; set; }

    public AdminEntity? Admin { get; set; }

    public ManagerEntity? Manager { get; set; }

    public ICollection<GroupAdminEntity>? GroupAdmin { get; set; } = new List<GroupAdminEntity>();

    public ICollection<GroupManagerEntity>? GroupManager { get; set; } = new List<GroupManagerEntity>();

    public ICollection<GroupMemberEntity>? GroupMember { get; set; } = new List<GroupMemberEntity>();

    public ICollection<DiscussionMemberEntity>? DiscussionMember { get; set; } = new List<DiscussionMemberEntity>();

    public ICollection<DiscussionManagerEntity>? DiscussionManager { get; set; } = new List<DiscussionManagerEntity>();

    public ICollection<DiscussionAdminEntity>? DiscussionAdmin { get; set; } = new List<DiscussionAdminEntity>();

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}