using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    public Task<RefreshToken?> GetAsync(string refreshToken);
    public Task CreateAsync(RefreshToken refreshToken);
    public Task DeleteAsync(RefreshToken refreshToken);
    public Task DeleteByStringAsync(string? refreshToken);
    public Task<List<RefreshToken>> GetByUserIdAsync (Guid userId);
    public Task<bool> ExistsAsync(string? refreshToken);
}