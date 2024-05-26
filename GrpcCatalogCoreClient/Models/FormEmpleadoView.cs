using System.ComponentModel.DataAnnotations;

namespace GrpcCatalogCoreClient.Models
{
    public class FormEmpleadoView
    {
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El Rol es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Eliga un rol")]
        public int RolId { get; set; }

        public string Rol {  get; set; }

        [Required(ErrorMessage = "La fecha de contratacion es obligatoria")]
        public DateTime FechaContratacion {  get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El salario es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El salario no es correcto")]
        public double Salario { get; set; }

        [Required(ErrorMessage = "El horario es obligatorio")]
        public string HorarioTrabajo { get; set; }

        [Required(ErrorMessage = "El contacto de emergencia es obligatorio")]
        public string ContactoEmergencia { get; set; }
    }
}
