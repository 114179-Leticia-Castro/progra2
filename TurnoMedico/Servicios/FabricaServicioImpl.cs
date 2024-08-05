using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnoMedico.Servicios.Implementacion;
using TurnoMedico.Servicios.Interfaz;

namespace TurnoMedico.Servicios
{
    public class FabricaServicioImpl : FabricaServicio
    {
        public override IService CrearServicio()
        {
            return new Service();
        }
    }
}
