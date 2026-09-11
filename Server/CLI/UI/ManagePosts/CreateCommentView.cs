using Entities;
using RepositoryContracts;
namespace CLI.UI.ManagePosts;

public class CreateCommentView
{
   private readonly ICommentRepository commentRepository;
   private readonly IUserRepository userRepository;
   private readonly IPostRepository postRepository;
   
   public CreateCommentView(ICommentRepository? commentRepository,
      IUserRepository userRepository, IPostRepository postRepository)
   {
      this.commentRepository = commentRepository;
      this.userRepository = userRepository;
      this.postRepository = postRepository;
   }

   public async Task ShowAsync()
   {
      Console.WriteLine("Enter comment body:");
      string? body = Console.ReadLine();
      
      Console.WriteLine("Enter user Id:");
      string? userIdInput = Console.ReadLine();
      
      Console.WriteLine("Enter post Id:");
      string? postIdInput = Console.ReadLine();
      
      if (!int.TryParse(userIdInput, out int userId))
      {
         Console.WriteLine("Invalid user id.");
         return;
      }

      if (!int.TryParse(postIdInput, out int postId))
      {
         Console.WriteLine("Invalid post id.");
         return;
      }

      try
      {
         await userRepository.GetSingleAsync(userId);
      }
      catch (InvalidOperationException)
      {
         Console.WriteLine("User does not exist.");
         return;
      }

      try
      {
         await postRepository.GetSingleAsync(postId);
      }
      catch (InvalidOperationException)
      {
         Console.WriteLine("Post does not exist.");
         return;
      }

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