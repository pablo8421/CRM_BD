using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;



namespace CRM
{
    public partial class TweetsClientes : Form
    {
        static IMongoClient _client;
        static IMongoDatabase _database;

        public TweetsClientes()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {

            String nombre = tbNombre.Text;
            String screenName = tbScreenName.Text;
            String contenido = tbContenido.Text;
            String longitud = tbLongitud.Text;
            String minuto = cbMinuto.SelectedItem.ToString();
            String hora = cbHora.SelectedItem.ToString();
            String dia = cbHora.SelectedItem.ToString();
            String fechaAño = dpFecha.Value.Year.ToString();
            String fechaMes = dpFecha.Value.Month.ToString();
            String fechaDia = dpFecha.Value.Day.ToString();
            bool bandera = false;

            _client = new MongoClient();
            _database = _client.GetDatabase("tweet");

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

                        if (!nombre.Equals(""))
                        {
                            bandera = true;
                            filtros = Builders<BsonDocument>.Filter.Eq("name", nombre);

                        }

                        if (!screenName.Equals(""))
                        {
                            if (bandera)
                            {
                                filtros = filtros & Builders<BsonDocument>.Filter.Eq("screenName", screenName);
                            }
                            else
                            {
                                bandera = true;
                                filtros = Builders<BsonDocument>.Filter.Eq("name", nombre);
                            }
                        }


                    }
                }
            }

            

            var result = await collection.Find(filtros).ToListAsync();
            

        }
    }
}
