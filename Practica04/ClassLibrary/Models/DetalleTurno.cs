using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class DetalleTurno
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public Turno? Turno { get; set; }
        public Servicio? Servicio { get; set; }
        public string? Observaciones { get; set; }

    }
}
