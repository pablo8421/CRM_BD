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
    public partial class EditarPerfil : Form
    {
        List<String> datosCliente;
        List<String> camposCliente;

        List<Label> labels;
        List<TextBox> textBoxes;
        DateTimePicker fecha;

        int id;
        
        public EditarPerfil()
        {
            InitializeComponent();
        }

        public EditarPerfil(List<String> dataCliente, List<CheckBox> filtros, DateTime nacimiento)
        {
            
            id = Int32.Parse(dataCliente[0]);

            InitializeComponent();

            datosCliente = new List<String>();
            foreach(String data in dataCliente)
            {
                datosCliente.Add(data);
            }
            datosCliente.RemoveAt(0);
            
            camposCliente = new List<String>();

            labels = new List<Label>();
            textBoxes = new List<TextBox>();

            for (int i = 0; i < datosCliente.Count; i++)
            {
                //Guardar el campo
                camposCliente.Add(filtros[i].Text);

                //Generar el label de los filtros
                Label label = new Label();
                label.Text = filtros[i].Text;
                label.Location = new Point(10, 25*i);

                //Generar el textBox de los datos del cliente

                TextBox textBox = new TextBox();
                textBox.Text = datosCliente[i];
                textBox.Location = new Point(150, 25*i);

                //Guardar los componentes
                labels.Add(label);
                textBoxes.Add(textBox);

                if (i == 2)
                {
                    fecha = new DateTimePicker();
                    fecha.Value = nacimiento;
                    fecha.Location = new Point(150, 25 * i);
                    fecha.ValueChanged += fecha_ValueChanged;
                    //Agregar los componentes
                    panel.Controls.Add(label);
                    panel.Controls.Add(fecha);
                }
                else
                {
                    //Agregar los componentes
                    panel.Controls.Add(label);
                    panel.Controls.Add(textBox);
                }
            }
        }

        private void fecha_ValueChanged(object sender, EventArgs e)
        {
            textBoxes[2].Text= "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<String> campos = new List<String>();
            List<String> valores= new List<String>();
            for (int i = 0; i < textBoxes.Count; i++)
            {
                if(!datosCliente[i].Equals(textBoxes[i].Text)){
                    campos.Add(camposCliente[i]);
                    if (i != 2)
                    {
                        valores.Add(textBoxes[i].Text);
                    }
                    else
                    {
                        String date = "'" + fecha.Value.Year + "-" + fecha.Value.Month + "-" +fecha.Value.Day+ "'";
                        valores.Add(date);
                    }
                }
            }
            if (campos.Count != 0)
            {
                String query = "";
                //Realizar los SETS
                for (int i = 0; i < valores.Count; i++)
                {
                    query += campos[i] + "='" + valores[i] + "',";
                }
                //Eliminar la ultima comilla innecesaria
                query = query.Substring(0, query.Length - 1);
                
                //Generar la query en si
                query = "UPDATE cliente SET " + query + " WHERE " + "id=" + id + ";";
                int res = Control_query.query(query);
                if(res == -5){
                    MessageBox.Show("La actualizacion no se pudo hacer", "Error actualizando");
                }
                this.Close();
            }
        }
    }
}
