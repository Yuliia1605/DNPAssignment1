using RepositoryContracts;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }
    
    public IQueryable<Comment> GetMany() {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
    
    public async Task UpdateAsync(Comment comment)
{
    string commentsAsJson = await File.ReadAllTextAsync(filePath);
    List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

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

    commentsAsJson = JsonSerializer.Serialize(comments);
    await File.WriteAllTextAsync(filePath, commentsAsJson);
}

public async Task DeleteAsync(int id)
{
    string commentsAsJson = await File.ReadAllTextAsync(filePath);
    List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

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

    commentsAsJson = JsonSerializer.Serialize(comments);
    await File.WriteAllTextAsync(filePath, commentsAsJson);
}

public async Task<Comment> GetSingleAsync(int id)
{
    string commentsAsJson = await File.ReadAllTextAsync(filePath);
    List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

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

    return foundComment;
}
}