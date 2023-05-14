using Application.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITarjetaQuery
    {
        List<Tarjeta> GetTarjetaList();
        Tarjeta GetTarjetaById(Guid tarjetaId);
        List<Tarjeta> GetTarjetasUser(Guid usuarioId);
    }
}
