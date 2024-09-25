using ClassLibrary.Models;
using ClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _repository;

        public ServicioService(IServicioRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Servicio> GetAll()
        {
            return _repository.GetAll();
        }

        public Servicio? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Save(Servicio value)
        {
            if(value != null)
            {
                if(value.Id == 0)
                {
                    var servicio = new Servicio()
                    {
                        Nombre = value.Nombre,
                        Costo = value.Costo,
                        EnPromocion = value.EnPromocion
                    };
                    _repository.Save(servicio);
                }
                else
                {
                    var servicio = _repository.GetById(value.Id);
                    if(servicio != null)
                    {
                        servicio.Nombre = value.Nombre;
                        servicio.Costo = value.Costo;
                        servicio.EnPromocion = value.EnPromocion;
                        _repository.Save(servicio);
                    }
                }
            }
        }
    }
}
