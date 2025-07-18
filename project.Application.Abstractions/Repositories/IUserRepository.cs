using project.Application.Models.DbModels;

namespace project.Application.Abstractions.Repositories;

public interface IUserRepository
{
    public Task CreateUser(User user);

    public Task<User> GetUserById(Guid id);
}