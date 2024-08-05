using ExamenSLN.datos.DTOs;
using ExamenSLN.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Servicios.Interfaz
{
    public interface IServicio
    {
        bool CargarEnvio(Envio oEnvio);
        bool CancelarEnvio(int codigo);
        List<Envio> ListaEnviosFiltrados(string dni, DateTime desde, DateTime hasta);
    }
}
