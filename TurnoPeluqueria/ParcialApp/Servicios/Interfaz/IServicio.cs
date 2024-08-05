using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Servicios.Interfaz
{
    public interface IServicio
    {
        List<Servicio> GetServicios();
        bool SaveTurno(Turno oTurno);
        bool ExisteTurno(string fecha, string hora);
    }
}
