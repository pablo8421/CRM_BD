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
    public partial class AgregarEmpleo : Form
    {
        public int queryResult;
        public string puesto, compania, direccion;

        public AgregarEmpleo()
        {
            InitializeComponent();
        }

        public AgregarEmpleo(String empleo)
        {
            InitializeComponent();
            queryResult = -5;
            //Texto de la ciudad
            textEmpleo.Text = empleo;

            //Compañias disponibles
            DataTable dtCompania = Control_query.querySelect("SELECT DISTINCT nombre_compañia FROM empleo;");
            comboCompania.DataSource = dtCompania;
            comboCompania.DisplayMember = "nombre_compañia";

            //Direcciones disponibles
            DataTable dtDirecciones = Control_query.querySelect("SELECT DISTINCT direccion_compañia FROM empleo;");
            comboDireccion.DataSource = dtDirecciones;
            comboDireccion.DisplayMember = "direccion_compañia";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            puesto = textEmpleo.Text;
            compania = comboCompania.Text;
            direccion = comboDireccion.Text;

            queryResult = Control_query.query("INSERT INTO empleo(nombre_puesto, nombre_compañia, direccion_compañia) VALUES('" + puesto + "', '" + compania + "', '" + direccion + "');");

            this.Close();
        }
    }
}
