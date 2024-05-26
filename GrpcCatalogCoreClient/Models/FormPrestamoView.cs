using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;

namespace GrpcCatalogCoreClient.Models
{
    public class FormPrestamoView
    {
        public int PrestamoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Eliga un usuario")]
        public int UsuarioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Eliga un material")]
        public int MaterialId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Eliga un empleado")]
        public int EmpleadoId {  get; set; }

        [Required(ErrorMessage = "Ingresar una fecha para el prestamo")]
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ingresar la fecha para la devolucion esperada")]
        public DateTime FechaDevolucionEsperada { get; set; } = DateTime.Now;

        public DateTime? FechaDevolucionReal { get; set; } = null;

        [Required(ErrorMessage = "Si no hay penalizacion, coloque 0")]
        [Range(0, int.MaxValue, ErrorMessage = "Eliga un usuario")]
        public double Penalizaciones {  get; set; }

    }
}
