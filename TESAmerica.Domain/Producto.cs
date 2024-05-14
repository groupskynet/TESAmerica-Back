using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("PRODUCTO")]
    public class Producto
    {
        [Key]
        [Required]
        [PropertyTab("CODPROD")]
        public string CodProd { get; set; } = string.Empty;
        [PropertyTab("NOMBRE")]
        public string? Nombre { get; set; }
        [PropertyTab("FAMILIA")]
        public string? Familia { get; set; }
        [PropertyTab("PRECIO")]
        public decimal? Precio { get; set; }
    }
}
