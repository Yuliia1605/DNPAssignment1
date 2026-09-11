using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter a title:");
        string? title = Console.ReadLine();
        
        Console.WriteLine("Enter a body:");
        string? body = Console.ReadLine();
        
        Console.WriteLine("Enter a user Id:");
        string? userId = Console.ReadLine();
        
        if (!int.TryParse(userId, out int parsedUserId))
        {
            Console.WriteLine("Invalid user id.");
            return;
        }

        try
        {
            await userRepository.GetSingleAsync(parsedUserId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("User does not exist.");
            return;
        }

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = parsedUserId,
        };
        Post created = await postRepository.AddAsync(post);
        Console.WriteLine($"Post {created.Title} created with id {created.Id}");
    }
    
}