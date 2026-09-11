using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly SinglePostView singlePostView;
    private readonly CreateCommentView createCommentView;

    public ManagePostsView(IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;

        createPostView = new CreatePostView(postRepository);
        listPostsView = new ListPostsView(postRepository);
        singlePostView = new SinglePostView(postRepository, 
            commentRepository);
        createCommentView = new CreateCommentView(commentRepository);
    }
    
    public async Task ShowAsync()
    {
        while (true)
        {
            Console.WriteLine("Post Management");
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. List Posts");
            Console.WriteLine("3. Single Post");
            Console.WriteLine("4. Add comment");
            Console.WriteLine("0. Back");
            
            string? choice = Console.ReadLine();
            
            if (choice == "1")
            {
                await createPostView.ShowAsync();
            }
            else if (choice == "2")
            {
                listPostsView.Show();
            }
            else if (choice == "3")
            {
                await singlePostView.ShowAsync();
            }
            else  if (choice == "4")
            {
                await createCommentView.ShowAsync();
            }
            else if (choice == "0")
            {
                return;
            }
        }
    }
}