using Practica01.Data;
using Practica01.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _repositorio;

        public FacturaService(IFacturaRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void CrearFactura(Factura factura)
        {
            _repositorio.Agregar(factura);
        }

        public Factura ObtenerFactura(int nroFactura)
        {
            return _repositorio.GetById(nroFactura);
        }

        public List<Factura> ListarFacturas()
        {
            return _repositorio.GetAll();
        }

        public void ActualizarFactura(Factura factura)
        {
            _repositorio.Actualizar(factura);
        }

        public void Eliminar(int nroFactura)
        {
            _repositorio.Eliminar(nroFactura);
        }
    }
}
