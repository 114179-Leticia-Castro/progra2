using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnoMedico.Servicios.Interfaz;

namespace TurnoMedico.Servicios
{
    public abstract class FabricaServicio
    {
        public abstract IService CrearServicio();
    }
}
