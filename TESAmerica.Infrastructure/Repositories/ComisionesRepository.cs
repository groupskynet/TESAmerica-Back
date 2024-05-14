using Microsoft.EntityFrameworkCore;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Application.Features.Comisiones;
using TESAmerica.Infrastructure.Presistence;

namespace TESAmerica.Infrastructure.Repositories;

public class ComisionesRepository(TESAmericaContext context) : IComisionesRepository
{
    private readonly TESAmericaContext _context = context;

    public async Task<List<ComisionResponse>> comisiones(ComisionesCommand command)
    {
        var vendedores = _context.Vendedores;
        var pedidos = _context.Pedidos;
        var items = _context.Items;
        
        /*
         * select v.CODVEND, SUM(i.SUBTOTAL * i.CANTIDAD * 0.10) comision from VENDEDOR v 
           left join PEDIDO p ON p.VENDEDOR  = v.CODVEND 
           inner join ITEMS i on p.NUMPEDIDO = i.NUMPEDIDO
           where p.FECHA = '2017-09-01 00:00:00.000'
           group by v.CODVEND
         */
        var queryComision = from p in pedidos 
            join v in vendedores on p.Vendedor equals v.CodVend into vendedorPedido
            from vp in vendedorPedido.DefaultIfEmpty()
            join i in items on p.NumPedido equals i.NumPedido
            where p.Fecha >= command.Desde && p.Fecha <= command.Hasta
            group i by new { vp.CodVend }
            into g
            select new
            {
                g.Key.CodVend,
                comision = g.Sum(x => x.Cantidad * x.Precio * (decimal)0.1) 
            };
        
        /*
          * select v.CODVEND,v.NOMBRE, c.comision from  VENDEDOR v left join
            (select v.CODVEND, SUM(i.SUBTOTAL * i.CANTIDAD * 0.10) comision from VENDEDOR v
            left join PEDIDO p ON p.VENDEDOR  = v.CODVEND
            inner join ITEMS i on p.NUMPEDIDO = i.NUMPEDIDO
            where p.FECHA = '2017-09-01 00:00:00.000'
            group by v.CODVEND) c on c.CODVEND = v.CODVEND
          */
        var query = from v in vendedores
            join c in queryComision on v.CodVend equals c.CodVend into vendedorComision
            from vc in vendedorComision.DefaultIfEmpty()
            select new ComisionResponse
            {
                CodVend = v.CodVend.Trim(),
                Nombre = v.Nombre,
                Comision = vc.comision ?? 0
            };   
                
        return await query.ToListAsync();
    }
}