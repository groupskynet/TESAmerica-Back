using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("CONTADORES")]
    public class Contadores
    {
        [Key]
        [Required]
        [PropertyTab("ID")]
        public string id { get; set; } = string.Empty;
        
        [PropertyTab("NOMBRE")]
        public string? Nombre { get; set; }
        
        [PropertyTab("ACUMULADO")]
        public decimal? acumulado { get; set; }
    }
}
