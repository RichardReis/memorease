using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ateno.API.ViewObjects.Account
{
    public class RegisterVO
    {

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(5)]
        [MaxLength(128)]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [MinLength(7)]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }
    }
}
