using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("CIUDAD")]
    public class Ciudad
    {
        [Key]
        [Required]
        [PropertyTab("CODCIU")]
        public string CodCiu { get; set; } = string.Empty;
        [PropertyTab("NOMBRE")]
        public string? Nombre { get; set; }
        [PropertyTab("DEPARTAMENTO")]
        public string? Departamento { get; set; }


    }
}
