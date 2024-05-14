using AutoMapper;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;
using TESAmerica.Domain;

namespace TESAmerica.Application.Features.Productos;

public class ProductoHandler(IUnitOfWork unitOfWork)
{
   private readonly IUnitOfWork _unitOfWork = unitOfWork;
   public async Task<ResponseBase<List<Producto>>> Handler()
   {
      var response = await _unitOfWork.GenericRepository<Producto>().GetAll();
      return new ResponseBase<List<Producto>>
      {
         StatusCode = System.Net.HttpStatusCode.OK,
         Data = response.ToList()
      };
   } 
}