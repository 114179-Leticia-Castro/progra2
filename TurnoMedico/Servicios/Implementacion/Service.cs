using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnoMedico.Datos.Implementacion;
using TurnoMedico.Datos.Interfaz;
using TurnoMedico.Entidades;
using TurnoMedico.Servicios.Interfaz;

namespace TurnoMedico.Servicios.Implementacion
{
    internal class Service : IService
    {
        private ITurnoDao Dao;

        public Service()
        {
            Dao = new TurnoDao();
        }
        public bool CrearTurno(Turno oTurno)
        {
            return Dao.CrearTurno(oTurno);
        }

        public bool ExisteTurno(string fecha, string hora, int matricula)
        {
           return Dao.ExistsTurno(fecha,hora, matricula);
        }

        public List<Medico> TraerMedico()
        {
            return Dao.ObtenerMedico();
        }
    }
}
