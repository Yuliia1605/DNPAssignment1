using Entities;
using RepositoryContracts;


namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter username:");
        string? username = Console.ReadLine();
        
        Console.WriteLine("Enter password:");
        string? password = Console.ReadLine();

        User user = new User
        {
            Username = username,
            Password = password
        };
        
        User created = await userRepository.AddAsync(user);
        Console.WriteLine($"User {created.Username} created with id {created.Id}");
        
    }
}