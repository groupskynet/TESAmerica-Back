using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("PEDIDO")]
    public class Pedido
    {
        [Key]
        [Required]
        [PropertyTab("NUMPEDIDO")]
        public string NumPedido { get; set; } = string.Empty;
        [PropertyTab("CLIENTE")]
        public string? Cliente { get; set; }
        [PropertyTab("FECHA")]
        public DateTime? Fecha { get; set; }
        [PropertyTab("VENDEDOR")]
        public string? Vendedor { get; set; }
    }
}
