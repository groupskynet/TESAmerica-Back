using Microsoft.AspNetCore.Mvc;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Clientes.GetAll;

namespace TESAmerica.Api.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await new GetAllHandler(_unitOfWork).Handler());
        }
    }
}
