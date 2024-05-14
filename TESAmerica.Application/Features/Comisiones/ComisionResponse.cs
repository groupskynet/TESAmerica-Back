namespace TESAmerica.Application.Features.Comisiones;

public class ComisionResponse
{
    public string CodVend { get; set; } = string.Empty;
    public string Nombre{ get; set; } = string.Empty;
    
    public decimal? Comision { get; set;}
};
