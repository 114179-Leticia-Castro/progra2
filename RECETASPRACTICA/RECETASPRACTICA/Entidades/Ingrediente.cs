using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Entidades
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; }

        public Ingrediente()
        {
            IngredienteId = 0;
            Nombre = "";
            Unidad = "";
        }

        public Ingrediente(int ingredienteId, string nombre, string unidad)
        {
            IngredienteId = ingredienteId;
            Nombre = nombre;
            Unidad = unidad;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
