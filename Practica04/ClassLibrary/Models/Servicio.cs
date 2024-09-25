using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public float Costo { get; set; }
        public bool EnPromocion { get; set; }
        public bool Activo { get; set; } = true;

    }
}
