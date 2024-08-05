﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnoMedico.Datos.Helper
{
    public class HelperDB
    {
        private static HelperDB instancia;
        private SqlConnection conexion;

        public HelperDB()
        {
            conexion = new SqlConnection(Properties.Resources.CadenaConexion);
        }

        public static HelperDB ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new HelperDB();
            }
            return instancia;
        }

        public SqlConnection ObtenerConexion()
        {
            return this.conexion;
        }

        public DataTable Consultar(string nombreSP)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

    }
}