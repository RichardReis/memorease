using System.Collections.Generic;

namespace Ateno.Application.DTOs
{
    public class DeckInfoDTO
    {
        public DeckInfoDTO()
        {
            CardInfo = new List<CardInfoDTO>();
        }

        public string Performance { get; set; }
        public int CardsStudied { get; set; }
        public List<CardInfoDTO> CardInfo { get; set; }
    }

    public class CardInfoDTO
    {
        public string Front { get; set; }
        public string Performance { get; set; }
        public int Repetition { get; set; }
    }
}
