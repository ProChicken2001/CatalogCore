using System.ComponentModel.DataAnnotations;

namespace GrpcCatalogCoreClient.Models
{
    public class FormProveedorView
    {
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresar un material, es obligatorio")]
        public string TipoMaterial { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }

    }
}
