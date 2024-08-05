using ExamenSLN.datos;
using ExamenSLN.dominio;
using RecetasSLN.datos.Interfaz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenSLN.datos.DTOs;

namespace RecetasSLN.datos.Implementacion
{
    public class EnvioDao : IEnvioDao
    {
        public bool CancelarEnvio(int codigo)
        {
            throw new NotImplementedException();
        }

        public bool RegistrarEnvio(Envio oEnvio)
        {
            bool resultado = true;
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand();
                t = conexion.BeginTransaction();
                comando.Connection = conexion;
                comando.Transaction = t;
                comando.CommandText = "SP_REGISTRAR_ENVIO";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@fecha_envio", oEnvio.FechaEnvio);
                comando.Parameters.AddWithValue("@direccion", oEnvio.direccion);
                comando.Parameters.AddWithValue("@dni_cliente", oEnvio.DniCliente);
                comando.ExecuteNonQuery();
                t.Commit();

            }

            catch (Exception)
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

        public List<Envio> TraerEnviosFiltrados(string dni, DateTime desde, DateTime hasta)
        {
            List<Envio> lEnvio = new List<Envio>();
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@dni_cliente", dni));
            lst.Add(new Parametro("@fecha_desde", desde));
            lst.Add(new Parametro("@fecha_hasta", hasta));



            DataTable tabla = HelperDB.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_ENVIOS", lst);
            foreach (DataRow fila in tabla.Rows)//para recorrer la tabla, es el mapeo de presupuesto
            {

                Envio e = new Envio();

                //lo q esta en el formulario (grilla) lo parseo con lo q esta en la base de datos
                e.Codigo = fila[0].ToString();
                e.DniCliente = fila[4].ToString();//si no sabemos la posicion ponemos el nombre del campo "cliente"
                e.FechaEnvio = DateTime.Parse(fila[1].ToString());
                e.Estado = fila[3].ToString();
                lEnvio.Add(e);
            }
            return lEnvio;
        }
    }
}
