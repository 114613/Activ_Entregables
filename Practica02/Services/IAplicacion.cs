using Practica02.Models;

namespace Practica02.Services
{
    public interface IAplicacion
    {
        void AgregarArticulo(Articulo articulo);
        Articulo ConsultarArticulo(int id);
        IEnumerable<Articulo> ListarArticulos();
        void EditarArticulo(Articulo articulo);
        void EliminarArticulo(int id);
    }
}
