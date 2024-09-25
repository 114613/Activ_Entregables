using ClassLibrary.Models;
using ClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class TurnoService
    {
        private readonly ITurnoRepository _repository;

        public TurnoService(ITurnoRepository repository)
        {
            _repository = repository;
        }

        public List<Turno>? GetAll()
        {
            return _repository.GetAll();
        }

        public Turno? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Save(Turno value)
        {
            if(value != null)
            {
                if(value.Id == 0)
                {
                    var turno = new Turno()
                    {
                        Fecha = value.Fecha,
                        Hora = value.Hora,
                        Cliente = value.Cliente,
                        Detalles = value.Detalles

                    };
                    _repository.Save(turno);
                }
                else
                {
                    var turno = _repository.GetById(value.Id);
                    if(turno != null)
                    {
                        turno.Fecha = value.Fecha;
                        turno.Hora = value.Hora;
                        turno.Cliente = value.Cliente;
                        turno.Detalles = value.Detalles;
                        _repository.Save(turno);
                    }
                }
            }
        }
    }
}
