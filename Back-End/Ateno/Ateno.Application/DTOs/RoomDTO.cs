using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ateno.Application.DTOs
{
    public class RoomDTO
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
        public List<UserDTO> Users { get; set; }
    }
}
