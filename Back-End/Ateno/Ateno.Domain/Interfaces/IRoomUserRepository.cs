using System.Threading.Tasks;

namespace Ateno.Domain.Interfaces
{
    public interface IRoomUserRepository
    {
        bool AccessAllowed(int roomId, string userId);
        Task<bool> Link(int roomId, string email);
        Task<bool> EnterRoom(string roomCode, string userId);
        Task<bool> Unlink(int roomId, string email, string userId);
        Task<bool> Unlink(int roomId, string userId);
    }
}
