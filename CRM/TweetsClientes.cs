﻿using System;
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
            String minuto = "";
            String hora =  "";
            int dia = -1;
            String fechaAño = ""; 
            String fechaMes = "";
            String fechaDia = ""; 
            bool bandera = false;
            tbTweets.Text = "";

            if (cbMinuto.SelectedItem != null){
                if (cbMinuto.SelectedIndex != 0)
                    minuto = cbMinuto.SelectedItem.ToString();
            }

            if (cbHora.SelectedItem != null){
                if (cbHora.SelectedIndex != 0)
                    hora = cbHora.SelectedItem.ToString();
            }

            if (cbDia.SelectedIndex - 1 > -1)
            {
                dia = cbDia.SelectedIndex-1;
            }

            if (buscarFecha.Checked)
            {
                fechaAño = dpFecha.Value.Year.ToString();
                fechaMes = dpFecha.Value.Month.ToString();
                fechaDia = dpFecha.Value.Day.ToString();
            }

            _client = new MongoClient();
            _database = _client.GetDatabase("tweet");

            var collection = _database.GetCollection<BsonDocument>("tweets");
            BsonDocument filter = new BsonDocument();
            bool done = false;
            try
            {
                using (var cursor = await collection.FindAsync(filter))
                {
                    int contador = 0;
                    while (await cursor.MoveNextAsync())
                    {
                        var batch = cursor.Current;
                        foreach (var document in batch)
                        {
                            var filtros = Builders<BsonDocument>.Filter.Eq("name", nombre);

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
                                    filtros = Builders<BsonDocument>.Filter.Eq("screenName", screenName);
                                }
                            }

                            if (!contenido.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Regex("contenido", contenido);
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Regex("contenido", contenido);
                                }
                            }

                            if (!longitud.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Lte("longitud", Convert.ToInt64(longitud));
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Lte("longitud", Convert.ToInt64(longitud));
                                }
                            }

                            if (!minuto.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Lte("publicado.minuto", Convert.ToInt64(minuto));
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Lte("publicado.minuto", Convert.ToInt64(minuto));
                                }
                            }

                            if (!hora.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Lte("publicado.hora", Convert.ToInt64(hora));
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Lte("publicado.hora", Convert.ToInt64(hora));
                                }
                            }

                            if (dia != -1)
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Eq("publicado.diaSemana", dia);
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Eq("publicado.diaSemana", dia);
                                }
                            }

                            if (!fechaDia.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Lte("publicado.dia", Convert.ToInt64(fechaDia));
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Lte("publicado.dia", Convert.ToInt64(fechaDia));
                                }
                            }

                            if (!fechaMes.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Lte("publicado.mes", Convert.ToInt64(fechaMes));
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Lte("publicado.mes", Convert.ToInt64(fechaMes));
                                }
                            }

                            if (!fechaAño.Equals(""))
                            {
                                if (bandera)
                                {
                                    filtros = filtros & Builders<BsonDocument>.Filter.Lte("publicado.anio", Convert.ToInt64(fechaAño));
                                }
                                else
                                {
                                    bandera = true;
                                    filtros = Builders<BsonDocument>.Filter.Lte("publicado.anio", Convert.ToInt64(fechaAño));
                                }
                            }
                            if (!done)
                            {
                                done = true;
                                if (bandera)
                                {
                                    var result = await collection.Find(filtros).ToListAsync();
                                    foreach (BsonDocument tweet in result)
                                    {
                                        string texto = "";
                                        texto += "Handle: " + tweet.GetElement("screenName").Value + Environment.NewLine;
                                        texto += tweet.GetElement("contenido").Value + Environment.NewLine;
                                        texto += "Publicado el: " + Control_query.getFecha(tweet) + Environment.NewLine;
                                        texto += Environment.NewLine;
                                        tbTweets.Text += texto;
                                        contador++;
                                    }
                                }
                                else
                                {
                                    filtros = new BsonDocument();
                                    var result = await collection.Find(filtros).ToListAsync();
                                    foreach (BsonDocument tweet in result)
                                    {
                                        string texto = "";
                                        texto += "Handle: " + tweet.GetElement("screenName").Value + Environment.NewLine;
                                        texto += tweet.GetElement("contenido").Value + Environment.NewLine;
                                        texto += "Publicado el: " + Control_query.getFecha(tweet) + Environment.NewLine;
                                        texto += Environment.NewLine;
                                        tbTweets.Text += texto;
                                        contador++;
                                    }
                                }
                            }
                        }
                    }
                    tbTweets.Text += "\nCantidad de resultados obtenidos: " + contador;
                }
            }
            catch (TimeoutException t) {
                MessageBox.Show("Verifique que esté corriendo MongoDB.", "Error de conexión en MongoDB", MessageBoxButtons.OK);
            }
        }
    }
}
