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
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            // show other form
            AgregarUsuario agregar = new AgregarUsuario();
            agregar.ShowDialog();

        }

        private void btnCampo_Click(object sender, EventArgs e)
        {
            NuevoCampo nc = new NuevoCampo();
            nc.ShowDialog();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Control_query.querySelect("SELECT * FROM Cliente");
            EsquemaCRM esquemaCRM = new EsquemaCRM();
            EsquemaTabla cliente = esquemaCRM.getTabla("cliente");
            List<CheckBox> filtros = new List<CheckBox>();
            List<TextBox> filtros_texto = new List<TextBox>();
            for (int i = 0; i < cliente.columnas.Count; i++) {
                CheckBox cb = new CheckBox();
                TextBox tb = new TextBox();
                cb.Text = cliente.columnas[i];
                
                Size tamanioCB = new Size();
                tamanioCB.Height = 20;
                tamanioCB.Width = 110;
                cb.Size = tamanioCB;

                Size tamanioTB = new Size();
                tamanioTB.Height = 20;
                tamanioTB.Width = 110;
                tb.Size = tamanioTB;

                Point pCB = new Point();
                pCB.X = 10;
                pCB.Y = i * 25+20;
                cb.Location = pCB;

                Point pTB = new Point();
                pTB.X = 125;
                pTB.Y = i * 25+20;
                tb.Location = pTB;

                filtros.Add(cb);
                filtros_texto.Add(tb);
            }

            for (int i = 0; i < filtros.Count; i++){
                splitContainer1.Panel1.Controls.Add(filtros[i]);
                splitContainer1.Panel1.Controls.Add(filtros_texto[i]);
            }

        }
    }
}
