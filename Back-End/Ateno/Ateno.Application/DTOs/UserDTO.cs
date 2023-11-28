using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ateno.Application.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        [MaxLength(32)]
        [DisplayName("Primeiro Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(5)]
        [MaxLength(128)]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [MinLength(7)]
        [MaxLength(128)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}