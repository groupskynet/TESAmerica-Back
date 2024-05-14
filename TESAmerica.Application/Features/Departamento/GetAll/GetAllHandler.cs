using System.Net;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;

namespace TESAmerica.Application.Features.Departamento.GetAll
{
    public class GetAllHandler(IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseBase<List<Domain.Departamento>>> Handler()
        {
            var departamentos = await _unitOfWork.GenericRepository<Domain.Departamento>().GetAll();

            if (!departamentos.Any())
                return new ResponseBase<List<Domain.Departamento>>
                {
                    Mensaje = "No hay registros en la base de datos",
                    StatusCode = HttpStatusCode.NoContent
                };

            return new ResponseBase<List<Domain.Departamento>>
            {
                Mensaje = "Consulta exitosa",
                Data = departamentos.ToList(),
                StatusCode = HttpStatusCode.OK
            };

        }
    }
}
