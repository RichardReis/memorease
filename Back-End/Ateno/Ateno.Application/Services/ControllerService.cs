using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Ateno.Application.Services
{
    public class ControllerService : IControllerService
    {
        private readonly IStudyDeckRepository _studyDeckRepository;
        private readonly IStudyCardRepository _studyCardRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomUserRepository _roomUserRepository;
        private readonly IUserService _userService;

        public ControllerService(IStudyDeckRepository studyDeckRepository, IStudyCardRepository studyCardRepository, IRoomRepository roomRepository, IRoomUserRepository roomUserRepository, IUserService userService)
        {
            _studyDeckRepository = studyDeckRepository;
            _studyCardRepository = studyCardRepository;
            _roomRepository = roomRepository;
            _roomUserRepository = roomUserRepository;
            _userService = userService;
        }

        public HomeDTO LoadHome(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return null;
                List<StudyDeck> decks = _studyDeckRepository.LoadUserDecks(userId);
                List<Room> rooms = _roomRepository.LoadUserRooms(userId);
                if (decks == null || rooms == null)
                    return null;
                HomeDTO response = new HomeDTO();
                response.UserFirstName = _userService.GetFirstName(userId);
                foreach (StudyDeck deck in decks)
                {
                    int countReview = _studyCardRepository.InReview(deck.Id, userId);
                    int countLearning = _studyCardRepository.InLearning(deck.Id, userId);
                    response.InReview += countReview;
                    response.InLearning += countLearning;
                    response.DeckCards.Add(new DeckCardDTO()
                    {
                        Id = deck.Id,
                        Name = deck.Name,
                        InReview = countReview,
                        InLearning = countLearning
                    });
                }
                response.DeckCards = response.DeckCards.OrderByDescending(x => x.InLearning + x.InReview).ThenBy(x => x.Name).ToList();
                response.TotalCount = response.InReview + response.InLearning;
                foreach (Room room in rooms)
                {
                    response.RoomCards.Add(new RoomCardDTO()
                    {
                        Id = room.Id,
                        Name = room.Name,
                        Code = room.IsPublic ? room.Code : null,
                        IsAdmin = (room.AdminId == userId)
                    });
                }
                return response;
            }
            catch
            {
                return null;
            }
        }

        public HomeRoomDTO LoadRoom(int roomId, string userId)
        {
            try
            {
                if (roomId < 1 || string.IsNullOrWhiteSpace(userId))
                    return null;
                if (!_roomUserRepository.AccessAllowed(roomId, userId))
                    return null;
                List<StudyDeck> decks = _studyDeckRepository.LoadRoomDecks(roomId, userId);
                Room room = _roomRepository.LoadRoom(roomId, userId);
                if (decks == null || room == null)
                    return null;
                HomeRoomDTO response = new HomeRoomDTO()
                {
                    RoomId = room.Id,
                    RoomName = room.Name,
                    RoomCode = room.IsPublic ? room.Code : null,
                    IsAdmin = (room.AdminId == userId)
                };
                foreach (StudyDeck deck in decks)
                {
                    int countReview = _studyCardRepository.InReview(deck.Id, userId);
                    int countLearning = _studyCardRepository.InLearning(deck.Id, userId);
                    response.InReview += countReview;
                    response.InLearning += countLearning;
                    response.DeckCards.Add(new DeckCardDTO()
                    {
                        Id = deck.Id,
                        Name = deck.Name,
                        InReview = countReview,
                        InLearning = countLearning
                    });
                }
                response.DeckCards = response.DeckCards.OrderByDescending(x => x.InLearning + x.InReview).ThenBy(x => x.Name).ToList();
                response.TotalCount = response.InReview + response.InLearning;
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}