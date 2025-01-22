using UserService.Domain.Enums;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class BaseMembershipEntityRepository : IBaseMembershipEntityRepository
{
    private readonly UserDbContext _context;

    public BaseMembershipEntityRepository(UserDbContext context)
    {
        _context = context;
    }


    public List<Guid> GetListOfResourceId(Guid userId, Role role)
    {
        switch (role)
        {
            case Role.DiscussionAdmin:
                return _context.DiscussionAdmins
                    .Where(e => e.UserId == userId)
                    .Select(e => e.ResourceId)
                    .ToList();

            case Role.DiscussionManager:
                return _context.DiscussionManagers
                    .Where(e => e.UserId == userId)
                    .Select(e => e.ResourceId)
                    .ToList();

            case Role.DiscussionMember:
                return _context.DiscussionMembers
                    .Where(e => e.UserId == userId)
                    .Select(e => e.ResourceId)
                    .ToList();

            case Role.GroupAdmin:
                return _context.GroupAdmins
                    .Where(e => e.UserId == userId)
                    .Select(e => e.ResourceId)
                    .ToList();

            case Role.GroupManager:
                return _context.GroupManagers
                    .Where(e => e.UserId == userId)
                    .Select(e => e.ResourceId)
                    .ToList();

            case Role.GroupMember:
                return _context.GroupMembers
                    .Where(e => e.UserId == userId)
                    .Select(e => e.ResourceId)
                    .ToList();

            default:
                return [];
        }
    }
}