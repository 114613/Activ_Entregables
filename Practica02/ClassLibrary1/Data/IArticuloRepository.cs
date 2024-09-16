using Practica02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public interface IArticuloRepository
    {
        void AgregarArticulo(Articulo articulo);
        Articulo ConsultarArticulo(int id);
        IEnumerable<Articulo> ListarArticulo();
        void EditarArticulo(Articulo articulo);
        void EliminarArticulo(int id);
    }
}
