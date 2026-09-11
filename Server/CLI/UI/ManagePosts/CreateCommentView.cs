using Entities;
using RepositoryContracts;
namespace CLI.UI.ManagePosts;

public class CreateCommentView
{
   private readonly ICommentRepository commentRepository;
   
   public CreateCommentView(ICommentRepository commentRepository)
   {
      this.commentRepository = commentRepository;
   }

   public async Task ShowAsync()
   {
      Console.WriteLine("Enter comment body:");
      string? body = Console.ReadLine();
      
      Console.WriteLine("Enter user Id:");
      string? userIdInput = Console.ReadLine();
      
      Console.WriteLine("Enter post Id:");
      string? postIdInput = Console.ReadLine();
      
      int userId = int.Parse(userIdInput);
      int postId = int.Parse(postIdInput);

      Comment comment = new Comment
      {
         Body = body,
         UserId = userId,
         PostId = postId
      };
      
      Comment created = await commentRepository.AddAsync(comment);
      Console.WriteLine($"Comment created with id {created.Id}");
   }
}