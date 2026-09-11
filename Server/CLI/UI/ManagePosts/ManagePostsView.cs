using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly CreatePostView createPostView;
    private readonly ListPostsView listPostsView;
    private readonly SinglePostView singlePostView;

    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;

        createPostView = new CreatePostView(postRepository);
        listPostsView = new ListPostsView(postRepository);
        singlePostView = new SinglePostView(postRepository);
    }
}