using RECETASPRACTICA.Datos.Implementacion;
using RECETASPRACTICA.Datos.Interfaz;
using RECETASPRACTICA.Entidades;
using RECETASPRACTICA.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private IRecetaDao dao;

        public Servicio()
        {
            dao= new RecetaDao();
        }
        public bool CrearReceta(Receta oReceta)
        {
            return dao.Crear(oReceta);
        }

        public List<Ingrediente> TraerIngrediente()
        {
            return dao.ObtenerIngrediente();
        }

        public int TraerProximaReceta()
        {
            return dao.ObtenerProximaReceta();
        }
    }
}
