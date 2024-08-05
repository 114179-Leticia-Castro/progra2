using ParcialApp.Acceso_a_datos;
using ParcialApp.Acceso_a_datos.Implementacion;
using ParcialApp.Dominio;
using ParcialApp.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private ITurnoDao dao;

        public Servicio()
        {
            dao = new TurnoDao();
        }

        public bool ExisteTurno(string fecha, string hora)
        {
            return dao.ExisteTurno(fecha, hora);
        }

        public List<Dominio.Servicio> GetServicios()
        {
            return dao.GetServicios();
        }

        public bool SaveTurno(Turno oTurno)
        {
            return dao.Save(oTurno);
        }
    }
}
