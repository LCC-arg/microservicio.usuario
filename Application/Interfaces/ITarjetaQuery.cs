using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITarjetaQuery
    {
        List<Tarjeta> GetTarjetaList();
        Tarjeta GetTarjetaById(Guid tarjetaId);
    }
}
