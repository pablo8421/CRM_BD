using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace CRM
{
    static class Control_query
    {
        //Datos para conectarse a la BD
        static string host = "127.0.0.1";
        static string puerto = "5432";
        static string usuario = "computadora";
        static string contrasenia = "123456";
        static string nombre_bd = "CRM";

        static NpgsqlConnection conexion;

        public static void iniciarConexion()
        {
            string conexion_string = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    host, puerto, usuario,
                    contrasenia, nombre_bd);

            conexion = new NpgsqlConnection(conexion_string);
            conexion.Open();
        }

        public static void prueba() 
        {
            string query = "INSERT INTO CIUDAD VALUES(1, 'Puebla', 'Mexico');";

            NpgsqlCommand da = new NpgsqlCommand(query, conexion);

            //Console.WriteLine(da.ExecuteNonQuery());
            
        }

        static public DataTable querySelect(String query)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conexion);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            return dt;
        }

        static public int query(String query)
        {
            NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
            try
            {
                return comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return -5;
            }
        }
    }
}
