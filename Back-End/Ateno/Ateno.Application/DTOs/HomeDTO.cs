using System.Collections.Generic;

namespace Ateno.Application.DTOs
{
    public class HomeDTO
    {
        public HomeDTO()
        {
            DeckCards = new List<DeckCardDTO>();
            RoomCards = new List<RoomCardDTO>();
        }

        public string UserFirstName { get; set; }
        public int InReview { get; set; }
        public int InLearning { get; set; }
        public int TotalCount { get; set; }
        public List<DeckCardDTO> DeckCards { get; set; }
        public List<RoomCardDTO> RoomCards { get; set; }
    }

    public class DeckCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InReview { get; set; }
        public int InLearning { get; set; }
    }

    public class RoomCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsAdmin { get; set; }
    }
}