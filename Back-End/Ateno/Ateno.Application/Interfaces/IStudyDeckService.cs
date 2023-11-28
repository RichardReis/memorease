using Ateno.Application.DTOs;
using Ateno.Domain.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Application.Interfaces
{
    public interface IStudyDeckService
    {
        StudyDeckDTO LoadStudyDeck(int studyDeckId, string userId, bool loadCards = false);
        Task<ResponseDTO> Create(StudyDeckDTO studyDeckDTO, ICollection<StudyCardDTO> studyCardDTOs);
        Task<ResponseDTO> Create(StudyDeckDTO studyDeckDTO);
        Task<ResponseDTO> UpdateName(int studyDeckId, string name, string userId);
        StudyCardDTO LoadStudyCard(int studyCardId, string userId);
        Permission AccessAllowed(int studyDeckId, string userId);
        Task<ResponseDTO> AddCards(int studyDeckId, ICollection<StudyCardDTO> studyCardDTOs, string userId);
        Task<ResponseDTO> UpdateStudyCard(StudyCardDTO studyCardDTOs, string userId);
        Task<ResponseDTO> RemoveDeck(int studyDeckId, string userId);
        Task<ResponseDTO> RemoveCard(int studyCardId, string userId);
        int GetRoomIdByDeck(int studyDeckId);
        Task<DeckInfoDTO> DeckInfo(int deckId, string userId);
    }
}
