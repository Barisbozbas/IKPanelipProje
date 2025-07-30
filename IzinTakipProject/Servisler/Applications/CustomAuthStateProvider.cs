using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using IzinTakipProject.Data.Entities;
using Blazored.LocalStorage;




public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public CustomAuthStateProvider(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        try
        {
            var userInfo = await _localStorage.GetItemAsync<LoginResult>("user");
            if (userInfo != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                    new Claim(ClaimTypes.Name, userInfo.UserName),
                    new Claim(ClaimTypes.Email, userInfo.Email)
                }, "Server authentication");

                foreach (var role in userInfo.Roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task MarkUserAsAuthenticated(LoginResult userInfo)
    {
        await _localStorage.SetItemAsync("user", userInfo);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("user");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}