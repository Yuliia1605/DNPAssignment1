using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    
    public SinglePostView(IPostRepository postRepository, 
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter post Id:");
        string? postIdInput = Console.ReadLine();
        
        int postId;
        if (!int.TryParse(postIdInput, out postId))
        {
            Console.WriteLine("Invalid post id.");
            return;
        }
        
        Post post;
        try
        {
            post = await postRepository.GetSingleAsync(postId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Post does not exist.");
            return;
        }
        
        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        
        IQueryable<Comment> comments = commentRepository.GetMany()
            .Where(c => c.PostId == postId);
        
        Console.WriteLine($"Comments:");
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"{comment.UserId}: {comment.Body}");
        }
    }
}