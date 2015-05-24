using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MongoDB.Driver;
using MongoDB.Bson;

namespace CRM
{
    public partial class PerfilCliente : Form
    {
        Principal miPrincipal;
        List<String> datosCliente;
        List<CheckBox> filtros;
        public PerfilCliente(Principal miPrincipal, List<String> datosCliente)
        {
            InitializeComponent();
            this.miPrincipal = miPrincipal;
            this.datosCliente = datosCliente;
            llenarTweets(datosCliente[12]);
        }
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void PerfilCliente_Load(object sender, EventArgs e)
        {
            cargarPerfil();
        }
        public void cargarPerfil()
        {
            filtros = miPrincipal.filtros;
            lbNombre.Text = datosCliente[0];
            lbApellido.Text = datosCliente[1];
            lbDPI.Text = datosCliente[5];
            lbEdad.Text = datosCliente[2];
            lbEmail.Text = datosCliente[6];
            lbTelFijo.Text = datosCliente[7];
            lbTelMovil.Text = datosCliente[8];
            lbCiudad.Text = datosCliente[4];
            lbPais.Text = datosCliente[3];
            lbOcupacion.Text = datosCliente[9];
            lbNombreC.Text = datosCliente[10];
            lbDireccionC.Text = datosCliente[11];
            lbTwitter.Text = datosCliente[12];

            String path = datosCliente[1] + "_" + datosCliente[5] + ".jpg";
            pictureFotoCliente.Load("Imagenes\\" + path);
            int contador = 0;
            for (int i = 12; i < datosCliente.Count; i++)
            {
                Label label1 = new Label();
                Label label2 = new Label();
                label1.Text = filtros[i].Text + ":";
                label2.Text = datosCliente[i];

                Size tamanioLB1 = new Size();
                tamanioLB1.Height = 20;
                tamanioLB1.Width = 125;
                label1.Size = tamanioLB1;

                Size tamanioLB2 = new Size();
                tamanioLB2.Height = 20;
                tamanioLB2.Width = 226;
                label2.Size = tamanioLB2;

                Point pLB1 = new Point();
                pLB1.X = 12;
                pLB1.Y = 467 + contador * 30;
                label1.Location = pLB1;

                Point pLB2 = new Point();
                pLB2.X = 160;
                pLB2.Y = 467 + contador * 30;
                label2.Location = pLB2;
                contador++;
            }
        }

        private string getFecha(BsonDocument tweet)
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

        private async void llenarTweets(String handle)
        {
            List<BsonDocument> tweets = await Control_query.buscarPorScreenName(handle);
            int contador = 0;

            foreach(BsonDocument tweet in tweets)
            {
                string texto = "";
                texto += "Handle: " + tweet.GetElement("screenName").Value + Environment.NewLine;
                texto += tweet.GetElement("contenido").Value + Environment.NewLine;
                texto += "Publicado el: " + getFecha(tweet) + Environment.NewLine;
                texto += Environment.NewLine;
                textTweets.Text += texto;
                
                contador++;
                if (contador >= 200)
                {
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
