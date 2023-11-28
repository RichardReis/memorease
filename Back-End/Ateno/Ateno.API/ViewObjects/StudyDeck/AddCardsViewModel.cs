using Ateno.Application.DTOs;
using System.Collections.Generic;

namespace Ateno.WebUI.ViewModels.StudyDeck
{
    public class AddCardsViewModel
    {
        public int studyDeckId { get; set; }
        public ICollection<StudyCardDTO> Cards { get; set; }
    }
}
