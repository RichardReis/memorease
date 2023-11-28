using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using Ateno.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.Repositories
{
    public class RoomUserRepository : IRoomUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoomUserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool AccessAllowed(int roomId, string userId)
        {
            try
            {
                return (_applicationDbContext.RoomUser.Where(x => x.RoomId == roomId && x.UserId == userId).Count() > 0);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Link(int roomId, string email)
        {
            try
            {
                if (_applicationDbContext.RoomUser.Where(x => x.RoomId == roomId && x.User.Email == email).Count() > 0)
                    return false;
                string userId = _applicationDbContext.User.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();
                if (userId == null)
                    return false;
                _applicationDbContext.RoomUser.Add(new RoomUser(0, roomId, userId, DateTime.Now));
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EnterRoom(string roomCode, string userId)
        {
            try
            {
                if (_applicationDbContext.RoomUser.Where(x => x.Room.Code == roomCode && x.UserId == userId).Count() > 0)
                    return false;
                int roomId = _applicationDbContext.Room.Where(x => x.Code == roomCode && x.IsPublic == true).Select(x => x.Id).FirstOrDefault();
                if (roomId == 0)
                    return false;
                _applicationDbContext.RoomUser.Add(new RoomUser(0, roomId, userId, DateTime.Now));
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Unlink(int roomId, string email, string userId)
        {
            try
            {
                RoomUser remove = _applicationDbContext.RoomUser.Where(x => x.RoomId == roomId && x.User.Email == email && x.UserId != userId).FirstOrDefault();
                if (remove == null)
                    return true;
                List<StudyProcess> list = _applicationDbContext.StudyProcess.Where(x => x.StudyDeck.RoomId == roomId && x.UserId == userId).ToList();
                _applicationDbContext.RoomUser.Remove(remove);
                foreach (StudyProcess item in list)
                    _applicationDbContext.StudyProcess.Remove(item);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Unlink(int roomId, string userId)
        {
            try
            {
                RoomUser remove = _applicationDbContext.RoomUser.Where(x => x.RoomId == roomId && x.UserId == userId && x.Room.AdminId != userId).FirstOrDefault();
                if (remove == null)
                    return true;
                List<StudyProcess> list = _applicationDbContext.StudyProcess.Where(x => x.StudyDeck.RoomId == roomId && x.UserId == userId).ToList();
                _applicationDbContext.RoomUser.Remove(remove);
                foreach (StudyProcess item in list)
                    _applicationDbContext.StudyProcess.Remove(item);
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
