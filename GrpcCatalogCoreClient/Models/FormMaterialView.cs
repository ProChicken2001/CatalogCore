using System;
using System.ComponentModel.DataAnnotations;

namespace GrpcCatalogCoreClient.Models
{
    public class FormMaterialView
    {
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo {  get; set; } = string.Empty;

        [Range(1,int.MaxValue,ErrorMessage = "Eliga una categoria")]
        public int CategoriaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Eliga un proveedor")]
        public int ProveedorId { get; set; }

        public string Categoria {  get; set; }
        public string Proveedor { get; set; }

        [Required(ErrorMessage = "La editorial es obligatoria")]
        public string Editorial { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ISBN es obligatorio")]
        [RegularExpression(@"^978-.+$", ErrorMessage = "El formato debe ser 978-(numeros o letras)")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Año de publicacion es obligatorio")]
        [Range(1000, 2024, ErrorMessage = "El año de publicacion no es correcto")]
        public int AnoPublicacion {  get; set; }

        [Required(ErrorMessage = "La edicion es obligatoria")]
        public string Edicion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ubicacion fisica es obligatoria")]
        public string UbicacionFisica { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [RegularExpression(@"^(Disponible|Prestado|En reparacion|Perdido)$", ErrorMessage = "Estados: Disponible | Prestado | En reparacion | Perdido")]
        public string Estado { get; set; } = string.Empty;


    }
}
