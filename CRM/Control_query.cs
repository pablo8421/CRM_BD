using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace CRM
{
    class Control_query
    {
        string host = "127.0.0.1";
        string puerto = "5432";
        string usuario = "computadora";
        string contrasenia = "123456";
        string nombre_bd = "CRM";

        NpgsqlConnection conexion;

        public void iniciarConexion() {
            string conexion_string = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    host, puerto, usuario,
                    contrasenia, nombre_bd);

            conexion = new NpgsqlConnection(conexion_string);
            conexion.Open();
        }

        public void prueba() {
            string query = "INSERT INTO CIUDAD VALUES(1, 'Puebla', 'Mexico');";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conexion);

        }
    }
}
