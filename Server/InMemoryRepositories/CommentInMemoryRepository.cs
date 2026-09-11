using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments = new();

    public Task<Comment> AddAsync(Comment comment)
    {
        int newId = 1;

        foreach (Comment existingComment in comments)
        {
            if (existingComment.Id >= newId)
            {
                newId = existingComment.Id + 1;
            }
        }

        comment.Id = newId;
        comments.Add(comment);

        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = null;

        foreach (Comment existing in comments)
        {
            if (existing.Id == comment.Id)
            {
                existingComment = existing;
                break;
            }
        }

        if (existingComment == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = null;

        foreach (Comment comment in comments)
        {
            if (comment.Id == id)
            {
                commentToRemove = comment;
                break;
            }
        }

        if (commentToRemove == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);

        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? foundComment = null;

        foreach (Comment comment in comments)
        {
            if (comment.Id == id)
            {
                foundComment = comment;
                break;
            }
        }

        if (foundComment == null)
        {
            throw new InvalidOperationException(
                $"Comment with ID '{id}' not found");
        }

        return Task.FromResult(foundComment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}