using TESAmerica.Application.Features.Pedidos.PedidosDepartamento;

namespace TESAmerica.Application.Contracts.Persistence
{
    public interface IPedidoRepository
    {
        Task<List<PedidosDepartamentoResponse>> PedidosDepartamento(PedidosDepartamentoCommand command);
    }
}
