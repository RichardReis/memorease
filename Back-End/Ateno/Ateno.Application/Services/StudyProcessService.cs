using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ateno.Application.Services
{
    public class StudyProcessService : IStudyProcessService
    {
        private readonly IStudyDeckRepository _studyDeckRepository;
        private readonly IStudyProcessRepository _studyProcessRepository;
        private readonly IStudyCardRepository _studyCardRepository;
        private readonly IMapper _mapper;

        public StudyProcessService(IStudyDeckRepository studyDeckRepository, IStudyProcessRepository studyProcessRepository, IStudyCardRepository studyCardRepository, IMapper mapper)
        {
            _studyDeckRepository = studyDeckRepository;
            _studyProcessRepository = studyProcessRepository;
            _studyCardRepository = studyCardRepository;
            _mapper = mapper;
        }

        public bool HasStudy(int studyDeckId, string userId)
        {
            try
            {
                return _studyCardRepository.HasStudy(studyDeckId, userId);
            }
            catch
            {
                return false;
            }
        }

        public StudyCardDTO LoadStudy(int studyDeckId, string userId)
        {
            try
            {
                if (studyDeckId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                StudyCard item = _studyCardRepository.GetNewStudy(studyDeckId, userId);
                if (item == null)
                    item = _studyCardRepository.GetReviewStudy(studyDeckId, userId);
                if (item == null)
                    item = _studyCardRepository.GetLearningStudy(studyDeckId, userId);
                if (item == null)
                    return null;
                StudyCardDTO response = _mapper.Map<StudyCardDTO>(item);
                response.StudyDeckName = item.StudyDeck.Name;
                return response;
            }
            catch
            {
                return null;
            }
        }

        public List<StudyDeckListDTO> LoadUserDecks(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return null;
                List<StudyDeck> decks = _studyDeckRepository.LoadUserDecks(userId);

                if (decks == null)
                    return new List<StudyDeckListDTO>();

                List<StudyDeckListDTO> response = new List<StudyDeckListDTO>();
                foreach (StudyDeck deck in decks)
                {
                    int countReview = _studyCardRepository.InReview(deck.Id, userId);
                    int countLearning = _studyCardRepository.InLearning(deck.Id, userId);
                    response.Add(new StudyDeckListDTO { 
                        Id = deck.Id,
                        Name = deck.Name,
                        StudyRoomId = deck.RoomId,
                        CreatedAt = deck.CreatedAt.ToString("dd/MM/yyyy"),
                        InReview = countReview,
                        InLearning = countLearning,
                    });
                }

                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseDTO> SaveStudy(int cardId, int answerQuality, string userId)
        {
            try
            {
                if (cardId < 1 || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Cartão de Estudo inválido." };
                if (answerQuality < 1 || answerQuality > 5)
                    return new ResponseDTO() { Message = "Qualidade da Resposta inválida." };
                StudyProcess studyProcess = _studyProcessRepository.GetByCard(cardId, userId);
                ResponseDTO response = new ResponseDTO();
                if (studyProcess != null)
                {
                    studyProcess = ScheduleStudy(studyProcess, answerQuality);
                    response.Success = await _studyProcessRepository.Update(studyProcess);
                }
                else
                {
                    int deckId = _studyCardRepository.GetDeckIdByCard(cardId, userId);
                    if (deckId == 0)
                        return new ResponseDTO() { Message = "Cartão de Estudo inválido." };
                    studyProcess = new StudyProcess(0, cardId, deckId, userId, true, 0, 2.5f, DateTime.Now.Date);
                    studyProcess = ScheduleStudy(studyProcess, answerQuality);
                    response.Success = await _studyProcessRepository.Create(studyProcess);
                }
                if (response.Success)
                    response.Value = studyProcess.StudyDeckId;
                else
                    response.Message = "Falha ao registrar Estudo.";
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao registrar Estudo." };
            }
        }

        private StudyProcess ScheduleStudy(StudyProcess studyProcess, int answerQuality)
        {
            try
            {
                //Calcular Fator E
                float ef = EFactorFunction(studyProcess.EFactor, answerQuality);
                if (answerQuality >= 4 && studyProcess.Repetitions > 0)
                {
                    //Calcular dias para a próxima repetição
                    int days = InterRepetitionFunction(studyProcess.Repetitions, studyProcess.EFactor);
                    //Agendar nova repetição
                    studyProcess.ScheduleStudy(days, ef);
                }
                else
                {
                    //Se não souber corretamente ou se for a primeira vez, aprende
                    studyProcess.InLearning(ef);
                    //Se errar, reinicia a contagem de estudos
                    if (answerQuality < 3)
                        studyProcess.RestartStudy();
                }
                return studyProcess;
            }
            catch
            {
                return null;
            }
        }

        private int InterRepetitionFunction(int repetitions, float eFactor)
        {
            if (repetitions <= 2)
            {
                switch (repetitions)
                {
                    case 0:
                        return 0;

                    case 1:
                        return 1;

                    case 2:
                        return 3;

                    default:
                        return 0;
                }
            }
            else
            {
                return ((int)Math.Round(((repetitions - 1) * (eFactor))));
            }
        }

        private float EFactorFunction(float oldEFactor, int answerQuality)
        {
            float result = ((float)(0.1 - (5 - answerQuality) * (0.008 + (5 - answerQuality) * 0.02)));
            result = oldEFactor + result;
            if (result >= 1.3)
                return result;
            else
                return 1.3f;
        }
    }
}
