using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Models
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago? FormaPago { get; set; }
        public string? Cliente { get; set; }
        public List<DetalleFactura> Detalle { get; set; } = new List<DetalleFactura>();
    }
}
