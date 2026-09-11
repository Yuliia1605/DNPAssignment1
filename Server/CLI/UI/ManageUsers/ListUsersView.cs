using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;
    
    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void Show()
    {
        IQueryable<User> users = userRepository.GetMany();

        foreach (User user in users)
        {
            Console.WriteLine($"{user.Id}: {user.Username}");
        }
    }
}