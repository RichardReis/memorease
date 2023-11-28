using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ateno.API.ViewObjects.Room
{
    public class RoomVO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome da Sala é obrigatório")]
        [MaxLength(32)]
        [DisplayName("Nome da Sala")]
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdminId { get; set; }
        [DisplayName("Sala pública")]
        public bool IsPublic { get; set; }
    }

    public class RoomUserVO
    {
        public int RoomId { get; set; }
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [MaxLength(32)]
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
