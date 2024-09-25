using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    public interface ITurnoRepository
    {
        List<Turno>? GetAll();
        Turno? GetById(int id);
        void Save(Turno turno);
        void Delete(int id);
    }
}
