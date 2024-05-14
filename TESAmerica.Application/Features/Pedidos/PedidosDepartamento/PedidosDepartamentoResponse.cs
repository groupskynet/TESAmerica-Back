namespace TESAmerica.Application.Features.Pedidos.PedidosDepartamento
{
    public class PedidosDepartamentoResponse
    {
        public string CodigoDepartamento { get; set; } = string.Empty;
        public string NombreDepartamento { get; set; } = string.Empty;
        public decimal? PrecioTotal { get; set;}
        public decimal? Subtotal { get; set; }
    }
}
