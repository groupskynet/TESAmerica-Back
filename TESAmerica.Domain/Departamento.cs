using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("DEPARTAMENTO")]
    public class Departamento
    {
        [Key]
        [Required]
        [PropertyTab("CODDEP")]
        public string CodDep { get; set; } = string.Empty;
        [PropertyTab("NOMBRE")]
        public string? Nombre { get; set; }

    }
}
