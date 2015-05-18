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
    }
}
