using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class DetalleTurno
    {
        public Servicio Servicio { get; set; }
        public string Observaciones { get; set; }

        public DetalleTurno(Servicio servicio, string observaciones)
        {
            Servicio = servicio;
            Observaciones = observaciones;
        }

        
    }
}
