using RECETASPRACTICA.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Servicios.Interfaz
{
    public interface IServicio
    {
        List<Ingrediente> TraerIngrediente();
        bool CrearReceta(Receta oReceta);
        int TraerProximaReceta();


    }
}
