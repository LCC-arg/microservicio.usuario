namespace Application.Request
{
    public class UsuarioRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FechaNac { get; set; }
        public string Email { get; set; }
        public string Nacionalidad { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
    }
}
