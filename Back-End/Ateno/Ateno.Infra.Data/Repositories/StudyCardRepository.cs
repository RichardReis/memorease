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
    public class StudyCardRepository : IStudyCardRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudyCardRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public StudyCard LoadStudyCard(int studyCardId, string userId)
        {
            try
            {
                return _applicationDbContext.StudyCard.Where(x => x.Id == studyCardId && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.AdminId == userId)).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Add(ICollection<StudyCard> studyCards)
        {
            try
            {
                foreach (StudyCard item in studyCards)
                    _applicationDbContext.StudyCard.Add(item);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(StudyCard studyCard)
        {
            try
            {
                _applicationDbContext.StudyCard.Update(studyCard);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetDeckIdByCard(int cardId, string userId)
        {
            try
            {
                return _applicationDbContext.StudyCard.Where(x => x.Id == cardId && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.RoomUsers.Where(y => y.UserId == userId).Count() > 0)).Select(x => x.StudyDeckId).FirstOrDefault();
            }
            catch
            {
                return 0;
            }
        }

        public bool HasStudy(int deckId, string userId)
        {
            try
            {
                return (_applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.RoomUsers.Where(y => y.UserId == userId).Count() > 0) &&
                (x.StudyProcesses.Where(y => y.UserId == userId && (y.NextStudy <= DateTime.Now || y.Learning == true)).Count() > 0 || x.StudyProcesses.Where(y => y.UserId == userId).Count() == 0)).Count() != 0);

            }
            catch
            {
                return false;
            }
        }

        public int InReview(int deckId, string userId)
        {
            try
            {
                return _applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && x.StudyProcesses.Where(y => y.UserId == userId && y.NextStudy <= DateTime.Now && y.Learning == false).Count() > 0).Count();
            }
            catch
            {
                return 0;
            }
        }

        public int InLearning(int deckId, string userId)
        {
            try
            {
                return _applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && (x.StudyProcesses.Where(y => y.UserId == userId && y.Learning == true).Count() > 0 || x.StudyProcesses.Where(y => y.UserId == userId).Count() == 0)).Count();
            }
            catch
            {
                return 0;
            }
        }

        public StudyCard GetNewStudy(int deckId, string userId)
        {
            try
            {
                List<StudyCard> studyCards = _applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && x.StudyProcesses.Where(y => y.UserId == userId).Count() == 0
                && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.RoomUsers.Where(y => y.UserId == userId).Count() > 0))
                    .Include(x => x.StudyDeck).Take(15).OrderBy(x => Guid.NewGuid()).AsNoTracking().ToList();
                if (studyCards.Count == 0)
                    return null;
                Random random = new Random();
                int rand = random.Next(1, studyCards.Count);
                return studyCards[rand -1];
            }
            catch
            {
                return null;
            }
        }

        public StudyCard GetReviewStudy(int deckId, string userId)
        {
            try
            {
                List<StudyCard> studyCards = _applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && x.StudyProcesses.Where(y => y.UserId == userId && y.NextStudy <= DateTime.Now && y.Learning == false).Count() > 0
                && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.RoomUsers.Where(y => y.UserId == userId).Count() > 0))
                    .Include(x => x.StudyDeck).Take(15).OrderBy(x => Guid.NewGuid()).AsNoTracking().ToList();
                if (studyCards.Count == 0)
                    return null;
                Random random = new Random();
                int rand = random.Next(1, studyCards.Count);
                return studyCards[rand - 1];
            }
            catch
            {
                return null;
            }
        }

        public StudyCard GetLearningStudy(int deckId, string userId)
        {
            try
            {
                List<StudyCard> studyCards = _applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && x.StudyProcesses.Where(y => y.UserId == userId && y.NextStudy <= DateTime.Now && y.Learning == true).Count() > 0
                && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.RoomUsers.Where(y => y.UserId == userId).Count() > 0))
                    .Include(x => x.StudyDeck).Take(15).OrderBy(x => Guid.NewGuid()).AsNoTracking().ToList();
                if (studyCards.Count == 0)
                    return null;
                Random random = new Random();
                int rand = random.Next(1, studyCards.Count);
                return studyCards[rand - 1];
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Remove(int cardId, string userId)
        {
            try
            {
                StudyCard remove = _applicationDbContext.StudyCard.Where(x => x.Id == cardId && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.AdminId == userId)).FirstOrDefault();
                if (remove == null)
                    return false;
                _applicationDbContext.StudyCard.Remove(remove);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<StudyCard>> GetAllStudyCards(int deckId, string userId)
        {
            try
            {
                return await _applicationDbContext.StudyCard.Where(x => x.StudyDeckId == deckId && (x.StudyDeck.Room.AdminId == userId || x.StudyDeck.UserId == userId || x.StudyProcesses.Where(y => y.UserId == userId).Count() > 0)).Include(x => x.StudyProcesses).AsNoTracking().ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}