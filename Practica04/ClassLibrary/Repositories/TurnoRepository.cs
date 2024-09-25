using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly AppDbContext _context;

        public TurnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var turno = GetById(id);
            if(turno != null)
            {
                _context.Turnos.Remove(turno);
                _context.SaveChanges();
            }
        }

        public List<Turno> GetAll()
        {
            return _context.Turnos.Include(t => t.Detalles).ToList();
        }

        public Turno? GetById(int id)
        {
            return _context.Turnos.Include(t => t.Detalles).FirstOrDefault(t => t.Id == id);
        }

        public void Save(Turno turno)
        {
            if(turno != null)
            {
                if(turno.Id == 0)
                {
                    _context.Turnos.Add(turno);
                }
                else
                {
                    _context.Turnos.Update(turno);
                }
                _context.SaveChanges();
            }
        }
    }
}
