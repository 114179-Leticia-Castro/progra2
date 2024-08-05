using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Servicio
    {
        public Servicio(int id, string nombre, int costo, string enPromocion)
        {
            Id = id;
            Nombre = nombre;
            Costo = costo;
            EnPromocion = enPromocion;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Costo { get; set; }
        public string EnPromocion { get; set; }

        override
        public string ToString()
        {
            return Nombre + "| $" + Costo.ToString();
        }

    }
}
