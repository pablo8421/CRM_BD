using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool validarCampo(String campo)
        {
            Match match = Regex.Match(campo, @"[a-zA-Z_][a-zA-Z0-9_]*$");
            if (match.Success)
                return true;
            else
            {
                MessageBox.Show("El nombre del campo es invalido.", "Error en el nombre del campo", MessageBoxButtons.OK);
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool queryCorrecta = true;
            String nombreCampo = textBoxNombre.Text;
            String tipoCampo = "";
            queryCorrecta = queryCorrecta && validarCampo(nombreCampo);

            if (comboBoxTipo.SelectedItem != null){
                tipoCampo = comboBoxTipo.SelectedItem.ToString();

                
            }

            else {
                MessageBox.Show("Debe seleccionar un tipo de campo", "Error en el tipo de campo", MessageBoxButtons.OK);
                queryCorrecta = false;
            }
            if (queryCorrecta)
            {
                String query = "ALTER TABLE cliente ADD COLUMN " + nombreCampo + " " + tipoCampo + ";";
                int valor = Control_query.query(query);
                if (valor == -5)
                {
                    MessageBox.Show("Hubo un error en el ingreso de datos.", "Error en el ingreso de datos", MessageBoxButtons.OK);
                }
                miPrincipal.cargarVentanaPrincipal();
                this.Close();
            }   

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
