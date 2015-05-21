using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

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

        static IMongoClient _client;
        static IMongoDatabase _database;

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

        public static void iniciarDBTwitter(){
            _client = new MongoClient();
            _database = _client.GetDatabase("test");
        }

        public async static void mongoPrueba()
        {
            var collection = _database.GetCollection<BsonDocument>("test");
            BsonDocument filter = new BsonDocument();

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var filtros = Builders<BsonDocument>.Filter.Eq("a", 1);
                        var result = await collection.Find(filtros).ToListAsync();
                        Console.Write("wiii");
                        Console.WriteLine(result[0]);


                    }
                }
            }
        }

        public static void agregarTweet()
        {

        }

    }
}
