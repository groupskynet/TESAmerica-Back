using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Comisiones;

namespace TESAmerica.Api.Controllers
{
    [Route("api/comisiones")]
    [ApiController]
    public class ComisionesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost()]
        public async Task<ActionResult> comisionesAsync(ComisionesCommand command)
        {
            var response = await new ComisionesHandler(_unitOfWork, _mapper).Handler(command);
            return Ok(response);
        } 
    }
}