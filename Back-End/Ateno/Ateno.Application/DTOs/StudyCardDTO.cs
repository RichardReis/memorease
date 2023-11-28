using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ateno.Application.DTOs
{
    public class StudyCardDTO
    {
        public int Id { get; set; }
        public int StudyDeckId { get; set; }
        public string StudyDeckName { get; set; }
        [Required(ErrorMessage = "A Frente da Carta é obrigatória")]
        [MaxLength(2048)]
        [DisplayName("Frente da Carta")]
        public string Front { get; set; }
        [Required(ErrorMessage = "O Verso da Carta é obrigatório")]
        [MaxLength(2048)]
        [DisplayName("Verso da Carta")]
        public string Back { get; set; }
    }
}