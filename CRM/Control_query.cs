using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

using MongoDB.Bson;
using MongoDB.Driver;

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

        static public List<BsonDocument> ultimoResultado;

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
            _database = _client.GetDatabase("tweet");
        }

        public async static void mongoPrueba()
        {
            var collection = _database.GetCollection<BsonDocument>("tweets");
            BsonDocument filter = new BsonDocument();

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var filtros = Builders<BsonDocument>.Filter.Eq("screenName", "EmisorasUnidas");
                        filtros = filtros & Builders<BsonDocument>.Filter.Eq("publicado.dia", 21);
                        var result = await collection.Find(filtros).ToListAsync();
                        Console.WriteLine();
                        foreach (var algo in result)
                        {
                            Console.WriteLine(algo);
                        }



                    }
                }
            }
        }

        public static string getFecha(BsonDocument tweet)
        {
            int minuto = (Int32)tweet.GetElement("publicado").Value.AsBsonDocument.GetElement("minuto").Value;
            int hora = (Int32)tweet.GetElement("publicado").Value.AsBsonDocument.GetElement("hora").Value;
            int dia = (Int32)tweet.GetElement("publicado").Value.AsBsonDocument.GetElement("dia").Value;
            int dia_semana = (Int32)tweet.GetElement("publicado").Value.AsBsonDocument.GetElement("diaSemana").Value; ;
            int mes = (Int32)tweet.GetElement("publicado").Value.AsBsonDocument.GetElement("mes").Value;
            int anio = (Int32)tweet.GetElement("publicado").Value.AsBsonDocument.GetElement("anio").Value;

            string dia_semanaS;
            string mesS;

            switch (dia_semana)
            {
                case 0:
                    dia_semanaS = "Domingo";
                    break;
                case 1:
                    dia_semanaS = "Lunes";
                    break;
                case 2:
                    dia_semanaS = "Martes";
                    break;
                case 3:
                    dia_semanaS = "Miercoles";
                    break;
                case 4:
                    dia_semanaS = "Jueves";
                    break;
                case 5:
                    dia_semanaS = "Viernes";
                    break;
                case 6:
                    dia_semanaS = "Sabado";
                    break;
                default:
                    throw new NotImplementedException("oh shit hermano, ese dia no existe");
            }

            switch (mes)
            {
                case 1:
                    mesS = "Enero";
                    break;
                case 2:
                    mesS = "Febrero";
                    break;
                case 3:
                    mesS = "Marzo";
                    break;
                case 4:
                    mesS = "Abril";
                    break;
                case 5:
                    mesS = "Mayo";
                    break;
                case 6:
                    mesS = "Junio";
                    break;
                case 7:
                    mesS = "Julio";
                    break;
                case 8:
                    mesS = "Agosto";
                    break;
                case 9:
                    mesS = "Septiembre";
                    break;
                case 10:
                    mesS = "Octubre";
                    break;
                case 11:
                    mesS = "Noviembre";
                    break;
                case 12:
                    mesS = "Diciembre";
                    break;
                default:
                    throw new NotImplementedException("oh shit hermano, ese dia no existe");

            }

            return dia_semanaS + " " + dia + " de " + mesS + " del " + anio + ", a las " + hora + ":" + minuto;
        }

        public async static Task<List<BsonDocument>> buscarPorScreenName(string handle)
        {
            var collection = _database.GetCollection<BsonDocument>("tweets");
            BsonDocument filter = new BsonDocument();

            List<BsonDocument> retorno = new List<MongoDB.Bson.BsonDocument>();

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var filtros = Builders<BsonDocument>.Filter.Eq("screenName", handle);
                        List<BsonDocument> result = await collection.Find(filtros).ToListAsync();
                        Console.WriteLine();
                        foreach (BsonDocument algo in result)
                        {
                            retorno.Add(algo);
                        }



                    }
                }
            }

            ultimoResultado = retorno;
            return retorno;
        }

        public async static void agregarTweet(Tweetinvi.Core.Interfaces.ITweet[] tweets)
        {
            if (tweets != null)
            {
                foreach (Tweetinvi.Core.Interfaces.ITweet tweet in tweets)
                {
                    string name = tweet.Creator.Name;
                    string screeName = tweet.Creator.ScreenName;
                    string contenido = tweet.Text;
                    int longitud = tweet.Length;

                    var document = new BsonDocument { {"name", name},
                                                  {"screenName", screeName},
                                                  {"contenido", contenido},
                                                  {"longitud", longitud},
                                                  {"publicado", new BsonDocument{ {"minuto", tweet.CreatedAt.Minute},
                                                                                  {"hora", tweet.CreatedAt.Hour},
                                                                                  {"dia", tweet.CreatedAt.Day},
                                                                                  {"diaSemana", tweet.CreatedAt.DayOfWeek},
                                                                                  {"mes", tweet.CreatedAt.Month},
                                                                                  {"anio", tweet.CreatedAt.Year}}}
                };

                    var collection = _database.GetCollection<BsonDocument>("tweets");
                    await collection.InsertOneAsync(document);
                }
            }
        }
    }
}
