using Ateno.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.WebUI.ViewModels.StudyDeck
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
            Deck = new StudyDeckDTO();
        }

        public StudyDeckDTO Deck { get; set; }
        public ICollection<StudyCardDTO> Cards { get; set; }
    }
}
