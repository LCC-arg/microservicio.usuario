using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class TarjetaGetResponse
    {   
        public Guid id { get; set; }
        public string NumeroTarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public DateTime Vencimiento { get; set; }
        public string EntidadTarjeta { get; set; }

    }
}
