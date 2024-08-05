using RECETASPRACTICA.Datos.Interfaz;
using RECETASPRACTICA.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RECETASPRACTICA.Datos.Implementacion
{
    public class RecetaDao : IRecetaDao
    {
        public bool Crear(Receta oReceta)
        {
            bool resultado = true;
            SqlConnection conexion = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            try
            {
                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("SP_INSERTAR_RECETA", conexion, t);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@tipo_receta", oReceta.TipoReceta);
                comando.Parameters.AddWithValue("@nombre", oReceta.Nombre);
                comando.Parameters.AddWithValue("@cheff", oReceta.Cheff);


                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@id";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);
                comando.ExecuteNonQuery();


                int idReceta = (int)parametro.Value;
                int detallenro = 1;
                foreach (DetalleReceta det in oReceta.detalle)
                {
                    if (det.Ingrediente != null)//en este caso xq el detalle es nulo en la base de datos
                    {
                        SqlCommand cmd = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_receta", idReceta);
                        cmd.Parameters.AddWithValue("@id_ingrediente", det.Ingrediente.IngredienteId);//no olvidar poner ingrediente.ingredienteid xq no va al sql
                        cmd.Parameters.AddWithValue("@cantidad", det.Cantidad);
                        

                        cmd.ExecuteNonQuery();
                        detallenro++;
                    }
                }
                t.Commit();

            }
            catch
            {
                if (t != null)
                    t.Rollback();
                resultado = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return resultado;
        }

        public List<Ingrediente> ObtenerIngrediente()
        {
            List<Ingrediente> lIngrediente = new List<Ingrediente>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_CONSULTAR_INGREDIENTES");
            foreach (DataRow fila in tabla.Rows)
            {
                //lo que esta en la clase ingrediente=lo que esta en la tabla del sql
                int ingredienteId = int.Parse(fila["id_ingrediente"].ToString());
                string nombre = fila["n_ingrediente"].ToString();
                string unidad = fila["unidad_medida"].ToString();

                Ingrediente i = new Ingrediente(ingredienteId, nombre, unidad);

                lIngrediente.Add(i);
            }
            return lIngrediente;
        }

        public int ObtenerProximaReceta()
        {
            SqlConnection conexion = HelperDao.ObtenerInstancia().ObtenerConexion();
            conexion.Open();
            SqlCommand comando = new SqlCommand("SP_PROXIMO_ID", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "@next";
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;

            comando.Parameters.Add(parametro);//agregar el parametro al procedimiento almacenado
            comando.ExecuteNonQuery();

            conexion.Close();

            return (int)parametro.Value;
        }
    }
}
