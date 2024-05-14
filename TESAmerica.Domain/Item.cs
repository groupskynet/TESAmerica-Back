
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("ITEMS")]
    public class Item
    {
        [Key, Column(Order = 0)]
        [Required]
        [PropertyTab("NUMPEDIDO")]
        public string NumPedido { get; set; } = string.Empty;
        [Key, Column(Order = 1)]
        [Required]
        [PropertyTab("PRODUCTO")]
        public string Producto { get; set; } = string.Empty;
        [Required]
        [PropertyTab("PRECIO")]
        public decimal Precio { get; set; }
        [PropertyTab("CANTIDAD")]
        public decimal? Cantidad { get; set; }
        [PropertyTab("SUBTOTAL")]
        public decimal? Subtotal { get; }

    }
}
