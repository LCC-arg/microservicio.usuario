using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase.Tarjetas
{
    public class TarjetaService : ITarjetaService
    {
        private readonly ITarjetaCommand _command;
        private readonly ITarjetaQuery _query;
        private readonly IUsuarioService _usuarioService;

        public TarjetaService(ITarjetaCommand command, ITarjetaQuery query, IUsuarioService usuarioService)
        {
            _command = command;
            _query = query;
            _usuarioService = usuarioService;
        }

        public TarjetaResponse CreateTarjeta(TarjetaRequest request)
        {
            var tarjeta = new Tarjeta
            {
                NumeroTarjeta = request.NumeroTarjeta,
                TipoTarjeta = request.TipoTarjeta,
                Vencimiento = request.Vencimiento,
                EntidadTarjeta = request.EntidadTarjeta,
                UsuarioId = request.usuarioId
            };
            _command.InsertTarjeta(tarjeta);
            return new TarjetaResponse
            {
                TarjetaId = tarjeta.TarjetaId,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                EntidadTarjeta = tarjeta.EntidadTarjeta,
                UsuarioId = tarjeta.UsuarioId,
                Usuario =  _usuarioService.GetUsuarioById(tarjeta.UsuarioId)
            };
        }

        public TarjetaResponse GetTarjetaById(Guid tarjetaId)
        {
            Tarjeta tarjeta = _query.GetTarjetaById(tarjetaId);
            return new TarjetaResponse
            {
                TarjetaId = tarjeta.TarjetaId,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                EntidadTarjeta = tarjeta.EntidadTarjeta
            };
        }

        public List<Tarjeta> GetTarjetaList()
        {
            return _query.GetTarjetaList();
        }

        public TarjetasUsuarioResponse GetUsuarioTarjetas(Guid usuarioId)
        {
            List<Tarjeta> tarjetasMapear = _query.GetTarjetasUser(usuarioId);

            if (_usuarioService.GetUsuarioById(usuarioId) == null)
            {
                return null;
            }

            List<TarjetaGetResponse> tarjetaGetResponses = new List<TarjetaGetResponse>();
            

            foreach (var tarjeta in tarjetasMapear)
            {
                var tarjetaResponse = new TarjetaGetResponse
                {
                    id = tarjeta.TarjetaId,
                    NumeroTarjeta = tarjeta.NumeroTarjeta,
                    TipoTarjeta = tarjeta.TipoTarjeta,
                    Vencimiento = tarjeta.Vencimiento,
                    EntidadTarjeta = tarjeta.EntidadTarjeta
                    
                };

                tarjetaGetResponses.Add(tarjetaResponse);
            }

            return new TarjetasUsuarioResponse
            {   
                usuarioId = usuarioId,
                nombre = _usuarioService.GetUsuarioById(usuarioId).Nombre,
                tarjetasUsuario = tarjetaGetResponses
            };
        }

        public TarjetaResponse RemoveTarjeta(Guid tarjetaId)
        {
            var tarjeta = _command.RemoveTarjeta(tarjetaId);

            return new TarjetaResponse
            {
                TarjetaId = tarjeta.TarjetaId,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                EntidadTarjeta = tarjeta.EntidadTarjeta
            };
        }

        public TarjetaResponse UpdateTarjeta(Guid tarjetaId, TarjetaRequest request)
        {
            var tarjeta = _command.UpdateTarjeta(tarjetaId, request);
            return new TarjetaResponse
            {
                TarjetaId = tarjeta.TarjetaId,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                EntidadTarjeta = tarjeta.EntidadTarjeta
            };

        }

    }
}