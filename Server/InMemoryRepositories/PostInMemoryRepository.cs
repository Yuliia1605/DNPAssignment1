using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = new();

    public PostInMemoryRepository()
    {
        posts.Add(new Post
        {
            Id = 1,
            Title = "First post",
            Body = "Hello everyone!",
            UserId = 1
        });

        posts.Add(new Post
        {
            Id = 2,
            Title = "Second post",
            Body = "This is another post.",
            UserId = 2
        });

        posts.Add(new Post
        {
            Id = 3,
            Title = "Question",
            Body = "How is everyone doing?",
            UserId = 3
        });
    }

    public Task<Post> AddAsync(Post post)
    {
        int newId = 1;

        foreach (Post existingPost in posts)
        {
            if (existingPost.Id >= newId)
            {
                newId = existingPost.Id + 1;
            }
        }

        post.Id = newId;
        posts.Add(post);

        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = null;

        foreach (Post existing in posts)
        {
            if (existing.Id == post.Id)
            {
                existingPost = existing;
                break;
            }
        }

        if (existingPost == null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = null;

        foreach (Post post in posts)
        {
            if (post.Id == id)
            {
                postToRemove = post;
                break;
            }
        }

        if (postToRemove == null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);

        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? foundPost = null;

        foreach (Post post in posts)
        {
            if (post.Id == id)
            {
                foundPost = post;
                break;
            }
        }

        if (foundPost == null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        return Task.FromResult(foundPost);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}