using CLI.UI.ManagePost;
using RepositoryContracts;
using CLI.UI.ManageUsers;

namespace CLI.UI;

public class CliApp
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    
    private readonly ManageUsersView manageUsersView;
    private readonly ManagePostsView managePostsView;

    public CliApp(IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
         this.userRepository = userRepository;
         this.postRepository = postRepository; 
         this.commentRepository = commentRepository;
         
         manageUsersView = new ManageUsersView(userRepository);
         managePostsView = new ManagePostsView(postRepository);
    }

    public async Task StartAsync()
    {
        
    }
       
}