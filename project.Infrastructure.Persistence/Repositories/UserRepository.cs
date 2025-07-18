using project.Application;
using project.Application.Abstractions;
using project.Application.Abstractions.Repositories;
using project.Application.Models;
using project.Application.Models.DbModels;

namespace project.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationContext db) : IUserRepository
{
    public async Task CreateUser(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task<User> GetUserById(Guid id) => await db.Users.FindAsync(id) ?? throw new Exception("User not found");
}