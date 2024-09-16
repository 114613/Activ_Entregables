using ClassLibrary1.Data;
using Practica02.Models;

namespace Practica02.Services
{
    public class AplicacionService : IAplicacion
    {
        private readonly IArticuloRepository _repositorio;

        public AplicacionService(IArticuloRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AgregarArticulo(Articulo articulo)
        {
            _repositorio.AgregarArticulo(articulo);
        }

        public Articulo ConsultarArticulo(int id)
        {
            return _repositorio.ConsultarArticulo(id);
        }

        public IEnumerable<Articulo> ListarArticulos()
        {
            return _repositorio.ListarArticulo();
        }

        public void EditarArticulo(Articulo articulo)
        {
            _repositorio.EditarArticulo(articulo);
        }

        public void EliminarArticulo(int id)
        {
            _repositorio.EliminarArticulo(id);
        }
    }
}
