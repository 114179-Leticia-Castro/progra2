using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Entidades
{
    public class Receta
    {
        public int RecetaNro { get; set; }
        public string Nombre { get; set; }
        public int TipoReceta { get; set; }
        public string Cheff { get; set; }

        public List<DetalleReceta> detalle { get; set; }

        public Receta()
        {
            detalle = new List<DetalleReceta>();
        }

        public void AgregarDetalle(DetalleReceta det)
        {
            detalle.Add(det);
        }

        public void QuitarDetalle(int nroDet)
        {
            detalle.RemoveAt(nroDet);
        }
    }
}
