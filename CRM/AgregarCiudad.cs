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
    public partial class AgregarCiudad : Form
    {
        public int queryResult;
        public string ciudad;
        public string pais;

        public AgregarCiudad()
        {
            InitializeComponent();

        }

        public AgregarCiudad(String ciudad)
        {
            InitializeComponent();
            queryResult = -5;
            //Texto de la ciudad
            textCiudad.Text = ciudad;

            //Paises disponibles
            DataTable dtPaises = Control_query.querySelect("SELECT DISTINCT pais from ciudad;");
            comboPais.DataSource = dtPaises;
            comboPais.DisplayMember = "pais";
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            ciudad = textCiudad.Text;
            pais = comboPais.Text;

            queryResult = Control_query.query("INSERT INTO ciudad(nombre_ciudad, pais) VALUES('" + ciudad + "', '" + pais + "')");
            
            this.Close();
        }
    }
}
