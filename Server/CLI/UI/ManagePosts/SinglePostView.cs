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
        
        int postId = int.Parse(postIdInput);
        
        Post post = await postRepository.GetSingleAsync(postId);
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