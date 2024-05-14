using AutoMapper;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;

namespace TESAmerica.Application.Features.Comisiones
{
    public class ComisionesHandler(IUnitOfWork unitOfWork, IMapper mapper)  
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseBase<List<ComisionResponse>>> Handler(ComisionesCommand command)
        {
            var comisiones = await _unitOfWork.ComisionesRepository.comisiones(command);
            
            return new ResponseBase<List<ComisionResponse>>
            {
                Mensaje = "Consulta exitosa",
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = comisiones,
            };
        }
    }
}