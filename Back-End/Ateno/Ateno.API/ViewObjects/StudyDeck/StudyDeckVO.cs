using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ateno.API.ViewObjects.StudyDeck
{
    public class StudyDeckVO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome do Baralho é obrigatório")]
        [MaxLength(32)]
        [DisplayName("Nome do Baralho")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public int? StudyRoomId { get; set; }
    }
}
