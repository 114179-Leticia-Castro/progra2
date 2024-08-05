using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnoMedico.Entidades;

namespace TurnoMedico.Datos.Interfaz
{
    public interface ITurnoDao
    {
        List<Medico> ObtenerMedico();
        bool CrearTurno(Turno oTurno);
        bool ExistsTurno(string fecha, string hora, int matricula);
    }
}
