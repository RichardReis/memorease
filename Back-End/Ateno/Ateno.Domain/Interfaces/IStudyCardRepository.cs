using Ateno.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Domain.Interfaces
{
    public interface IStudyCardRepository
    {
        StudyCard LoadStudyCard(int studyCardId, string userId);
        Task<bool> Add(ICollection<StudyCard> studyCards);
        Task<bool> Update(StudyCard studyCard);
        int GetDeckIdByCard(int cardId, string userId);
        bool HasStudy(int deckId, string userId);
        int InReview(int deckId, string userId);
        int InLearning(int deckId, string userId);
        StudyCard GetNewStudy(int deckId, string userId);
        StudyCard GetReviewStudy(int deckId, string userId);
        StudyCard GetLearningStudy(int deckId, string userId);
        Task<bool> Remove(int cardId, string userId);
        Task<List<StudyCard>> GetAllStudyCards(int deckId, string userId);
    }
}