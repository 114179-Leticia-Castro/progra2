using ExamenSLN.datos.DTOs;
using ExamenSLN.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.Interfaz
{
    public interface IEnvioDao
    {
        bool RegistrarEnvio(Envio oEnvio);
        bool CancelarEnvio(int codigo);
        List<Envio> TraerEnviosFiltrados(string dni, DateTime desde, DateTime hasta);

    }
}
