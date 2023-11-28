using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ateno.Application.DTOs
{
    public class TokenUserDTO
    {
        public string Value { get; set;}
        public string RefreshToken { get; set;}
        public DateTime Expiration { get; set;}
    }
}
