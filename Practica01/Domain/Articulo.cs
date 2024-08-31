﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Domain
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float PrecioUnitario { get; set; }

        public Articulo(string nombre, float precioUnitario)
        {
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
        }
    }
}