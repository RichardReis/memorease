using System.Collections.Generic;

namespace Ateno.Application.DTOs
{
    public class HomeRoomDTO
    {
        public HomeRoomDTO()
        {
            DeckCards = new List<DeckCardDTO>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public int InReview { get; set; }
        public int InLearning { get; set; }
        public int TotalCount { get; set; }
        public bool IsAdmin { get; set; }
        public List<DeckCardDTO> DeckCards { get; set; }
    }
}
