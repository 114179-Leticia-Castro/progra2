using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenSLN.dominio
{
    public class Empresa
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public int CodigoPostal { get; set; }
        public string Rubro { get; set; }
        public DateTime FechaBaja { get; set; }


        public List<Envio> Envios { get; set; }

        public Empresa()
        {
            Envios = new List<Envio>();
        }

        public void AgregarPedido(Envio oEnvio)
        {
            if (oEnvio != null)
                Envios.Add(oEnvio);
        }

        public void QuitarPedido(Envio oEnvio)
        {
            if (oEnvio != null)
                Envios.Remove(oEnvio);
        }
    }
}
