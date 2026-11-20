using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddAsync(Post post)
    {
        var entry = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await ctx.Posts.FindAsync(id);

        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {id} not found");
        }

        ctx.Posts.Remove(post);
        await ctx.SaveChangesAsync();
    }
    public async Task<Post> GetSingleAsync(int id)
    {
        var post = await ctx.Posts.FindAsync(id);

        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {id} not found");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.AsQueryable();
    }
}