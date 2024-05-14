using System.Net;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;

namespace TESAmerica.Application.Features.Vendedores.GetAll
{
    public class GetAllHandler(IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseBase<List<Domain.Vendedor>>> Handler()
        {
            var vendedores = await _unitOfWork.GenericRepository<Domain.Vendedor>().GetAll();

            if (!vendedores.Any())
                return new ResponseBase<List<Domain.Vendedor>>
                {
                    Mensaje = "No hay registros en la base de datos",
                    StatusCode = HttpStatusCode.NoContent
                };

            return new ResponseBase<List<Domain.Vendedor>>
            {
                Mensaje = "Consulta exitosa",
                Data = vendedores.ToList(),
                StatusCode = HttpStatusCode.OK
            };

        }
    }
}
