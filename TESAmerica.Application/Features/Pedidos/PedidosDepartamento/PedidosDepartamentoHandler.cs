using AutoMapper;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;

namespace TESAmerica.Application.Features.Pedidos.PedidosDepartamento
{
    public class PedidosDepartamentoHandler(IUnitOfWork unitOfWork, IMapper mapper): ValidationCommand<PedidosDepartamentoCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseBase<List<PedidosDepartamentoResponse>>> Handler(PedidosDepartamentoCommand command)
        {
            var validaciones = await Validation(command);

            if (validaciones.Any())
                return new ResponseBase<List<PedidosDepartamentoResponse>>
                {
                    Errores = validaciones,
                    Mensaje = "Hubo uno o más errores en la operación",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var pedidos = await _unitOfWork.PedidoRepository.PedidosDepartamento(command);
            if(pedidos == null)
                return new ResponseBase<List<PedidosDepartamentoResponse>>
                {
                    Mensaje = "No se encotraron registros",
                    StatusCode = System.Net.HttpStatusCode.NoContent
                };

            var pedidosResponse = pedidos.OrderBy(x => x.NombreDepartamento).ToList();
            return new ResponseBase<List<PedidosDepartamentoResponse>>
            {
                Mensaje = "Consulta exitosa",
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = pedidosResponse
            };
        }

        public override async Task<List<string>> Validation(PedidosDepartamentoCommand command)
        {
            var errores = new List<string>();
            if (command == null)
            {
                errores.Add("El filtro a realizar no puede ser nulo");
                return errores;
            }

            if (command.Desde == null) errores.Add("La fecha desde no puede ser nula");
            if (command.Hasta == null) errores.Add("La fecha hasta no puede ser nula");
            if (command.Desde != null && command.Hasta != null)
            {
                if (command.Hasta < command.Desde) errores.Add("La fecha hasta debe ser mayor a la fecha desde");
            }

            return await Task.FromResult(errores);
        }
    }
}
