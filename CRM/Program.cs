using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Control_query.iniciarConexion();

            Control_query.iniciarDBTwitter();
            Control_query.mongoPrueba();

            //Prueba EsquemaCRM
            EsquemaCRM esquema = new EsquemaCRM();
            Tweet_control tweets = new Tweet_control();

            //Control_query.agregarTweet(Tweet_control.getTweets("emisorasunidas"));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }
    }
}
