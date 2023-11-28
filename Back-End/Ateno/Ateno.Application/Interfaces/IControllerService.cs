using Ateno.Application.DTOs;

namespace Ateno.Application.Interfaces
{
    public interface IControllerService
    {
        HomeDTO LoadHome(string userId);
        HomeRoomDTO LoadRoom(int roomId, string userId);
    }
}
