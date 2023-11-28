using Ateno.Domain.Entities;
using Ateno.Domain.Enum;
using Ateno.Domain.Interfaces;
using Ateno.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.Repositories
{
    public class StudyDeckRepository : IStudyDeckRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudyDeckRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<StudyDeck> LoadUserDecks(string userId)
        {
            try
            {
                List<StudyDeck> decks = _applicationDbContext.StudyDeck.Where(x => x.UserId == userId).AsNoTracking().ToList();
                return decks;
            }
            catch
            {
                return null;
            }
        }

        public List<StudyDeck> LoadRoomDecks(int roomId, string userId)
        {
            try
            {
                List<StudyDeck> decks = _applicationDbContext.StudyDeck.Where(x => x.RoomId == roomId && x.Room.RoomUsers.Where(y => y.RoomId == roomId && y.UserId == userId).Count() > 0).AsNoTracking().ToList();
                return decks;
            }
            catch
            {
                return null;
            }
        }

        public StudyDeck GetStudyDeck(int studyDeckId, string userId, bool loadCards = false)
        {
            try
            {
                if (loadCards)
                    return _applicationDbContext.StudyDeck.Where(x => x.Id == studyDeckId && (x.UserId == userId || x.Room.AdminId == userId)).Include(x => x.StudyCards).FirstOrDefault();
                else
                    return _applicationDbContext.StudyDeck.Where(x => x.Id == studyDeckId && (x.UserId == userId || x.Room.AdminId == userId)).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> Create(StudyDeck studyDeck)
        {
            try
            {
                _applicationDbContext.StudyDeck.Add(studyDeck);
                await _applicationDbContext.SaveChangesAsync();
                return studyDeck.Id;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<bool> UpdateName(StudyDeck studyDeck, string userId)
        {
            try
            {
                _applicationDbContext.StudyDeck.Update(studyDeck);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Permission AccessAllowed(int studyDeckId, string userId)
        {
            try
            {
                StudyDeck studyDeck = _applicationDbContext.StudyDeck.Where(x => x.Id == studyDeckId && (x.UserId == userId || x.Room.AdminId == userId || x.Room.RoomUsers.Where(y => y.RoomId == x.RoomId && y.UserId == userId).Count() > 0)).Include(x => x.Room).FirstOrDefault();
                if (studyDeck == null)
                    return Permission.Unknown;
                if (studyDeck.UserId == userId)
                    return Permission.Admin;
                if (studyDeck.Room.AdminId == userId)
                    return Permission.Admin;
                return Permission.User;
            }
            catch
            {
                return Permission.Unknown;
            }
        }

        public async Task<bool> Remove(int studyDeckId, string userId)
        {
            try
            {
                StudyDeck remove = _applicationDbContext.StudyDeck.Where(x => x.Id == studyDeckId && (x.UserId == userId || x.Room.AdminId == userId)).FirstOrDefault();
                if (remove == null)
                    return false;
                _applicationDbContext.StudyDeck.Remove(remove);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetRoomIdByDeck(int studyDeckId)
        {
            return _applicationDbContext.StudyDeck.Where(x => x.Id == studyDeckId).Select(x => x.RoomId).FirstOrDefault() ?? 0;
        }

        public async Task<int> StudentsCount(int studyDeckId)
        {
            return await _applicationDbContext.StudyProcess.Where(x => x.StudyDeckId == studyDeckId).Select(x => x.UserId).Distinct().CountAsync();
        }
    }
}
