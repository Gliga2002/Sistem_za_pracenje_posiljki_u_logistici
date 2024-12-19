using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

public class AuthService
{
    private readonly IJSRuntime _jsRuntime;
    public bool IsLoggedIn { get; private set; }
    public event Action OnAuthStateChanged;

    public AuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Proverava stanje prijave
    public async Task CheckLoginStatusAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authToken");
        IsLoggedIn = !string.IsNullOrEmpty(token);
        NotifyAuthStateChanged();
    }

    // Prijavljuje korisnika i postavlja token
    public async Task LoginAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", token);
        IsLoggedIn = true;
        NotifyAuthStateChanged();
    }

    // Odjavljuje korisnika
    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
        IsLoggedIn = false;
        NotifyAuthStateChanged();
    }

    private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();
}