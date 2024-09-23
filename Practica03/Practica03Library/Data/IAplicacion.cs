using Practica03Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Data
{
    public interface IAplicacion
    {
        List<Articulo> Consultar();
        void AgregarArticulo(Articulo articulo);
        Articulo ConsultarArticulo(int id);
        void EditarArticulo(Articulo articulo);
        void BajaArticulo(int id);


        void RegistrarFactura(Factura factura);
        Factura ConsultarFactura(int nroFactura);
        void EditarFactura(Factura factura);
        List<Factura> ConsultarFactura(DateTime? fecha, string formaPago);
    }
}
