namespace Domain.Entities
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FechaNac { get; set; }
        public string Email { get; set; }
        public string Nacionalidad { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }

        public ICollection<Tarjeta> Tarjetas { get; set; }
    }
}