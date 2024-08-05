using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Turno
    {
        public int Id { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string cliente { get; set; }
        public List<DetalleTurno> lDetalle { get; set; }

        public Turno()
        {
            lDetalle = new List<DetalleTurno>();
        }

        public void AgregarDetalle(DetalleTurno det)
        {
            lDetalle.Add(det);
        }
        public void QuitarDetalle(int nroDet)
        {
            lDetalle.RemoveAt(nroDet);
        }

    }
}
