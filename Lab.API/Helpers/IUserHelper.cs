using Lab.Shared.DTOs;
using Lab.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lab.API.Helpers
{
    //para que me consuma los usuarios
    //clase auxiliar que me ayuda manipular los usuarios
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();

    }
}

