﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class FormaPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public FormaPago(string nombre)
        {
            Nombre = nombre;
        }
    }
}
