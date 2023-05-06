namespace Application.Request
{
    public class TarjetaRequest
    {
        public string NumeroTarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public DateTime Vencimiento { get; set; }
        public string EntidadTarjeta { get; set; }
        public Guid usuarioId { get; set; }
    }
}
