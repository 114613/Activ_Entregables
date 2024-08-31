using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public Cliente Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public Factura(int nroFactura, DateTime fecha, FormaPago formaPago, Cliente cliente)
        {
            NroFactura = nroFactura;
            Fecha = fecha;
            FormaPago = formaPago;
            Cliente = cliente;
            Detalles = new List<DetalleFactura>();
        }

        //public void AgregarDetalle(Articulo articulo, int cantidad)
        //{
        //    var detalleExistente = Detalles.FirstOrDefault(d => d.Articulo.Nombre == articulo.Nombre);
        //    if(detalleExistente != null)
        //    {
        //        detalleExistente.Cantidad += cantidad;
        //    }
        //    else
        //    {
        //        Detalles.Add(new DetalleFactura(articulo, cantidad));
        //    }
        //}

        public void AgregarDetalle(DetalleFactura detalle)
        {
            var existente = Detalles.FirstOrDefault(d => d.Articulo.Nombre == detalle.Articulo.Nombre);
            if (existente != null)
            {
                existente.Cantidad += detalle.Cantidad;
            }
            else
            {
                Detalles.Add(detalle);
            }
        }

        public float Total()
        {
            return Detalles.Sum(d => d.Subtotal());
        }
    }
}
