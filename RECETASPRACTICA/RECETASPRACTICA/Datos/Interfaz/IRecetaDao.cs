using RECETASPRACTICA.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Datos.Interfaz
{
    public interface IRecetaDao
    {
        List<Ingrediente> ObtenerIngrediente();
        bool Crear(Receta oReceta);
        int ObtenerProximaReceta();
    }
}
