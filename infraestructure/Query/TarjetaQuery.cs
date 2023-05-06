using Application.Interfaces;
using Domain.Entities;
using infraestructure.Persistence;

namespace Infrastructure.Query
{
    public class TarjetaQuery : ITarjetaQuery
    {
        private readonly MicroservicioUsuarioContext _context;

        public TarjetaQuery(MicroservicioUsuarioContext context)
        {
            _context = context;
        }
        public Tarjeta GetTarjetaById(Guid tarjetaId)
        {
            var tarjeta = _context.Tarjetas.Single(fe => fe.TarjetaId == tarjetaId);
            return tarjeta;
        }

        public List<Tarjeta> GetTarjetaList()
        {
            var listaTarjeta = _context.Tarjetas.OrderBy(fe => fe.TarjetaId).ToList();
            return listaTarjeta;
        }
    }
}
