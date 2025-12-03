using System.ComponentModel.DataAnnotations;

    public class UsuarioCreacionDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string ContrasenaHash { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de usuario es requerido")]
        public string TipoUsuario { get; set; } = null!;

        [Required(ErrorMessage = "Debe seleccionar un empleado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido")]
        public int IdEmp { get; set; }
    }
