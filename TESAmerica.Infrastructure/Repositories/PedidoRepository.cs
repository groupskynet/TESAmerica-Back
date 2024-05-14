
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Pedidos.PedidosDepartamento;
using TESAmerica.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TESAmerica.Domain;
using System.Runtime.Intrinsics.Arm;

namespace TESAmerica.Infrastructure.Repositories
{
    public class PedidoRepository(TESAmericaContext context) : IPedidoRepository
    {
        private readonly TESAmericaContext _context = context;
        public async Task<List<PedidosDepartamentoResponse>> PedidosDepartamento(PedidosDepartamentoCommand command)
        {

            var departamentos = _context.Departamentos; // Asumiendo que tienes una tabla de DEPARTAMENTOS
            var ciudades = _context.Ciudades; // Asumiendo que tienes una tabla de CIUDADES
            var clientes = _context.Clientes; // Asumiendo que tienes una tabla de CLIENTES
            var pedidos = _context.Pedidos; // Asumiendo que tienes una tabla de PEDIDOS
            var items = _context.Items; // Asumiendo que tienes una tabla de ITEMS

            //(select cl.CODCLI, cl.NOMBRE as nombrecli, d.CODDEP, d.NOMBRE as nombreDep " +
            //    $"from CLIENTE cl right join CIUDAD cd on(cd.CODCIU = cl.CIUDAD) " +
            //    $"right join DEPARTAMENTO d on(d.CODDEP = cd.DEPARTAMENTO))

            var queryCliente = from cd in ciudades
                               join cl in clientes on cd.CodCiu equals cl.Ciudad into ciudadesClientes
                               from cc in ciudadesClientes.DefaultIfEmpty()
                               join dp in departamentos on cd.Departamento equals dp.CodDep into departamentosCiudades
                               from dc in departamentosCiudades.DefaultIfEmpty()
                               select new
                               {
                                   cc.CodCli,
                                   nombreCliente = cc.Nombre,
                                   dc.CodDep,
                                   nombreDep = dc.Nombre,
                               };

            //select pe.numpedido, pe.fecha,sum(precio) as precio, sum(subtotal) as subtotal " +
            //    $"from Items i inner join PEDIDO pe on(pe.NUMPEDIDO = i.NUMPEDIDO) " +
            //    $"where pe.fecha between {0} and {1} group by pe.numpedido, pe.FECHA
            var pedidosItems = from i in items
                          join pe in pedidos on i.NumPedido equals pe.NumPedido
                          where pe.Fecha >= command.Desde && pe.Fecha <= command.Hasta
                          group i by new { pe.NumPedido, pe.Fecha } into g
                          select new
                          {
                              g.Key.NumPedido,
                              g.Key.Fecha,
                              precio = g.Sum( x => x.Precio),
                              subtotal = g.Sum( x => x.Cantidad * x.Precio)
                          };

            //(select p.CLIENTE, sum(subp.precio) precio, sum(subp.subtotal) subtotal " +
            //    $"from pedido p join (select pe.numpedido, pe.fecha,sum(precio) as precio, sum(subtotal) as subtotal " +
            //    $"from Items i inner join PEDIDO pe on(pe.NUMPEDIDO = i.NUMPEDIDO) " +
            //    $"where pe.fecha between {0} and {1} group by pe.numpedido, pe.FECHA  ) subp on(p.NUMPEDIDO = subp.NUMPEDIDO) " +
            //    $"group by p.CLIENTE)
            var queryClientePedido = from p in pedidos
                                     join subp in pedidosItems on p.NumPedido equals subp.NumPedido
                                     group new { p, subp } by new { p.Cliente } into g
                                     select new
                                     {
                                         g.Key.Cliente,
                                         precio = g.Sum(x => x.subp.precio),
                                         subtotal = g.Sum(x => x.subp.subtotal)
                                     };

            //select cld.CODDEP as CodigoDepartamento, cld.nombreDep as NombreDepartamento, " +
            //    $"sum(clp.precio) as precioTotal, sum(clp.subtotal) as subtotal " +
            //    $"from (select cl.CODCLI, cl.NOMBRE as nombrecli, d.CODDEP, d.NOMBRE as nombreDep " +
            //    $"from CLIENTE cl right join CIUDAD cd on(cd.CODCIU = cl.CIUDAD) " +
            //    $"right join DEPARTAMENTO d on(d.CODDEP = cd.DEPARTAMENTO)) cld " +
            //    $"left join (select p.CLIENTE, sum(subp.precio) precio, sum(subp.subtotal) subtotal " +
            //    $"from pedido p join (select pe.numpedido, pe.fecha,sum(precio) as precio, sum(subtotal) as subtotal " +
            //    $"from Items i inner join PEDIDO pe on(pe.NUMPEDIDO = i.NUMPEDIDO) " +
            //    $"where pe.fecha between {0} and {1} group by pe.numpedido, pe.FECHA  ) subp on(p.NUMPEDIDO = subp.NUMPEDIDO) " +
            //    $"group by p.CLIENTE) clp on(clp.CLIENTE = cld.CODCLI) group by cld.CODDEP, cld.nombreDep


            var query = from cld in queryCliente
                        join clp in queryClientePedido on cld.CodCli equals clp.Cliente into clientePedidos
                        from cp in clientePedidos.DefaultIfEmpty()
                        group new { cld, cp } by new { cld.CodDep, cld.nombreDep } into g
                        select new PedidosDepartamentoResponse
                        {
                            CodigoDepartamento = g.Key.CodDep,
                            NombreDepartamento = g.Key.nombreDep,
                            PrecioTotal = g.Sum( x => x.cp.precio),
                            Subtotal = g.Sum( x => x.cp.subtotal)
                        };

            return await query.ToListAsync();
        }
    }
}
