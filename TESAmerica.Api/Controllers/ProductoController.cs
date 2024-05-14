using Microsoft.AspNetCore.Mvc;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Productos;
using TESAmerica.Infrastructure.Presistence;

namespace TESAmerica.Api.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("getproducts")]
        public async Task<ActionResult> GetProductos()
        {
            var response = await new ProductoHandler(_unitOfWork).Handler();
            return Ok(response);
        }
    }
}