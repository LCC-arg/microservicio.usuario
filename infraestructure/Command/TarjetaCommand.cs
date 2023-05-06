using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using infraestructure.Persistence;

namespace Infrastructure.Command
{
    public class TarjetaCommand : ITarjetaCommand
    {
        private readonly MicroservicioUsuarioContext _context;

        public TarjetaCommand(MicroservicioUsuarioContext context)
        {
            _context = context;
        }
        public Tarjeta RemoveTarjeta(Guid tarjetaId)
        {
            var removeTarjeta = _context.Tarjetas.Single(x => x.TarjetaId == tarjetaId);
            _context.Remove(removeTarjeta);
            _context.SaveChanges();
            return removeTarjeta;
        }

        public Tarjeta InsertTarjeta(Tarjeta tarjeta)
        {
            _context.Add(tarjeta);
            _context.SaveChanges();
            return tarjeta;
        }

        public Tarjeta UpdateTarjeta(Guid tarjetaId, TarjetaRequest request)
        {
            var updateTarjeta = _context.Tarjetas
            .FirstOrDefault(x => x.TarjetaId == tarjetaId);

            updateTarjeta.TipoTarjeta = request.TipoTarjeta;
            updateTarjeta.EntidadTarjeta = request.EntidadTarjeta;
            updateTarjeta.NumeroTarjeta = request.NumeroTarjeta;
            updateTarjeta.Vencimiento = request.Vencimiento;


            _context.Update(updateTarjeta);
            _context.SaveChanges();
            return updateTarjeta;
        }
    }
}
