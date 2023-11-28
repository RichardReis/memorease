using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomUserRepository _roomUserRepository;
        private readonly IStudyCardRepository _studyCardRepository;
        private readonly IStudyDeckRepository _studyDeckRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IRoomUserRepository roomUserRepository, IStudyCardRepository studyCardRepository, IStudyDeckRepository studyDeckRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomUserRepository = roomUserRepository;
            _studyCardRepository = studyCardRepository;
            _studyDeckRepository = studyDeckRepository;
            _mapper = mapper;
        }

        public bool AccessAllowed(int roomId, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(userId))
                    return false;
                return _roomUserRepository.AccessAllowed(roomId, userId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<ResponseDTO> EnterRoom(string roomCode, string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(roomCode) || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Falha ao entrar na Sala de Estudos." };
                bool allow = _roomRepository.IsPublic(roomCode);
                if (!allow)
                    return new ResponseDTO() { Message = "Sala privada ou não encontrada." };
                ResponseDTO response = new ResponseDTO();
                response.Success = await _roomUserRepository.EnterRoom(roomCode, userId);
                if (!response.Success)
                    return new ResponseDTO() { Message = "Sala privada ou não encontrada." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao entrar na Sala de Estudos." };
            }
        }

        public bool IsAdmin(int roomId, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(userId))
                    return false;
                return _roomRepository.IsAdmin(roomId, userId);
            }
            catch
            {
                return false;
            }
        }

        public List<RoomDTO> LoadAllRooms(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return null;
                List<Room> rooms = _roomRepository.LoadUserRooms(userId);
                if (rooms == null)
                    return null;
                List<RoomDTO> response = _mapper.Map<List<RoomDTO>>(rooms);
                return response;
            }
            catch
            {
                return null;
            }
        }

        public List<UserDTO> LoadAllRoomUsers(int roomId, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                Room room = _roomRepository.getRoom(roomId, userId);
                if (room == null)
                    return null;
                List<UserDTO> response = _mapper.Map<List<UserDTO>>(room.RoomUsers.Select(x => x.User));
                return response;
            }
            catch
            {
                return null;
            }
        }

        public RoomDTO getRoom(int roomId, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                Room room = _roomRepository.getRoom(roomId, userId);
                if (room == null)
                    return null;
                RoomDTO response = _mapper.Map<RoomDTO>(room);
                response.Users = _mapper.Map<List<UserDTO>>(room.RoomUsers.Select(x => x.User));
                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseDTO> Create(RoomDTO roomDTO)
        {
            try
            {
                if (roomDTO == null)
                    return new ResponseDTO() { Message = "Falha ao criar Sala de Estudos." };
                if (string.IsNullOrWhiteSpace(roomDTO.Name))
                    return new ResponseDTO() { Message = "O Nome da Sala é obrigatório." };
                if (string.IsNullOrWhiteSpace(roomDTO.AdminId))
                    return new ResponseDTO() { Message = "O Administrador da Sala é obrigatório." };
                ResponseDTO response = new ResponseDTO();
                response.Success = await _roomRepository.Create(_mapper.Map<Room>(roomDTO));
                if (!response.Success)
                    return new ResponseDTO() { Message = "Falha ao criar Sala de Estudos." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao criar Sala de Estudos." };
            }
        }

        public async Task<ResponseDTO> Edit(RoomDTO roomDTO, string userId)
        {
            try
            {
                if (roomDTO == null || roomDTO.Id < 1)
                    return new ResponseDTO() { Message = "Falha ao editar Sala de Estudos." };
                if (string.IsNullOrWhiteSpace(roomDTO.Name))
                    return new ResponseDTO() { Message = "O Nome da Sala é obrigatório." };
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "O Administrador da Sala é obrigatório." };
                Room room = _roomRepository.getRoom(roomDTO.Id, userId, false);
                if(room == null)
                    return new ResponseDTO() { Message = "Falha ao editar Sala de Estudos." };
                ResponseDTO response = new ResponseDTO();
                room.Update(roomDTO.Name, roomDTO.IsPublic);
                response.Success = await _roomRepository.Update(room);
                if (!response.Success)
                    return new ResponseDTO() { Message = "Falha ao editar Sala de Estudos." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao editar Sala de Estudos." };
            }
        }

        public async Task<ResponseDTO> Remove(int roomId, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Falha ao remover Sala de Estudos." };
                ResponseDTO response = new ResponseDTO();
                bool admin = _roomRepository.IsAdmin(roomId, userId);
                if (admin)
                    response.Success = await _roomRepository.Remove(roomId, userId);
                else
                    response.Success = await _roomUserRepository.Unlink(roomId, userId);
                if (response.Success)
                    return response;
            }
            catch { }
            return new ResponseDTO() { Message = "Falha ao remover Sala de Estudos." };
        }

        public async Task<ResponseDTO> AddUser(int roomId, string email, string userId)
        {
            try
            {
                if(roomId < 1 || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Falha ao adicionar usuário na Sala de Estudos." };
                bool allow = _roomRepository.IsAdmin(roomId, userId);
                if(!allow)
                    return new ResponseDTO() { Message = "Falha ao adicionar usuário na Sala de Estudos." };
                ResponseDTO response = new ResponseDTO();
                response.Success = await _roomUserRepository.Link(roomId, email);
                if (!response.Success)
                    response.Message = "Usuário não encontrado ou já inserido.";
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao adicionar usuário na Sala de Estudos." };
            }
        }

        public async Task<ResponseDTO> RemoveUser(int roomId, string email, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Falha ao remover usuário da Sala de Estudos." };
                bool allow = _roomRepository.IsAdmin(roomId, userId);
                if (!allow)
                    return new ResponseDTO() { Message = "Falha ao remover usuário da Sala de Estudos." };
                ResponseDTO response = new ResponseDTO();
                response.Success = await _roomUserRepository.Unlink(roomId, email, userId);
                if (!response.Success)
                    response.Message = "Usuário não encontrado.";
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao remover usuário da Sala de Estudos." };
            }
        }

        public async Task<RoomDeckInfoDTO> RoomDeckInfo(int deckId, string userId)
        {
            try
            {
                if (deckId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                RoomDeckInfoDTO response = new RoomDeckInfoDTO();
                List<StudyCard> list = await _studyCardRepository.GetAllStudyCards(deckId, userId);
                if (list == null)
                    return null;
                float totalPerf = 0;
                foreach (StudyCard item in list)
                {
                    float perf = (item.StudyProcesses.Sum(x => x.EFactor) / item.StudyProcesses.Select(x => x.EFactor).Count());
                    if (!float.IsNaN(perf))
                    {
                        response.RoomCardInfo.Add(new RoomCardInfoDTO()
                        {
                            Front = item.Front.Length < 32 ? item.Front : string.Concat(item.Front.Substring(0, 31), "..."),
                            Performance = perf.ToString("n2"),
                            StudyUsers = item.StudyProcesses.Select(x => x.EFactor).Count()
                        });
                        totalPerf += perf;
                    }
                    else
                    {
                        response.RoomCardInfo.Add(new RoomCardInfoDTO()
                        {
                            Front = item.Front.Length < 32 ? item.Front : string.Concat(item.Front.Substring(0, 31), "..."),
                            Performance = "-",
                            StudyUsers = item.StudyProcesses.Select(x => x.EFactor).Count()
                        });
                    }
                }
                response.Performance = (totalPerf / response.RoomCardInfo.Where(x => x.StudyUsers > 0).Count()).ToString("n2");
                response.StudyUsers = await _studyDeckRepository.StudentsCount(deckId);
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}