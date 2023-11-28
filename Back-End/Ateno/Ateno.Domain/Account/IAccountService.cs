using System.Threading.Tasks;

namespace Ateno.Domain.Account
{
    public interface IAccountService
    {
        Task<bool> Authenticate(string userName, string password);
        Task<string> RegisterUser(string userName, string password);
        Task<string> ChangePassword(string userId, string currentPassword, string newPassword);
        Task<bool> ChangeEmail(string userId, string userName);
        Task Logout();
        Task<string> GenerateRefreshTokenUser(string userName);
    }
}
