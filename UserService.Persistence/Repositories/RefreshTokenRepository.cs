using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly UserDbContext _context;

    public RefreshTokenRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetAsync(string refreshToken)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
    }

    public async Task CreateAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RefreshToken>> GetByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();
    }
}