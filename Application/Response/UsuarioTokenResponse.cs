using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class UsuarioTokenResponse
    {
        public string Token { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
