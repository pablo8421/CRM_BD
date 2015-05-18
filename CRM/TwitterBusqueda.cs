using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class TwitterBusqueda : Form
    {
        String[] opciones = {"General", "Publicados por" };

        public TwitterBusqueda()
        {
            InitializeComponent();
            comboOpciones.DataSource = opciones;
        }

        private void agregarTweet(string contenido)
        {
            textTweets.Text += Environment.NewLine + contenido + Environment.NewLine;
        }

        private void buscar(String busqueda, int tipo, int limite)
        {
            //Vaciar busqueda actual
            textTweets.Text = "";

            //Para almacenar los resultados
            Tweetinvi.Core.Interfaces.ITweet[] resultados;

            //Busqueda general
            if (tipo == 0)
            {
                resultados = Tweet_control.buscarTweets(busqueda);
                //Agregar los tweets
                foreach (Tweetinvi.Core.Interfaces.ITweet tweet in resultados)
                {

                    agregarTweet("@" + tweet.Creator + ": " + tweet.Text);
                }
            }
            //Busqueda segun quien publico
            else if (tipo == 1)
            {
                resultados = Tweet_control.getTweets(busqueda, limite);
                //Verificar si se encontro el usuario
                if (resultados == null)
                {
                    textTweets.Text = "Usuario no encontrado, puede que su timeline sea privada.";
                }
                else
                {
                    foreach (Tweetinvi.Core.Interfaces.ITweet tweet in resultados)
                    {
                        
                        agregarTweet("@" + tweet.Creator + ": " + tweet.Text);
                    }
                }
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            bool correcto = true;
            if(textBusqueda.Text.Trim().Equals("")){
                MessageBox.Show("No se especifico ninguna busqueda", "Busqueda incorrecta", MessageBoxButtons.OK);
                correcto = false;
            }
            int num = 200;


            if (correcto)
            {
                buscar(textBusqueda.Text, comboOpciones.SelectedIndex, num);
            }
        }
    }
}
