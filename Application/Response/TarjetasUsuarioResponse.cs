using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class TarjetasUsuarioResponse
    {   
        public Guid usuarioId {  get; set; }

        public string nombre { get; set; }
        public List<TarjetaGetResponse>? tarjetasUsuario { get; set; }
    }
}
