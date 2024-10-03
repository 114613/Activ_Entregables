using Practica05Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica05Library.Repositories
{
    public class TTurnoRepository : ITTurnoRepository
    {
        private readonly AppDbContext _context;

        public TTurnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Create(TTurno turno)
        {
            if(turno != null)
            {
                _context.TTurnos.Add(turno);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var turno = GetById(id);
            if(turno != null)
            {
                turno.Activo = false;
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public List<TTurno>? GetAll()
        {
            return _context.TTurnos.Where(t => t.Activo == true).ToList();
        }

        public List<TTurno> GetByDate(DateTime fec1, DateTime fec2)
        {
            return _context.TTurnos.Where(t => t.Fecha >= fec1 && t.Fecha <= fec2 && t.Activo == true).ToList();
        }

        public TTurno? GetById(int id)
        {
            return _context.TTurnos.FirstOrDefault(t => t.Id == id && t.Activo == true);
        }

        public bool Update(int id)
        {
            var turno = GetById(id);
            if( turno != null)
            {
                _context.TTurnos.Update(turno);
                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
