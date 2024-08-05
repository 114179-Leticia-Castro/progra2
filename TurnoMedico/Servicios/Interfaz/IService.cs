using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnoMedico.Entidades;

namespace TurnoMedico.Servicios.Interfaz
{
    public interface IService
    {
        List<Medico> TraerMedico();
        bool CrearTurno(Turno oTurno);
        bool ExisteTurno(string fecha, string hora, int matricula);
    }
}
