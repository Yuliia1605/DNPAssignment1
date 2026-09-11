using CLI.UI.ManagePosts;
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
         managePostsView = new ManagePostsView(postRepository, commentRepository,
             userRepository);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("Main Menu");
                    Console.WriteLine("1. Manage users");
                    Console.WriteLine("2. Manage posts");
                    Console.WriteLine("0. Exit");
                    
                    string? choice = Console.ReadLine();
            
                    if (choice == "1")
                    {
                        await manageUsersView.ShowAsync();
                    }
                    else if (choice == "2")
                    {
                        await managePostsView.ShowAsync();
                    }
                    else if (choice == "0")
                    {
                        return;
                    }
        }
        
    }
       
}