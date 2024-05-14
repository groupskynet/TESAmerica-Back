using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESAmerica.Application.Features.Pedidos.Agregar
{
    public class AgregarCommand
    {
        public string CodCli { get; set; }
        public string CodVend { get; set; }

        public List<ProductoPedido> ProductosPedido { get; set; }

    }

    public class ProductoPedido
    {
        public string CodPro { get; set; }
        public decimal Cantidad { get; set; }
    }
}
