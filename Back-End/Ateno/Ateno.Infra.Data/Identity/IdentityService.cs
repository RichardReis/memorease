using Ateno.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.Identity
{
    public class IdentityService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Authenticate(string userName, string password)
        {
            try
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(userName,
                    password, true, lockoutOnFailure: false);

                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> RegisterUser(string userName, string password)
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = userName,
                    UserName = userName,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = await _userManager.CreateAsync(applicationUser, password);

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(applicationUser, "User");
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(applicationUser, isPersistent: true);
                        return applicationUser.Id;
                    }
                }
                return result.Errors.First().Code;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return null;
                IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (result.Succeeded)
                    return "OK";
                else
                    return result.Errors.First().Code;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> ChangeEmail(string userId, string userName)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return false;
                IdentityResult result = await _userManager.SetUserNameAsync(user, userName);
                if (!result.Succeeded)
                    return false;
                result = await _userManager.SetEmailAsync(user, userName);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GenerateRefreshTokenUser(string userName)
        {
            try
            {
                ApplicationUser user = await _signInManager
                                                .UserManager
                                                .FindByEmailAsync(userName);

                if(user == null) return null;


                string loginProvider = "JwtRefreshToken";
                string purpose = "RefreshToken";

                string refreshToken = await _signInManager
                                                .UserManager
                                                .GenerateUserTokenAsync(user, loginProvider, purpose);

                await _signInManager
                         .UserManager
                         .SetAuthenticationTokenAsync(user, loginProvider, purpose, refreshToken);

                return refreshToken;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
