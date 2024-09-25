using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string? Cliente { get; set; }
        public List<DetalleTurno>? Detalles { get; set; }

    }
}
