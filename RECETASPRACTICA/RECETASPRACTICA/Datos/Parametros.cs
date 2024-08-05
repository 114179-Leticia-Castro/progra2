﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Datos
{
    public class Parametros
    {
        public string Nombre { get; set; }
        public object Valor { get; set; }

        public Parametros(string nombre, object valor)
        {
            Nombre = nombre;
            Valor = valor;
        }
    }
}
