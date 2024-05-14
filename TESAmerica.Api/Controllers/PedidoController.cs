using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Pedidos.Agregar;
using TESAmerica.Application.Features.Pedidos.PedidosDepartamento;

namespace TESAmerica.Api.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost("getPedidosDepartamento")]
        public async Task<ActionResult> GetPedidosDepartamentoAsync(PedidosDepartamentoCommand command)
        {
            var response = await new PedidosDepartamentoHandler(_unitOfWork,_mapper).Handler(command);
            return Ok(response);
        }

        [HttpPost("agregar")]
        public async Task<ActionResult> Agregar(AgregarCommand command)
        {
            var response = await new AgregarHandler(_unitOfWork).Handler(command);
            return Ok(response);
        }
    }
}
