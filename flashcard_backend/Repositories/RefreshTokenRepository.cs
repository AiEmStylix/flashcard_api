using flashcard_backend.DatabaseContext;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flashcard_backend.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RefreshTokenRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<RefreshToken>> GetAllRefreshToken()
    {
        return await _dbContext.RefreshTokens.ToListAsync();
    }

    public async Task<RefreshToken?> GetRefreshToken(string token)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(token));
    }

    public async Task<RefreshToken?> CreateRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<bool> DeleteRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Remove(refreshToken);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}