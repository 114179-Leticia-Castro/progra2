using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnoMedico.Entidades
{
    public class DetalleTurno
    {
        public DetalleTurno(Medico medico, string motivoConsulta, string fecha, string hora)
        {
            Medico = medico;
            MotivoConsulta = motivoConsulta;
            Fecha = fecha;
            Hora = hora;
        }

        public Medico Medico { get; set; }
        public string MotivoConsulta { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
    }
}
