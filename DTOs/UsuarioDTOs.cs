namespace ProyectoAPI.DTOs
{
    public class UsuarioCreacionDTO
    {
        public required string Nombre { get; set; }
        public required string Contraseña { get; set; }
        public required string TipoUsuario { get; set; }
        public int IdEmp { get; set; }
    }

    public class UsuarioDTO
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
        public bool EstadoUser { get; set; }
        public string EmpleadoNombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public required string Nombre { get; set; }
        public required string Contraseña { get; set; }
    }
}
