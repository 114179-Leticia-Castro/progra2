using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenSLN.datos.DTOs
{
    public class EnvioDTO
    {
        public int Codigo { get; set; }
        public string DniCliente { get; set; }

        public string direccion { get; set; }

        public DateTime FechaEnvio { get; set; }
        public string estado { get; set; }




    }
}
