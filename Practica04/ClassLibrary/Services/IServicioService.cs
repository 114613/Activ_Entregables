using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public interface IServicioService
    {
        List<Servicio> GetAll();
        Servicio? GetById(int id);
        void Save(Servicio servicio);
        void Delete(int id);
    }
}
