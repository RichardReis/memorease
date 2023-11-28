using Ateno.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Application.Interfaces
{
    public interface IStudyProcessService
    {
        bool HasStudy(int studyDeckId, string userId);
        StudyCardDTO LoadStudy(int studyDeckId, string userId);
        List<StudyDeckListDTO> LoadUserDecks(string userId);
        Task<ResponseDTO> SaveStudy(int cardId, int answerQuality, string userId);
    }
}
