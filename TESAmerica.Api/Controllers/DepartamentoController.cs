using Microsoft.AspNetCore.Mvc;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Departamento.GetAll;

namespace TESAmerica.Api.Controllers
{
    [Route("api/departamento")]
    [ApiController]
    public class DepartamentoController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await new GetAllHandler(_unitOfWork).Handler());
        }
    }
}
