using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITarjetaService
    {
        TarjetaResponse CreateTarjeta(TarjetaRequest tarjeta);
        TarjetaResponse RemoveTarjeta(Guid tarjetaId);
        TarjetaResponse UpdateTarjeta(Guid tarjetaId, TarjetaRequest tarjeta);
        List<Tarjeta> GetTarjetaList();
        TarjetaResponse GetTarjetaById(Guid tarjetaId);
    }
}
