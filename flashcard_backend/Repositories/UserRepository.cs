using flashcard_backend.DatabaseContext;
using flashcard_backend.DTOs;
using flashcard_backend.Interfaces;
using flashcard_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flashcard_backend.Repositories;

public class UserRepository : IUserRepository
{
   private readonly ApplicationDbContext _context;

   public UserRepository(ApplicationDbContext context)
   {
      _context = context;
   }

   public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
   {
      return await _context.Users.ToListAsync();
   }

   public async Task<UserModel?> GetUserByIdAsync(int id)
   {
      return await _context.Users.FindAsync(id);
   }

   public async Task<UserModel?> GetUserByUserName(string username)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
   }

   public async Task<UserModel?> GetUserByEmail(string email)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
   }

   public async Task<UserModel> CreateUserAsync(UserModel user)
   {
      bool isExists = await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email);
      if (isExists)
      {
         return null;
      }
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return user;
   }

   public async Task<UserModel> UpdateUserAsync(int id, UserModel user)
   {
      var existingUser = await _context.Users.FindAsync(id);
      if (existingUser == null)
      {
         return null;
      }
      
      //Update properties
      existingUser.FullName = user.FullName;
      existingUser.Role = user.Role;
      // existingUser.Status = user.Status;

      await _context.SaveChangesAsync();
      return existingUser;
   }

   public async Task<bool> DeleteUserAsync(int id)
   {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
         return false;
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();
      return true;
   }

   public async Task<DeleteUserResultDto> DeleteMutipleUsersAsync(List<int> uIds)
   {
      var usersToDelete = await _context.Users
         .Where(u => uIds.Contains(u.Id))
         .ToListAsync();

      var foundIds = usersToDelete.Select(u => u.Id).ToList();
      var notFoundIds = uIds.Except(foundIds).ToList();
      _context.Users.RemoveRange(usersToDelete);
      _context.SaveChangesAsync();

      return new DeleteUserResultDto()
      {
         Deleted = foundIds,
         NotFound = notFoundIds
      };
   }
}