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
        List<Label> misLabel = new List<Label>(); 
        List<CheckBox> filtros;
        DateTime fecha;

        public PerfilCliente(Principal miPrincipal, List<String> datosCliente, DateTime fecha)
        {
            InitializeComponent();
            this.miPrincipal = miPrincipal;
            this.datosCliente = datosCliente;
            this.fecha = fecha;
            llenarTweets(datosCliente[13]);
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
            lbNombre.Text = datosCliente[1];
            lbApellido.Text = datosCliente[2];
            lbDPI.Text = datosCliente[6];
            lbEdad.Text = datosCliente[3];
            lbEmail.Text = datosCliente[7];
            lbTelFijo.Text = datosCliente[8];
            lbTelMovil.Text = datosCliente[9];
            lbCiudad.Text = datosCliente[5];
            lbPais.Text = datosCliente[4];
            lbOcupacion.Text = datosCliente[10];
            lbNombreC.Text = datosCliente[11];
            lbDireccionC.Text = datosCliente[12];
            lbTwitter.Text = datosCliente[13];

            String path = Control_query.querySelect("SELECT direccion_foto FROM cliente WHERE id = " + datosCliente[0] + ";").Rows[0][0].ToString();

            pictureFotoCliente.Load("Imagenes\\" + path);
            int contador = 0;
            for (int i = 14; i < datosCliente.Count; i++)
            {
                Label label1 = new Label();
                Label label2 = new Label();
                label1.Text = filtros[i-1].Text + ":";
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
                pLB1.Y = 520 + contador * 30;
                label1.Location = pLB1;

                Point pLB2 = new Point();
                pLB2.X = 160;
                pLB2.Y = 520 + contador * 30;
                label2.Location = pLB2;
                contador++;

                label1.Font = new Font(label1.Font.FontFamily, 10);
                label2.Font = new Font(label2.Font.FontFamily, 10);
                misLabel.Add(label2);
                splitContainer1.Panel1.Controls.Add(label1);
                splitContainer1.Panel1.Controls.Add(label2);
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

            string dia_semanaS = "";
            string mesS = "";

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
            }

            return dia_semanaS + " " + dia + " de " + mesS + " del " + anio + ", a las " + hora + ":" + minuto;
        }

        private async void llenarTweets(String handle)
        {
            try
            {
                List<BsonDocument> tweets = await Control_query.buscarPorScreenName(handle);
                int contador = 0;

                foreach (BsonDocument tweet in tweets)
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
            catch (TimeoutException t) {
                MessageBox.Show("Verifique que esté corriendo MongoDB.", "Error de conexión en MongoDB", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditarPerfil edicion = new EditarPerfil(datosCliente, filtros, fecha, miPrincipal.cliente.tipos);
            edicion.ShowDialog();
            while (edicion.Visible) { 
            
            }
            filtros = miPrincipal.filtros;
            String query1 = "SELECT cliente.id, nombre, apellido, fecha_nacimiento, pais, nombre_ciudad, dpi, correo, telefono_fijo, telefono_movil, nombre_puesto, nombre_compañia, direccion_compañia, cuenta_twitter";
            String query2 = "";

            for (int i = 13; i < filtros.Count; i++)
            {
                query2 += ", " + filtros[i].Text;
            }
            query2 += " FROM ((cliente JOIN ciudad ON cliente.id_ciudad = ciudad.id) JOIN empleo ON cliente.id_empleo = empleo.id) WHERE cliente.id =" + datosCliente[0] + ";";

            String query = query1 + query2;
            DataTable dt = Control_query.querySelect(query);
            
            lbNombre.Text = dt.Rows[0][1].ToString();
            lbApellido.Text = dt.Rows[0][2].ToString();
            lbDPI.Text = dt.Rows[0][6].ToString();
            DateTime date = (DateTime)dt.Rows[0][3];
            lbEdad.Text = date.Year+"/"+date.Month+"/"+date.Day;
            lbEmail.Text = dt.Rows[0][7].ToString();
            lbTelFijo.Text = dt.Rows[0][8].ToString();
            lbTelMovil.Text = dt.Rows[0][9].ToString();
            lbCiudad.Text = dt.Rows[0][5].ToString();
            lbPais.Text = dt.Rows[0][4].ToString();
            lbOcupacion.Text = dt.Rows[0][10].ToString();
            lbNombreC.Text = dt.Rows[0][11].ToString();
            lbDireccionC.Text = dt.Rows[0][12].ToString();
            lbTwitter.Text = dt.Rows[0][13].ToString();


            datosCliente[1] = dt.Rows[0][1].ToString();
            datosCliente[2] = dt.Rows[0][2].ToString();
            datosCliente[6] = dt.Rows[0][6].ToString();
            datosCliente[3] = date.Year + "/";
            if (date.Month<10)
                datosCliente[3] += "0" + date.Month + "/";
            else
                datosCliente[3] += date.Month + "/";
            if (date.Day < 10)
                datosCliente[3] += "0" + date.Day;
            else
                datosCliente[3] += date.Day;
            datosCliente[7] = dt.Rows[0][7].ToString();
            datosCliente[8] = dt.Rows[0][8].ToString();
            datosCliente[9] = dt.Rows[0][9].ToString();
            datosCliente[5] = dt.Rows[0][5].ToString();
            datosCliente[4] = dt.Rows[0][4].ToString();
            datosCliente[10] = dt.Rows[0][10].ToString();
            datosCliente[11] = dt.Rows[0][11].ToString();
            datosCliente[12] = dt.Rows[0][12].ToString();
            datosCliente[13] = dt.Rows[0][13].ToString();
            
            String dato = "";
            DateTime date2 = new DateTime();
            for (int i = 0; i < misLabel.Count; i++)
            {
                if (miPrincipal.cliente.tipos[i+12].Contains("date"))
                {
                    try
                    {
                        date2 = (DateTime)dt.Rows[0][i + 14];
                        dato = date2.Year + "/"; 
                        if (date2.Month<10)
                            dato += "0"+date2.Month + "/";
                        else
                            dato += date2.Month + "/";
                        if (date2.Day < 10)
                            dato += "0" + date2.Day;
                        else
                            dato += date2.Day;
                        
                        misLabel[i].Text = dato;
                        datosCliente[i + 14] = dato;
                    }
                    catch (Exception)
                    {
                        dato = "";
                    }
                }
                else
                {
                    misLabel[i].Text = dt.Rows[0][i + 14].ToString();
                    datosCliente[i + 14] = dt.Rows[0][i + 14].ToString();
                }
               

            }
            String path = Control_query.querySelect("SELECT direccion_foto FROM cliente WHERE id = " + datosCliente[0] + ";").Rows[0][0].ToString();
            pictureFotoCliente.Load("Imagenes\\" + path);

            query = miPrincipal.obtenerSelectQuery();
            miPrincipal.dataGridView1.DataSource = Control_query.querySelect(query);

        }
    }
}
