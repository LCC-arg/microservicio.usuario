using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class TarjetaResponse
    {
        public Guid TarjetaId { get; set; }
        public string NumeroTarjeta { get; set; }
        public string EntidadTarjeta { get; set; }
        public Guid UsuarioId { get; set; }
        public UsuarioResponse Usuario { get; set; }
    }
}
