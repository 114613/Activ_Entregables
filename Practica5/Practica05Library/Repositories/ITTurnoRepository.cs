using Practica05Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica05Library.Repositories
{
    public interface ITTurnoRepository
    {
        List<TTurno>? GetAll();
        TTurno? GetById(int id);
        bool Create(TTurno turno);
        bool Update(int id);
        bool Delete(int id);
        List<TTurno> GetByDate(DateTime fec1, DateTime fec2);
    }
}
