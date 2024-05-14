using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("VENDEDOR")]
    public class Vendedor
    {
        [Key]
        [Required]
        [PropertyTab("CODVEND")]
        public string CodVend { get; set; } = string.Empty;
        [PropertyTab("NOMBRE")]
        public string? Nombre { get; set; }
        [PropertyTab("ESTADO")]
        public string? Estado { get; set; }
    }
}
