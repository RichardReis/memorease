using Ateno.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Domain.Interfaces
{
    public interface IRoomRepository
    {
        List<Room> LoadUserRooms(string userId);
        Room LoadRoom(int roomId, string userId);
        Room getRoom(int roomId, string userId, bool loadUsers = true);
        bool IsAdmin(int roomId, string userId);
        bool IsPublic(string roomCode);
        Task<bool> Create(Room room);
        Task<bool> Update(Room room);
        Task<bool> Remove(int roomId, string userId);
    }
}