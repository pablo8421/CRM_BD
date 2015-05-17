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
    public partial class AgregarUsuario : Form
    {
        Principal miPrincipal;
        public List<Label> label;
        public List<TextBox> label_texto;
        public AgregarUsuario(Principal miPrincipal)
        {
            InitializeComponent();
            this.miPrincipal = miPrincipal;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboOcuapcion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void cargarOtros()
        {
            label = new List<Label>();
            label_texto = new List<TextBox>();
            int contador = 1;
            for (int i = 9; i < miPrincipal.filtros.Count; i++)
            {
                Label lb = new Label();
                TextBox tb = new TextBox();

                lb.Text = miPrincipal.filtros[i].Text+":";

                Size tamanioCB = new Size();
                tamanioCB.Height = 20;
                tamanioCB.Width = 110;
                lb.Size = tamanioCB;

                Size tamanioTB = new Size();
                tamanioTB.Height = 20;
                tamanioTB.Width = 226;
                tb.Size = tamanioTB;

                Point pLB = new Point();
                pLB.X = 10;
                pLB.Y = contador * 25 + 20;
                lb.Location = pLB;

                Point pTB = new Point();
                pTB.X = 122;
                pTB.Y = contador * 25 + 20;
                tb.Location = pTB;

                label.Add(lb);
                label_texto.Add(tb);
                contador++;
            }
            for (int i = 0; i < label.Count; i++)
            {
                splitContainer1.Panel2.Controls.Add(label[i]);
                splitContainer1.Panel2.Controls.Add(label_texto[i]);
            }
        }

        private void AgregarUsuario_Load(object sender, EventArgs e)
        {
            cargarOtros();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png |" +
                                     "All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                try
                {
                    //Obtener la imagen obtenida del archivo
                    PictureBox imagen = new PictureBox();
                    imagen.Image = new Bitmap(openFileDialog1.FileName);

                    //Mostrar la imagen
                    pictureBox1.Image = imagen.Image;
                }
                catch (Exception error)
                {
                    MessageBox.Show("El archivo seleccionado no era una archivo de imagen valido.", "Error al cargar imagen", MessageBoxButtons.OK);
                }
            }
        }
    }
}
