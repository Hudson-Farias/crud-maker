using Microsoft.EntityFrameworkCore;

using Database;
using UsersAPI.Models;

namespace UsersAPI.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(int id);
    Task Add(User user);
    Task Update(User user);
    Task Delete(int id);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
