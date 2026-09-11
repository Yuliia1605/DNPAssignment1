using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter a title:");
        string? title = Console.ReadLine();
        
        Console.WriteLine("Enter a body:");
        string? body = Console.ReadLine();
        
        Console.WriteLine("Enter a user Id:");
        string? userId = Console.ReadLine();
        int parsedUserId = int.Parse(userId);

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