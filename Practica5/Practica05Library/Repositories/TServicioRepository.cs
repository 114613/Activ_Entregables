using Practica05Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica05Library.Repositories
{
    public class TServicioRepository : ITServicioRepository
    {
        private readonly AppDbContext _context;

        public TServicioRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Create(TServicio servicio)
        {
            if(servicio != null)
            {
                _context.TServicios.Add(servicio);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var servicio = GetById(id);
            if (servicio != null)
            {
                //No realizo la baja lógica debido a que no existe el campo en la base de datos
                _context.TServicios.Remove(servicio);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public List<TServicio>? GetAll()
        {
            return _context.TServicios.ToList();
        }

        public TServicio? GetById(int id)
        {
            return _context.TServicios.FirstOrDefault(s => s.Id == id);
        }

        public List<TServicio>? GetPromotion()
        {
            return _context.TServicios.Where(s => s.EnPromocion != "n").ToList();
        }

        public bool Update(int id)
        {
            var servicio = GetById(id);
            if (servicio != null)
            {
                _context.TServicios.Update(servicio);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
