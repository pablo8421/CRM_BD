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

        public List<CheckBox> filtros;
        public List<TextBox> filtros_texto;
        public EsquemaCRM esquemaCRM;
        public EsquemaTabla cliente;
        public EsquemaTabla ciudad;
        public EsquemaTabla empleo;
        int indiceCliente = -1;
        String nombreCliente = "";

        public Principal()
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (indiceCliente != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar a: "+nombreCliente+"?", "Eliminar cliente", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    String query = "DELETE FROM cliente WHERE id = " + indiceCliente+";";
                    Control_query.query(query);
                    indiceCliente = -1;
                    //SELECT del query
                    query = obtenerSelectQuery();

                    //Hacer la query
                    dataGridView1.DataSource = Control_query.querySelect(query);
                    dataGridView1.Columns[0].Visible = false;
                }
            }
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            // show other form
            AgregarUsuario agregar = new AgregarUsuario(this);
            agregar.ShowDialog();

        }

        private void btnCampo_Click(object sender, EventArgs e)
        {
            NuevoCampo nc = new NuevoCampo(this);
            nc.ShowDialog();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            cargarVentanaPrincipal();
        }

        public void cargarVentanaPrincipal() {

            esquemaCRM = new EsquemaCRM();
            cliente = esquemaCRM.getTabla("cliente");
            filtros = new List<CheckBox>();
            filtros_texto = new List<TextBox>();
            int contador = 1;

            for (int i = 0; i < cliente.columnas.Count; i++)
            {
                if (!(cliente.columnas[i].Equals("direccion_foto") || cliente.columnas[i].Equals("id")))
                {
                    if (!(cliente.columnas[i].Equals("id_ciudad") || cliente.columnas[i].Equals("id_empleo")))
                    {
                        CheckBox cb = new CheckBox();
                        TextBox tb = new TextBox();
                        cb.Checked = true;
                        cb.CheckedChanged += controlDataGrid;
                        cb.Text = cliente.columnas[i];

                        Size tamanioCB = new Size();
                        tamanioCB.Height = 20;
                        tamanioCB.Width = 125;
                        cb.Size = tamanioCB;

                        Size tamanioTB = new Size();
                        tamanioTB.Height = 20;
                        tamanioTB.Width = 226;
                        tb.Size = tamanioTB;

                        Point pCB = new Point();
                        pCB.X = 10;
                        pCB.Y = contador * 25 + 20;
                        cb.Location = pCB;

                        Point pTB = new Point();
                        pTB.X = 140;
                        pTB.Y = contador * 25 + 20;
                        tb.Location = pTB;

                        filtros.Add(cb);
                        filtros_texto.Add(tb);
                        contador++;
                    }
                    else if (cliente.columnas[i].Equals("id_ciudad"))
                    {
                        ciudad = esquemaCRM.getTabla("ciudad");
                        for (int j = 0; j < ciudad.columnas.Count; j++)
                        {
                            if (!ciudad.columnas[j].Equals("id"))
                            {
                                CheckBox cb = new CheckBox();
                                TextBox tb = new TextBox();
                                cb.Checked = true;
                                cb.CheckedChanged += controlDataGrid;
                                cb.Text = ciudad.columnas[j];

                                Size tamanioCB = new Size();
                                tamanioCB.Height = 20;
                                tamanioCB.Width = 125;
                                cb.Size = tamanioCB;

                                Size tamanioTB = new Size();
                                tamanioTB.Height = 20;
                                tamanioTB.Width = 226;
                                tb.Size = tamanioTB;

                                Point pCB = new Point();
                                pCB.X = 10;
                                pCB.Y = contador * 25 + 20;
                                cb.Location = pCB;

                                Point pTB = new Point();
                                pTB.X = 140;
                                pTB.Y = contador * 25 + 20;
                                tb.Location = pTB;

                                filtros.Add(cb);
                                filtros_texto.Add(tb);
                                contador++;
                            }
                        }
                    }
                    else if (cliente.columnas[i].Equals("id_empleo"))
                    {
                        empleo = esquemaCRM.getTabla("empleo");
                        for (int j = 0; j < empleo.columnas.Count; j++)
                        {
                            if (!empleo.columnas[j].Equals("id"))
                            {
                                CheckBox cb = new CheckBox();
                                TextBox tb = new TextBox();
                                cb.Checked = true;
                                cb.CheckedChanged += controlDataGrid;
                                cb.Text = empleo.columnas[j];

                                Size tamanioCB = new Size();
                                tamanioCB.Height = 20;
                                tamanioCB.Width = 125;
                                cb.Size = tamanioCB;

                                Size tamanioTB = new Size();
                                tamanioTB.Height = 20;
                                tamanioTB.Width = 226;
                                tb.Size = tamanioTB;

                                Point pCB = new Point();
                                pCB.X = 10;
                                pCB.Y = contador * 25 + 20;
                                cb.Location = pCB;

                                Point pTB = new Point();
                                pTB.X = 140;
                                pTB.Y = contador * 25 + 20;
                                tb.Location = pTB;

                                filtros.Add(cb);
                                filtros_texto.Add(tb);
                                contador++;
                            }
                        }
                    }    
                    
                }
                

            }
            String query = "SELECT cliente.id, ";
            for (int i = 0; i < filtros.Count; i++)
            {
                splitContainer1.Panel1.Controls.Add(filtros[i]);
                splitContainer1.Panel1.Controls.Add(filtros_texto[i]);
                if (!filtros[i].Equals("id"))
                {
                    query += filtros[i].Text + ", ";
                }

            }
            query = query.Substring(0, query.Length - 2);
            query += " FROM (cliente JOIN ciudad ON (cliente.id_ciudad = ciudad.id)) JOIN empleo ON (cliente.id_empleo = empleo.id)";
            dataGridView1.DataSource = Control_query.querySelect(query);
            dataGridView1.Columns[0].Visible = false;
        }

        public void controlDataGrid(object sender, EventArgs e)
        {
            for (int i = 0; i < filtros.Count-1; i++)
            {
                if (filtros[i].Checked)
                {
                    dataGridView1.Columns[i+1].Visible = true;
                }
                else {
                    dataGridView1.Columns[i+1].Visible = false;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string obtenerSelectQuery()
        {
            //SELECT del query 
            string query = "SELECT cliente.id,";

            foreach (CheckBox caja in filtros)
            {
                if (caja.Checked)
                {
                    query += caja.Text + ", ";
                }
            }
            if (query.EndsWith(", "))
            {
                query = query.Substring(0, query.Length - 2) + " ";
            }
            else
            {
                query += "* ";
            }

            //FROM del query 
            query += "FROM (cliente JOIN ciudad ON (cliente.id_ciudad = ciudad.id)) JOIN empleo ON (cliente.id_empleo = empleo.id) ";

            //WHERE del query 
            Boolean sinWhere = true;

            //Por cada filtro 
            for (int i = 0; i < filtros.Count; i++)
            {
                string subquery = "";
                //Si el filtro no se encuentra vacio
                if (filtros_texto[i].Text.Trim().Length != 0 && filtros[i].Checked)
                {
                    //Cada parametro que se desea buscar
                    String[] parametros = filtros_texto[i].Text.Trim().Split(',');
                    //Por cada parametro, unirlos por ORs
                    foreach (string parametro in parametros)
                    {
                        subquery += filtros[i].Text + " = '" + parametro.Trim() + "' OR ";
                    }
                    //Quitar el ultimo OR
                    subquery = subquery.Substring(0, subquery.Length - 3);
                    //Agregar el where si hace falta y poner AND para el siguiente filtro
                    if (sinWhere)
                    {
                        query += "WHERE " + "(" + subquery + ") AND ";
                        sinWhere = false;
                    }
                    else
                    {
                        query += "(" + subquery + ") AND ";
                    }
                }

            }
            //Si no hay where se coloca ;
            if (sinWhere)
            {
                query = query.Substring(0, query.Length - 1) + ";";
            }
            //Si hay where si quita el and del ultimo filtro
            else
            {
                query = query.Substring(0, query.Length - 5) + ";";
            }

            return query;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //SELECT del query
            string query = obtenerSelectQuery();
            
            //Hacer la query
            dataGridView1.DataSource = Control_query.querySelect(query);
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) { 
                List<String> datosCliente = new List<String>();
                String dato = "";
                List<Int32> fecha = new List<Int32>();
                for (int i = 0; i <= filtros.Count; i++) {
                        dato = dataGridView1.Rows[e.RowIndex].Cells[i].Value+"";
                        datosCliente.Add(dato);
                    if (i == 3)
                    {
                        String fecha_texto = datosCliente[i] + "";
                        Int32 dia = Convert.ToInt32(fecha_texto.Substring(0, fecha_texto.IndexOf('/')));
                        fecha.Add(dia);
                        fecha_texto = fecha_texto.Substring(fecha_texto.IndexOf('/')+1);
                        Int32 mes = Convert.ToInt32(fecha_texto.Substring(0, fecha_texto.IndexOf('/')));
                        fecha.Add(mes);
                        fecha_texto = fecha_texto.Substring(fecha_texto.IndexOf('/')+1);
                        Int32 anio = Convert.ToInt32(fecha_texto.Substring(0, 4));
                        fecha.Add(anio);
                    }
                }
                PerfilCliente perfil = new PerfilCliente(this, datosCliente, fecha);
                perfil.Show();
            }
        }

        private void btnTwitter_Click(object sender, EventArgs e)
        {
            TwitterBusqueda formTwitter = new TwitterBusqueda();
            formTwitter.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                indiceCliente = (Int32)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                nombreCliente = (String)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TweetsClientes twcliente = new TweetsClientes();
            twcliente.ShowDialog();
        }
    }
}
