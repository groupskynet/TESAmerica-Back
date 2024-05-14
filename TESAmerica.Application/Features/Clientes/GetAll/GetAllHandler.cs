using System.Net;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;

namespace TESAmerica.Application.Features.Clientes.GetAll
{
    public class GetAllHandler(IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseBase<List<Domain.Cliente>>> Handler()
        {
            var clientes = await _unitOfWork.GenericRepository<Domain.Cliente>().GetAll();

            if (!clientes.Any())
                return new ResponseBase<List<Domain.Cliente>>
                {
                    Mensaje = "No hay registros en la base de datos",
                    StatusCode = HttpStatusCode.NoContent
                };

            return new ResponseBase<List<Domain.Cliente>>
            {
                Mensaje = "Consulta exitosa",
                Data = clientes.ToList(),
                StatusCode = HttpStatusCode.OK
            };

        }
    }
}
