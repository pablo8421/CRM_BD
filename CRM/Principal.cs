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
            String query = "SELECT ";
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
        }

        public void controlDataGrid(object sender, EventArgs e)
        {
            int contador = 0;
            for (int i = 0; i < filtros.Count; i++)
            {
                if (filtros[i].Checked)
                {
                    contador++;
                }
            }
            if (contador >= 1)
            {
                dataGridView1.Show();
                String query = "SELECT ";
                foreach (CheckBox cb in filtros)
                {
                    if (cb.Checked)
                    {
                        if (!cb.Equals("id"))
                        {

                            query += cb.Text + ", ";
                        }
                    }
                }
                query = query.Substring(0, query.Length - 2);
                query += " FROM (cliente JOIN ciudad ON (cliente.id_ciudad = ciudad.id)) JOIN empleo ON (cliente.id_empleo = empleo.id)";
                dataGridView1.DataSource = Control_query.querySelect(query);
            }
            else
            {
                dataGridView1.Hide();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //SELECT del query
            string query = "SELECT ";

            foreach (CheckBox caja in filtros)
            {
                if(caja.Checked)
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



            //Hacer la query
            //Console.WriteLine(query);
            dataGridView1.DataSource = Control_query.querySelect(query);
        }
    }
}
