using Ateno.Domain.Entities;
using Ateno.Domain.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Domain.Interfaces
{
    public interface IStudyDeckRepository
    {
        List<StudyDeck> LoadUserDecks(string userId);
        List<StudyDeck> LoadRoomDecks(int roomId, string userId);
        StudyDeck GetStudyDeck(int studyDeckId, string userId, bool loadCards = false);
        Task<int> Create(StudyDeck studyDeck);
        Task<bool> UpdateName(StudyDeck studyDeck, string userId);
        Permission AccessAllowed(int studyDeckId, string userId);
        Task<bool> Remove(int studyDeckId, string userId);
        int GetRoomIdByDeck(int studyDeckId);
        Task<int> StudentsCount(int studyDeckId);
    }
}