using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    
    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
}