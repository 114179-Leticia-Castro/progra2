using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenSLN.dominio
{
    public class Envio
    {
        public string Codigo { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime direccion { get; set; }
        public string DniCliente { get; set; }
        public string Estado { get; set; }
    }
}
