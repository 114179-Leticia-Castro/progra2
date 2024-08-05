using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnoMedico.Entidades
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public string Paciente { get; set; }
        public bool Estado { get; set; }
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
