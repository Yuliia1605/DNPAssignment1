using System.Net.Http.Json;
using System.Security.Claims;
using ApiContracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient client;
    private ClaimsPrincipal currentUser = new(new ClaimsIdentity());

    public SimpleAuthProvider(HttpClient client)
    {
        this.client = client;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(currentUser));
    }

    public async Task Login(string userName, string password)
    {
        LoginRequest request = new()
        {
            UserName = userName,
            Password = password
        };

        HttpResponseMessage response =
            await client.PostAsJsonAsync("auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid username or password.");
        }

        UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();

        if (user is null)
        {
            throw new Exception("Login response was empty.");
        }

        Claim[] claims =
        [
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("Id", user.Id.ToString())
        ];

        currentUser = new ClaimsPrincipal(
            new ClaimsIdentity(claims, "apiauth"));

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void Logout()
    {
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}