using System.ComponentModel.DataAnnotations;

namespace Ateno.API.ViewObjects.Account
{
    public class LoginVO
    {
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }
    }
}
