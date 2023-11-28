using Ateno.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Application.Interfaces
{
    public interface IRoomService
    {
        bool AccessAllowed(int roomId, string userId);
        Task<ResponseDTO> EnterRoom(string roomCode, string userId);
        List<RoomDTO> LoadAllRooms(string userId);
        List<UserDTO> LoadAllRoomUsers(int roomId, string userId);
        bool IsAdmin(int roomId, string userId);
        RoomDTO getRoom(int roomId, string userId);
        Task<ResponseDTO> Create(RoomDTO roomDTO);
        Task<ResponseDTO> Remove(int roomId, string userId);
        Task<ResponseDTO> Edit(RoomDTO roomDTO, string userId);
        Task<ResponseDTO> AddUser(int roomId, string email, string userId);
        Task<ResponseDTO> RemoveUser(int roomId, string email, string userId);
        Task<RoomDeckInfoDTO> RoomDeckInfo(int deckId, string userId);
    }
}