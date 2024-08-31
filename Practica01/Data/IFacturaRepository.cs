using Practica01.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Data
{
    public interface IFacturaRepository
    {
        void Agregar(Factura factura);
        Factura GetById(int nroFactura);
        List<Factura> GetAll();
        void Actualizar(Factura factura);
        void Eliminar(int nroFactura);
    }
}
