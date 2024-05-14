using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESAmerica.Domain
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Key]
        [Required]
        [PropertyTab("CODCLI")]
        public string CodCli { get; set; } = string.Empty;
        [PropertyTab("NOMBRE")]
        public string? Nombre { get; set; }
        [PropertyTab("DIRECCION")]
        public string? Direccion { get; set; }
        [PropertyTab("TELEFONO")]
        public string? Telefono { get; set; }
        [PropertyTab("CUPO")]
        public int? Cupo { get; set; }
        [PropertyTab("FECHACREACION")]
        public DateTime? FechaCreacion { get; set; }
        [PropertyTab("CANAL")]
        public string? Canal { get; set; }
        [PropertyTab("VENDEDOR")]
        public string? Vendedor { get; set; }
        [PropertyTab("CIUDAD")]
        public string? Ciudad { get; set; }
        [PropertyTab("PADRE")]
        public string? Padre { get; set; }

    }
}
