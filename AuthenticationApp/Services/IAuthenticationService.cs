using System.Threading.Tasks;
using AuthenticationApp.Models;

namespace AuthenticationApp.Services;

public interface IAuthenticationService
{
    Task<bool> RegisterAsync(string firstName, string lastName, string email, string password);
    Task<User?> LoginAsync(string email, string password);
    Task<bool> UserExistsAsync(string email);
}