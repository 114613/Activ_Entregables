using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Models
{
    public class DetalleFactura
    {
        public Articulo? Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
