using Application.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITarjetaCommand
    {
        Tarjeta InsertTarjeta(Tarjeta tarjeta);
        Tarjeta UpdateTarjeta(Guid tarjetaId,TarjetaRequest request);
        Tarjeta RemoveTarjeta(Guid tarjetaId);
    }
}
