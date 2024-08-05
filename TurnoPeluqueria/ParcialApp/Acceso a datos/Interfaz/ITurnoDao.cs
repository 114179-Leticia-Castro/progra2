using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    interface ITurnoDao
    {

        List<Servicio> GetServicios(); //obtener
        bool Save(Turno oTurno); //crear
        bool ExisteTurno(string fecha, string hora);
    }
}
