using Microsoft.AspNetCore.Mvc;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Vendedores.GetAll;

namespace TESAmerica.Api.Controllers
{
    [Route("api/vendedores")]
    [ApiController]
    public class VendedoresController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await new GetAllHandler(_unitOfWork).Handler());
        }
    }
}
