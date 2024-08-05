using ExamenSLN.datos.DTOs;
using ExamenSLN.dominio;
using RecetasSLN.datos.Implementacion;
using RecetasSLN.datos.Interfaz;
using RecetasSLN.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private IEnvioDao dao;

        public Servicio()
        {
            dao = new EnvioDao();
        }

        public bool CancelarEnvio(int codigo)
        {
            return dao.CancelarEnvio(codigo);
        }

        public bool CargarEnvio(Envio oEnvio)
        {
            return dao.RegistrarEnvio(oEnvio);
        }

        public List<Envio> ListaEnviosFiltrados(string dni, DateTime desde, DateTime hasta)
        {
            return dao.TraerEnviosFiltrados(dni, desde, hasta);
        }
    }
}
