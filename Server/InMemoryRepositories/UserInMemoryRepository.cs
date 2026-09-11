using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users = new();

    public UserInMemoryRepository()
    {
        users.Add(new User
        {
            Id = 1,
            Username = "Anna",
            Password = "1234"
        });

        users.Add(new User
        {
            Id = 2,
            Username = "Piter",
            Password = "5678"
        });

        users.Add(new User
        {
            Id = 3,
            Username = "Din",
            Password = "9876"
        });
    }

    public Task<User> AddAsync(User user)
    {
        int newId = 1;

        foreach (User existingUser in users)
        {
            if (existingUser.Id >= newId)
            {
                newId = existingUser.Id + 1;
            }
        }

        user.Id = newId;
        users.Add(user);

        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = null;

        foreach (User existing in users)
        {
            if (existing.Id == user.Id)
            {
                existingUser = existing;
                break;
            }
        }

        if (existingUser == null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = null;

        foreach (User user in users)
        {
            if (user.Id == id)
            {
                userToRemove = user;
                break;
            }
        }

        if (userToRemove == null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);

        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? foundUser = null;

        foreach (User user in users)
        {
            if (user.Id == id)
            {
                foundUser = user;
                break;
            }
        }

        if (foundUser == null)
        {
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }

        return Task.FromResult(foundUser);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}