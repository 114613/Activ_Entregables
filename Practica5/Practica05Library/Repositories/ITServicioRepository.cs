using Practica05Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica05Library.Repositories
{
    public interface ITServicioRepository
    {
        List<TServicio>? GetAll();
        TServicio? GetById(int id);
        bool Create(TServicio servicio);
        bool Update(int id, TServicio servicio);
        bool Delete(int id);
        List<TServicio>? GetPromotion();
    }
}
