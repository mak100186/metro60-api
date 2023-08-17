using Metro60.Core.Data;
using Metro60.Core.Extensions;
using Metro60.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace Metro60.Core.Services;

public interface IUserService
{
    Task<List<UserModel>> GetAll();
    Task<UserModel?> GetUser(string username, string password);
}

public class UserService : IUserService
{
    private readonly MetroDbContext _context;

    public UserService(MetroDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserModel>> GetAll()
    {
        return await _context.Users
            .Select(user => user.ToDto())
            .ToListAsync();
    }

    public async Task<UserModel?> GetUser(string username, string password)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

        return user?.ToDto();
    }
}
