namespace ProyectoAPI.DTOs
{
    public class EmpleadoDTOs
    {

        // Para crear (sin ID)
        public class EmpleadoCreacionDTO
        {
            public required string Nombre { get; set; }
            public required string Apellido { get; set; }
            public required int Dni { get; set; }
            public required int Telefono { get; set; }
            public required string Email { get; set; }
            public required string Direccion { get; set; }
            public int IdTipEmp { get; set; } // Tipo de empleado
        }

        // Para respuestas (con info adicional)
        public class EmpleadoDTO
        {
            public int IdEmp { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
            public int Dni { get; set; }
            public int Telefono { get; set; }
            public string Email { get; set; } = string.Empty;
            public string Direccion { get; set; } = string.Empty;
            public string TipoEmpleado { get; set; } = string.Empty; // Nombre del tipo
            public bool TieneUsuario { get; set; } // Para saber si ya tiene usuario
        }
    }
}
