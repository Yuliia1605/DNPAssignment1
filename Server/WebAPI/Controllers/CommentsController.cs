using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;
    private readonly IUserRepository userRepo;
    private readonly IPostRepository postRepo;

    public CommentsController(
        ICommentRepository commentRepo,
        IUserRepository userRepo,
        IPostRepository postRepo)
    {
        this.commentRepo = commentRepo;
        this.userRepo = userRepo;
        this.postRepo = postRepo;
    }
    
    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment(
        [FromBody] CreateCommentDto request)
    {
        bool userExists = userRepo.GetMany()
            .Any(user => user.Id == request.UserId);

        if (!userExists)
        {
            return BadRequest("User does not exist.");
        }

        bool postExists = postRepo.GetMany()
            .Any(post => post.Id == request.PostId);

        if (!postExists)
        {
            return BadRequest("Post does not exist.");
        }
        
        Comment comment = new()
        {
            Body = request.Body,
            UserId = request.UserId,
            PostId = request.PostId
        };

        Comment created = await commentRepo.AddAsync(comment);

        CommentDto dto = new()
        {
            Id = created.Id,
            Body = created.Body,
            UserId = created.UserId,
            PostId = created.PostId
        };

        return Created($"/Comments/{dto.Id}", dto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDto>> GetSingle([FromRoute] int id)
    {
        Comment comment = await commentRepo.GetSingleAsync(id);

        CommentDto dto = new()
        {
            Id = comment.Id,
            Body = comment.Body,
            UserId = comment.UserId,
            PostId = comment.PostId
        };

        return Ok(dto);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<CommentDto>> GetMany(
        [FromQuery] int? userId,
        [FromQuery] int? postId)
    {
        IQueryable<Comment> comments = commentRepo.GetMany();

        if (userId.HasValue)
        {
            comments = comments.Where(comment => comment.UserId == userId.Value);
        }

        if (postId.HasValue)
        {
            comments = comments.Where(comment => comment.PostId == postId.Value);
        }

        IEnumerable<CommentDto> dtos = comments.Select(comment => new CommentDto
        {
            Id = comment.Id,
            Body = comment.Body,
            UserId = comment.UserId,
            PostId = comment.PostId
        });

        return Ok(dtos);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateComment(
        [FromRoute] int id,
        [FromBody] UpdateCommentDto request)
    {
        Comment comment = await commentRepo.GetSingleAsync(id);

        comment.Body = request.Body;
        comment.UserId = request.UserId;
        comment.PostId = request.PostId;

        await commentRepo.UpdateAsync(comment);

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteComment([FromRoute] int id)
    {
        await commentRepo.DeleteAsync(id);
        return NoContent();
    }
}