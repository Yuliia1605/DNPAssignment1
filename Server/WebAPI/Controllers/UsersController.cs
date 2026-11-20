
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        bool usernameTaken = await userRepo.GetMany()
            .AnyAsync(user => user.Username.ToLower() == request.UserName.ToLower());

        if (usernameTaken)
        {
            return BadRequest("Username is already taken.");
        }

        User user = new()
        {
            Username = request.UserName,
            Password = request.Password
        };

        User created = await userRepo.AddAsync(user);

        UserDto dto = new()
        {
            Id = created.Id,
            UserName = created.Username
        };

        return Created($"/Users/{dto.Id}", dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetSingle([FromRoute] int id)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);

            UserDto dto = new()
            {
                Id = user.Id,
                UserName = user.Username
            };

            return Ok(dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetMany([FromQuery] string? username)
    {
        IQueryable<User> users = userRepo.GetMany();

        if (!string.IsNullOrWhiteSpace(username))
        {
            users = users.Where(user =>
                user.Username.ToLower().Contains(username.ToLower()));
        }

        List<UserDto> dtos = await users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.Username
        }).ToListAsync();

        return Ok(dtos);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute] int id,
        [FromBody] UpdateUserDto request)
    {
        User user = await userRepo.GetSingleAsync(id);

        user.Username = request.UserName;
        user.Password = request.Password;

        await userRepo.UpdateAsync(user);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        await userRepo.DeleteAsync(id);
        return NoContent();
    }
}
