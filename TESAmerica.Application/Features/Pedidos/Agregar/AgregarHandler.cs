using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Shared;
using TESAmerica.Domain;

namespace TESAmerica.Application.Features.Pedidos.Agregar
{
    public class AgregarHandler(IUnitOfWork unitOfWork) : ValidationCommand<AgregarCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseBase<bool>> Handler(AgregarCommand command)
        {
            await _unitOfWork.BeginTransaction();
            try
            {
                var errors = new List<string>();
                var validaciones = await Validation(command);


                if (validaciones.Any())
                    return new ResponseBase<bool>
                    {
                        Errores = validaciones,
                        Mensaje = "Hubo uno o más errores en la operación",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };


                var contador = await _unitOfWork.GenericRepository<Contadores>()
                    .FindFirstOrDefault(x => x.Nombre == "PEDIDO");

                if (contador == null)
                {
                    return new ResponseBase<bool>
                    {
                        Errores = validaciones,
                        Mensaje = "No se pudo crear el pedido",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError
                    };
                }

                var cliente = await _unitOfWork.GenericRepository<Cliente>().Find(command.CodCli);

                var vendedor = await _unitOfWork.GenericRepository<Vendedor>().Find(command.CodVend);

                contador.acumulado++;
                
                var newPedido = new Pedido
                {
                    NumPedido = $"{contador.acumulado}",
                    Cliente = cliente.CodCli,
                    Vendedor = vendedor.CodVend,
                    Fecha = DateTime.Now,
                };

                var pedido = await _unitOfWork.GenericRepository<Pedido>().AddAndReturn(newPedido);

                await _unitOfWork.Save();

                var items = new List<Item>();
                foreach (var x in command.ProductosPedido)
                {
                    var producto = await _unitOfWork.GenericRepository<Producto>().Find(x.CodPro);
                    items.Add(new Item
                    {
                        Producto = producto.CodProd,
                        NumPedido = pedido.NumPedido,
                        Cantidad = x.Cantidad,
                        Precio = producto.Precio ?? 0m,
                    });
                }

                await _unitOfWork.GenericRepository<Item>().AddRange(items);

                await _unitOfWork.Commit();

                return new ResponseBase<bool>
                {
                    Mensaje = "Se agrego existosamente",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new ResponseBase<bool>
                {
                    Mensaje = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public override async Task<List<string>> Validation(AgregarCommand command)
        {
            var errores = new List<string>();
            if (command == null)
            {
                errores.Add("El pedido no puede ser nulo");
                return errores;
            }

            if (string.IsNullOrEmpty(command.CodVend)) errores.Add("El pedido debe tener un vendedor asignado.");

            if (string.IsNullOrEmpty(command.CodCli)) errores.Add("El pedido debe tener un cliente asignado.");

            if (command.ProductosPedido == null || !command.ProductosPedido.Any())
                errores.Add("El pedido debe tener productos para vender.");

            command.ProductosPedido!.ForEach(x =>
            {
                if (string.IsNullOrEmpty(x.CodPro) || x.Cantidad <= 0m)
                {
                    errores.Add("Hay pedidos con id de producto vacio o cantidad menor de 0");
                    return;
                }
            });

            if (!errores.Any())
            {
                var cliente = await _unitOfWork.GenericRepository<Cliente>().Find(command.CodCli);
                if (cliente == null) errores.Add($"No se pudo encontrar el cliente {command.CodCli}");

                var vebdedor = await _unitOfWork.GenericRepository<Vendedor>().Find(command.CodVend);
                if (vebdedor == null) errores.Add($"No se pudo encontrar el vendedor {command.CodCli}");


                foreach (var pedido in command.ProductosPedido)
                {
                    var producto = await _unitOfWork.GenericRepository<Producto>().Find(pedido.CodPro);
                    if (producto == null)
                    {
                        errores.Add($"No se pudo encontrar el producto {pedido.CodPro}");
                    }
                }
            }

            return errores;
        }
    }
}