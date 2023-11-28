using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Ateno.Domain.Entities;
using Ateno.Domain.Enum;
using Ateno.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.Application.Services
{
    public class StudyDeckService : IStudyDeckService
    {
        private readonly IStudyDeckRepository _studyDeckRepository;
        private readonly IStudyCardRepository _studyCardRepository;
        private readonly IMapper _mapper;

        public StudyDeckService(IStudyDeckRepository studyDeckRepository, IStudyCardRepository studyCardRepository, IMapper mapper)
        {
            _studyDeckRepository = studyDeckRepository;
            _studyCardRepository = studyCardRepository;
            _mapper = mapper;
        }

        public StudyDeckDTO LoadStudyDeck(int studyDeckId, string userId, bool loadCards = false)
        {
            try
            {
                if (studyDeckId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                StudyDeck deck = _studyDeckRepository.GetStudyDeck(studyDeckId, userId, loadCards);
                if (deck == null)
                    return null;
                StudyDeckDTO studyDeckDTO = _mapper.Map<StudyDeckDTO>(deck);
                if (loadCards)
                    studyDeckDTO.studyCardDTOs = _mapper.Map<ICollection<StudyCardDTO>>(deck.StudyCards);
                return studyDeckDTO;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseDTO> Create(StudyDeckDTO studyDeckDTO, ICollection<StudyCardDTO> studyCardDTOs)
        {
            try
            {
                if (studyDeckDTO == null || string.IsNullOrWhiteSpace(studyDeckDTO.Name))
                    return new ResponseDTO() { Message = "O Nome do Baralho é obrigatório." };
                if (studyCardDTOs == null || studyCardDTOs.Count == 0)
                    return new ResponseDTO() { Message = "Para cadastrar um Baralho, é necessário pelo menos uma Carta." };
                if (studyCardDTOs.Where(x => string.IsNullOrWhiteSpace(x.Front) || string.IsNullOrWhiteSpace(x.Back)).Count() > 0)
                    return new ResponseDTO() { Message = "A Frente e o Verso de todas as Cartas devem ser preenchidas." };
                StudyDeck studyDeck = null;
                if (!string.IsNullOrWhiteSpace(studyDeckDTO.UserId) && studyDeckDTO.StudyRoomId == -1)
                    studyDeck = new StudyDeck(0, studyDeckDTO.Name, studyDeckDTO.UserId, null, DateTime.Now);
                else if(string.IsNullOrWhiteSpace(studyDeckDTO.UserId) && studyDeckDTO.StudyRoomId > 0)
                    studyDeck = new StudyDeck(0, studyDeckDTO.Name, null, studyDeckDTO.StudyRoomId, DateTime.Now);
                else
                    return new ResponseDTO() { Message = "O Baralho deve ser vinculado a um Usuário ou a uma Sala." };
                int DeckId = await _studyDeckRepository.Create(studyDeck);
                if (DeckId <= 0)
                    return new ResponseDTO() { Message = "Falha ao cadastrar Baralho de Estudo." };
                foreach (StudyCardDTO item in studyCardDTOs)
                    item.StudyDeckId = DeckId;
                ICollection<StudyCard> studyCards = _mapper.Map<ICollection<StudyCard>>(studyCardDTOs);
                ResponseDTO response = new ResponseDTO();
                response.Success = await _studyCardRepository.Add(studyCards);
                if (response.Success)
                    return response;
                return new ResponseDTO() { Message = "Falha ao cadastrar Baralho de Estudo." };
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao cadastrar Baralho de Estudo." };
            }
        }

        public async Task<ResponseDTO> Create(StudyDeckDTO studyDeckDTO)
        {
            try
            {
                if (studyDeckDTO == null || string.IsNullOrWhiteSpace(studyDeckDTO.Name))
                    return new ResponseDTO() { Message = "O Nome do Baralho é obrigatório." };

                StudyDeck studyDeck = null;
                if (!string.IsNullOrWhiteSpace(studyDeckDTO.UserId) && studyDeckDTO.StudyRoomId == -1)
                    studyDeck = new StudyDeck(0, studyDeckDTO.Name, studyDeckDTO.UserId, null, DateTime.Now);
                else if (string.IsNullOrWhiteSpace(studyDeckDTO.UserId) && studyDeckDTO.StudyRoomId > 0)
                    studyDeck = new StudyDeck(0, studyDeckDTO.Name, null, studyDeckDTO.StudyRoomId, DateTime.Now);
                else
                    return new ResponseDTO() { Message = "O Baralho deve ser vinculado a um Usuário ou a uma Sala." };

                int DeckId = await _studyDeckRepository.Create(studyDeck);

                if (DeckId <= 0)
                    return new ResponseDTO() { Message = "Falha ao cadastrar Baralho de Estudo." };

                return new ResponseDTO() { Success = true };
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao cadastrar Baralho de Estudo." };
            }
        }

        public async Task<ResponseDTO> UpdateName(int studyDeckId, string name, string userId)
        {
            try
            {
                if (studyDeckId <= 0)
                    return new ResponseDTO() { Message = "Baralho de Estudo não encontrado." };
                if (string.IsNullOrWhiteSpace(name))
                    return new ResponseDTO() { Message = "O Nome do Baralho é obrigatório." };
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "O usuário informado é inválido." };
                StudyDeck studyDeck = _studyDeckRepository.GetStudyDeck(studyDeckId, userId);
                if(studyDeck == null)
                    return new ResponseDTO() { Message = "Falha ao alterar o Baralho de Estudo." };
                studyDeck.UpdateName(name);
                ResponseDTO response = new ResponseDTO();
                response.Success = await _studyDeckRepository.UpdateName(studyDeck, userId);
                if (response.Success)
                    return response;
                return new ResponseDTO() { Message = "Falha ao alterar o Baralho de Estudo." };
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao alterar o Baralho de Estudo." };
            }
        }

        public Permission AccessAllowed(int studyDeckId, string userId)
        {
            try
            {
                if (studyDeckId < 1 || string.IsNullOrWhiteSpace(userId))
                    return Permission.Unknown;
                return _studyDeckRepository.AccessAllowed(studyDeckId, userId);
            }
            catch
            {
                return Permission.Unknown;
            }
        }

        public StudyCardDTO LoadStudyCard(int studyCardId, string userId)
        {
            try
            {
                if (studyCardId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                StudyCard studyCard = _studyCardRepository.LoadStudyCard(studyCardId, userId);
                return _mapper.Map<StudyCardDTO>(studyCard);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseDTO> AddCards(int studyDeckId, ICollection<StudyCardDTO> studyCardDTOs, string userId)
        {
            try
            {
                if (studyDeckId < 1)
                    return new ResponseDTO() { Message = "Baralho de Estudo não localizado." };
                if (studyCardDTOs == null || studyCardDTOs.Count == 0)
                    return new ResponseDTO() { Message = "Não há cartas para serem adicionadas." };
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Usuário não encontrado." };
                if (studyCardDTOs.Where(x => string.IsNullOrWhiteSpace(x.Front) || string.IsNullOrWhiteSpace(x.Back)).Count() > 0)
                    return new ResponseDTO() { Message = "A Frente e o Verso de todas as Cartas devem ser preenchidas." };
                ResponseDTO response = new ResponseDTO();
                response.Success = (AccessAllowed(studyDeckId, userId) == Permission.Admin);
                if(!response.Success)
                    return new ResponseDTO() { Message = "Baralho de Estudo não localizado." };
                foreach (StudyCardDTO item in studyCardDTOs)
                    item.StudyDeckId = studyDeckId;
                ICollection<StudyCard> studyCards = _mapper.Map<ICollection<StudyCard>>(studyCardDTOs);
                response.Success = await _studyCardRepository.Add(studyCards);
                response.Value = _studyDeckRepository.GetRoomIdByDeck(studyDeckId);
                if (response.Success)
                    return response;
                return new ResponseDTO() { Message = "Falha ao adicionar as Cartas no Baralho de Estudo." };
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao adicionar as Cartas no Baralho de Estudo." };
            }
        }

        public async Task<ResponseDTO> UpdateStudyCard(StudyCardDTO studyCardDTOs, string userId)
        {
            try
            {
                if (studyCardDTOs == null)
                    return new ResponseDTO() { Message = "Carta invalida." };
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Usuário não encontrado." };
                if (string.IsNullOrWhiteSpace(studyCardDTOs.Front) || string.IsNullOrWhiteSpace(studyCardDTOs.Back))
                    return new ResponseDTO() { Message = "A Frente e o Verso de todas as Cartas devem ser preenchidas." };
                ResponseDTO response = new ResponseDTO();

                StudyCard studyCards = _mapper.Map<StudyCard>(studyCardDTOs);
                response.Success = await _studyCardRepository.Update(studyCards);
                if (response.Success)
                    return response;
                return new ResponseDTO() { Message = "Falha ao atualizar as Carta no Baralho de Estudo." };
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao atualizar as Carta no Baralho de Estudo." };
            }
        }

        public async Task<ResponseDTO> RemoveDeck(int studyDeckId, string userId)
        {
            try
            {
                if (studyDeckId < 1 || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Falha ao remover o Baralho de Estudo." };
                ResponseDTO response = new ResponseDTO();
                response.Value = _studyDeckRepository.GetRoomIdByDeck(studyDeckId);
                response.Success = await _studyDeckRepository.Remove(studyDeckId, userId);
                if(!response.Success)
                    return new ResponseDTO() { Message = "Falha ao remover o Baralho de Estudo." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao remover o Baralho de Estudo." };
            }
        }

        public async Task<ResponseDTO> RemoveCard(int studyCardId, string userId)
        {
            try
            {
                if (studyCardId < 1 || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Falha ao remover a Carta do Baralho de Estudo." };
                ResponseDTO response = new ResponseDTO();
                response.Success = await _studyCardRepository.Remove(studyCardId, userId);
                if (!response.Success)
                    return new ResponseDTO() { Message = "Falha ao remover a Carta do Baralho de Estudo." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao remover a Carta do Baralho de Estudo." };
            }
        }

        public int GetRoomIdByDeck(int studyDeckId)
        {
            try
            {
                return _studyDeckRepository.GetRoomIdByDeck(studyDeckId);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<DeckInfoDTO> DeckInfo(int deckId, string userId)
        {
            try
            {
                if (deckId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                DeckInfoDTO response = new DeckInfoDTO();
                List<StudyCard> list = await _studyCardRepository.GetAllStudyCards(deckId, userId);
                if (list == null)
                    return null;
                float totalPerf = 0;
                int count = 0;
                foreach (StudyCard item in list)
                {
                    if(item.StudyProcesses.Count > 0)
                    {
                        response.CardInfo.Add(new CardInfoDTO()
                        {
                            Front = item.Front.Length < 32 ? item.Front : string.Concat(item.Front.Substring(0, 31), "..."),
                            Performance = item.StudyProcesses.Select(x => x.EFactor).FirstOrDefault().ToString("n2"),
                            Repetition = item.StudyProcesses.Select(x => x.Repetitions).FirstOrDefault()
                        });
                        totalPerf += item.StudyProcesses.Select(x => x.EFactor).FirstOrDefault();
                        count++;
                    }
                    else
                    {
                        response.CardInfo.Add(new CardInfoDTO()
                        {
                            Front = item.Front.Length < 32 ? item.Front : string.Concat(item.Front.Substring(0, 31), "..."),
                            Performance = "-",
                            Repetition = 0
                        });
                    }
                }
                response.Performance = (totalPerf / count).ToString("n2");
                response.CardsStudied = response.CardInfo.Count;
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
