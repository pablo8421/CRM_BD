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
    public partial class NuevoCampo : Form
    {
        Principal miPrincipal;
        public NuevoCampo(Principal miPrincipal)
        {
            InitializeComponent();
            this.miPrincipal = miPrincipal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String nombreCampo = textBoxNombre.Text;
            String tipoCampo;
            if (comboBoxTipo.SelectedItem != null)
                tipoCampo = comboBoxTipo.SelectedItem.ToString();
            else
                tipoCampo = "";
            String query = "ALTER TABLE cliente ADD COLUMN " + nombreCampo + " " + tipoCampo + ";";
            int valor = Control_query.query(query);
            if (valor == -5)
            {
                Console.Write("Laca");
            }
            miPrincipal.cargarVentanaPrincipal();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
