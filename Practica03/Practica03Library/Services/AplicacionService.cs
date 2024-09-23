using Practica03Library.Data;
using Practica03Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Services
{
    public class AplicacionService : IAplicacion
    {
        private readonly ArticuloRepository _articuloRepository;
        private readonly FacturaRepository _facturaRepository;

        public AplicacionService(ArticuloRepository articuloRepository, FacturaRepository facturaRepository)
        {
            _articuloRepository = articuloRepository;
            _facturaRepository = facturaRepository;
        }

        // Métodos para la gestión de artículos
        public void AgregarArticulo(Articulo articulo)
        {
            if (articulo == null)
                throw new ArgumentNullException(nameof(articulo));

            _articuloRepository.AgregarArticulo(articulo);
        }

        public Articulo ConsultarArticulo(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            return _articuloRepository.ConsultarArticulo(id);
        }

        public void EditarArticulo(Articulo articulo)
        {
            if (articulo == null)
                throw new ArgumentNullException(nameof(articulo));

            if (articulo.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(articulo.Id));

            _articuloRepository.EditarArticulo(articulo);
        }

        public void BajaArticulo(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));

            _articuloRepository.RegistrarBajaArticulo(id);
        }

        public List<Articulo> Consultar()
        {
            return _articuloRepository.ListarArticulos();
        }


        public void RegistrarFactura(Factura factura)
        {
            if (factura == null)
                throw new ArgumentNullException(nameof(factura));

            if (factura.Detalle.Count == 0)
                throw new InvalidOperationException("La factura debe contener al menos un detalle.");

            _facturaRepository.RegistrarFactura(factura);
        }

        public Factura ConsultarFactura(int nroFactura)
        {
            if (nroFactura <= 0)
                throw new ArgumentException("El número de factura debe ser mayor que cero.", nameof(nroFactura));

            return _facturaRepository.ConsultarFactura(nroFactura);
        }

        public void EditarFactura(Factura factura)
        {
            if (factura == null)
                throw new ArgumentNullException(nameof(factura));

            if (factura.NroFactura <= 0)
                throw new ArgumentException("El número de factura debe ser mayor que cero.", nameof(factura.NroFactura));

            _facturaRepository.EditarFactura(factura);
        }

        public List<Factura> ConsultarFactura(DateTime? fecha, string formaPago)
        {
            return _facturaRepository.ConsultarFacturas(fecha, formaPago);
        }
    }
}
