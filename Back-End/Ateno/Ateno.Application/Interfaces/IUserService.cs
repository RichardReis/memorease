using Ateno.Application.DTOs;
using System.Threading.Tasks;

namespace Ateno.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseObjectDTO<TokenUserDTO>> Authentication(string userName, string password);
        string GetFirstName(string id);
        UserDTO GetById(string id);
        Task<ResponseDTO> Register(UserDTO userDTO, string password);
        Task<ResponseDTO> Update(string name, string email, string userId);
        Task<ResponseDTO> Delete(string userId);
        Task<ResponseDTO> ChangePassword(string userId, string currentPassword, string newPassword);
        Task Logout();
    }
}
