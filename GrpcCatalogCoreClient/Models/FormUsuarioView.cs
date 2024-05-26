using System.ComponentModel.DataAnnotations;

namespace GrpcCatalogCoreClient.Models
{
    public class FormUsuarioView
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Eliga el tipo de usuario")]
        public int TipoUsuarioId { get; set; }

        public string TipoUsuario {  get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string Direccion {  get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La fecha de inscripcion es obligatoria")]
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La accesibilidad es obligatoria")]
        public string AccesibilidadNecesaria { get; set; }

    }
}
