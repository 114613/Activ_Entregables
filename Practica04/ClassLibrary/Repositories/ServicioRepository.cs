using ClassLibrary.Data;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly AppDbContext _context;

        public ServicioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var servicio = GetById(id);
            if(servicio != null)
            {
                servicio.Activo = false;
                _context.SaveChanges();
            }
        }

        public List<Servicio> GetAll()
        {
            return _context.Servicios.ToList();
        }

        public Servicio? GetById(int id)
        {
            return _context.Servicios.Find(id);
        }

        public void Save(Servicio servicio)
        {
            if(servicio != null)
            {
                if(servicio.Id == 0)
                {
                    _context.Servicios.Add(servicio);
                }
                else
                {
                    _context.Servicios.Update(servicio);
                }
                _context.SaveChanges();
            }
        }
    }
}
