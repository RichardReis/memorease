using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using Ateno.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IRoomUserRepository _roomUserRepository;

        public RoomRepository(ApplicationDbContext applicationDbContext, IRoomUserRepository roomUserRepository)
        {
            _applicationDbContext = applicationDbContext;
            _roomUserRepository = roomUserRepository;
        }

        public List<Room> LoadUserRooms(string userId)
        {
            try
            {
                return _applicationDbContext.Room.Where(x => x.RoomUsers.Where(y => y.RoomId == x.Id && y.UserId == userId).Count() > 0).AsNoTracking().ToList();
            }
            catch
            {
                return null;
            }
        }

        public Room LoadRoom(int roomId, string userId)
        {
            try
            {
                return _applicationDbContext.Room.Where(x => x.Id == roomId && x.RoomUsers.Where(y => y.RoomId == roomId && y.UserId == userId).Count() > 0).AsNoTracking().FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public Room getRoom(int roomId, string userId, bool loadUsers = true)
        {
            try
            {
                if (loadUsers)
                    return _applicationDbContext.Room.Where(x => x.Id == roomId && x.AdminId == userId).Include(x => x.RoomUsers).ThenInclude(x => x.User).AsNoTracking().FirstOrDefault();
                else
                    return _applicationDbContext.Room.Where(x => x.Id == roomId && x.AdminId == userId).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public bool IsAdmin(int roomId, string userId)
        {
            try
            {
                return (_applicationDbContext.Room.Where(x => x.Id == roomId && x.AdminId == userId).Count() > 0);
            }
            catch
            {
                return false;
            }
        }

        public bool IsPublic(string roomCode)
        {
            try
            {
                return (_applicationDbContext.Room.Where(x => x.Code == roomCode && x.IsPublic == true).Count() > 0);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Create(Room room)
        {
            try
            {
                _applicationDbContext.Room.Add(room);
                await _applicationDbContext.SaveChangesAsync();
                room.GenerateCode();
                _applicationDbContext.Room.Update(room);
                await _applicationDbContext.SaveChangesAsync();
                _applicationDbContext.RoomUser.Add(new RoomUser(0, room.Id, room.AdminId, DateTime.Now));
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Remove(int roomId, string userId)
        {
            try
            {
                Room remove = _applicationDbContext.Room.Where(x => x.Id == roomId && x.AdminId == userId).Include(x => x.StudyDecks).FirstOrDefault();
                if (remove == null)
                    return false;
                foreach (StudyDeck item in remove.StudyDecks)
                    _applicationDbContext.StudyDeck.Remove(item);
                _applicationDbContext.Room.Remove(remove);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(Room room)
        {
            try
            {
                _applicationDbContext.Room.Update(room);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
