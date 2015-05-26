using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class EditarPerfil : Form
    {
        List<String> datosCliente;
        List<String> camposDeCliente;
        List<String> tiposCamposExtra;
        List<Label> labels;
        List<TextBox> textBoxes;
        DateTimePicker fecha;

        ComboBox ciudad;
        ComboBox empleo;

        bool hayFoto;
        int id;
        
        public EditarPerfil()
        {
            InitializeComponent();
        }

        public EditarPerfil(List<String> dataCliente, List<CheckBox> filtros, DateTime nacimiento, List<String> tiposCamposExtra)
        {
            this.tiposCamposExtra = tiposCamposExtra;
            id = Int32.Parse(dataCliente[0]);

            InitializeComponent();

            datosCliente = new List<String>();
            foreach(String data in dataCliente)
            {
                datosCliente.Add(data);
            }
            datosCliente.RemoveAt(0);
            String path = Control_query.querySelect("SELECT direccion_foto FROM cliente WHERE id = " + id + ";").Rows[0][0].ToString();
            picture.Load("Imagenes\\" + path);

            camposDeCliente = new List<String>();

            labels = new List<Label>();
            textBoxes = new List<TextBox>();

            int c = 1;
            for (int i = 0; i < datosCliente.Count; i++)
            {
                //Guardar el campo
                camposDeCliente.Add(filtros[i].Text);

                //Generar el label de los filtros
                Label label = new Label();
                label.Text = filtros[i].Text;
                label.Location = new Point(10, 25*c);

                //Generar el textBox de los datos del cliente

                TextBox textBox = new TextBox();
                textBox.Text = datosCliente[i];
                textBox.Location = new Point(150, 25*c);

                //Guardar los componentes
                labels.Add(label);
                textBoxes.Add(textBox);

                if (i == 2)
                {
                    fecha = new DateTimePicker();
                    fecha.Value = nacimiento;
                    fecha.Location = new Point(150, 25 * c);
                    fecha.ValueChanged += fecha_ValueChanged;
                    //Agregar los componentes
                    panel.Controls.Add(label);
                    panel.Controls.Add(fecha);
                    c++;
                }
                else if(i == 3)
                {

                    //Configurar el comboBox
                    actualizarComboBoxes();

                    //Agregarlo al panel
                    panel.Controls.Add(label);
                    panel.Controls.Add(ciudad);
                    c++;
                }
                else if(i == 9){
                    //Agregarlo al panel
                    panel.Controls.Add(label);
                    panel.Controls.Add(empleo);
                    c++;
                }
                //Nada
                else if (i == 4 || i == 10 || i == 11)
                {

                }
                else
                {
                    //Agregar los componentes
                    panel.Controls.Add(label);
                    panel.Controls.Add(textBox);
                    c++;
                }
            }
        }

        private void fecha_ValueChanged(object sender, EventArgs e)
        {
            textBoxes[2].Text= "";
        }

        public bool validarDPI(String dpi)
        {
            if (dpi.Length != 13)
            {
                MessageBox.Show("La longitud del No. DPI no es correcta.", "Error en la longitud del DPI", MessageBoxButtons.OK);
                return false;
            }

            Match match = Regex.Match(dpi, @"[0-9]+");
            if (match.Success)
                return true;
            else
            {
                MessageBox.Show("El No. DPI que ingresó es invalido.", "Error en el DPI", MessageBoxButtons.OK);
                return false;
            }

        }

        public bool validarTelefono(String telefono)
        {
            if (telefono.Length != 8)
            {
                MessageBox.Show("La longitud del No. de telefono no es correcta.", "Error en la longitud del No. de telefono", MessageBoxButtons.OK);
                return false;
            }
            Match match = Regex.Match(telefono, @"[0-9]+");
            if (match.Success)
                return true;
            else
            {
                MessageBox.Show("El No. de telefono que ingresó es invalido.", "Error en el telofono", MessageBoxButtons.OK);
                return false;
            }
        }

        public bool validarCorreo(String correo)
        {
            try
            {
                MailAddress m = new MailAddress(correo);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("El correo que ingresó es invalido", "Error en el correo", MessageBoxButtons.OK);
                return false;
            }
        }

        public bool validarFecha(String fecha)
        {
            DateTime dt;
            return DateTime.TryParseExact(fecha, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
        }

        public bool validarEntero(String entero)
        {
            try
            {
                Int64 m = Convert.ToInt64(entero);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool validarReal(String real)
        {
            try
            {
                Double m = Convert.ToDouble(real);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<String> camposCliente = new List<String>();
            List<String> valoresCliente = new List<String>();
            bool queryValida = true;

            queryValida = queryValida && validarDPI(textBoxes[5].Text);
            queryValida = queryValida && validarCorreo(textBoxes[6].Text);
            queryValida = queryValida && validarTelefono(textBoxes[7].Text);
            queryValida = queryValida && validarTelefono(textBoxes[8].Text);

            if (queryValida) { 
                textBoxes[4].Text = "";
                textBoxes[10].Text = "";

                for (int i = 0; i < textBoxes.Count; i++)
                {
                    if (queryValida) { 
                        if(!datosCliente[i].Equals(textBoxes[i].Text)){
                            if(i != 3 && i != 4 && !(i >= 9 && i <= 11)){
                                camposCliente.Add(camposDeCliente[i]);
                                if (i != 2)
                                {
                                    if (i >= 13) {
                                        if (tiposCamposExtra[i-1].Contains("text")) {
                                            valoresCliente.Add(textBoxes[i].Text);
                                        }
                                        else if (tiposCamposExtra[i-1].Contains("date"))
                                        {
                                            if (validarFecha(textBoxes[i].Text))
                                            {
                                                valoresCliente.Add(textBoxes[i].Text);
                                            }
                                            else {
                                                queryValida = false;
                                                MessageBox.Show("El valor '" + textBoxes[i].Text + "' debe ser una fecha. Formato: yyyy/mm/dd", "Error", MessageBoxButtons.OK);
                                            }
                                        }
                                        else if (tiposCamposExtra[i-1].Contains("integer"))
                                        {
                                            if (validarEntero(textBoxes[i].Text))
                                            {
                                                valoresCliente.Add(textBoxes[i].Text);
                                            }
                                            else
                                            {
                                                queryValida = false;
                                                MessageBox.Show("El valor '" + textBoxes[i].Text + "' debe ser un número entero.", "Error", MessageBoxButtons.OK);
                                            }
                                        }
                                        else if (tiposCamposExtra[i-1].Contains("double"))
                                        {
                                            if (validarReal(textBoxes[i].Text))
                                            {
                                                valoresCliente.Add(textBoxes[i].Text);
                                            }
                                            else
                                            {
                                                queryValida = false;
                                                MessageBox.Show("El valor '" + textBoxes[i].Text + "' debe ser un número real.", "Error", MessageBoxButtons.OK);
                                            }
                                        }
                                    } 
                                    else { 
                                        valoresCliente.Add(textBoxes[i].Text);
                                    }
                                }
                                else
                                {
                                    String date = fecha.Value.Year + "-" + fecha.Value.Month + "-" + fecha.Value.Day;
                                    valoresCliente.Add(date);
                                }

                            }
                            else if (i < 5)
                            {
                                camposCliente.Add("id_ciudad");
                                valoresCliente.Add(((DataRowView)ciudad.SelectedValue)[0].ToString());
                            }
                            else
                            {
                                camposCliente.Add("id_empleo");
                                valoresCliente.Add(((DataRowView)empleo.SelectedValue)[0].ToString());
                            }
                        }
                    }
                }
                if (queryValida) { 
                    if (camposCliente.Count != 0)
                    {
                        String query = "";
                        //Realizar los SETS
                        for (int i = 0; i < valoresCliente.Count; i++)
                        {
                            query += camposCliente[i] + "='" + valoresCliente[i] + "',";
                        }
                        //Eliminar la ultima comilla innecesaria
                        query = query.Substring(0, query.Length - 1);

                        insertarTweetsNuevos();

                        //Generar la query en si
                        query = "UPDATE cliente SET " + query + " WHERE " + "id=" + id + ";";
                        int res = Control_query.query(query);
                        if(res == -5)
                        {
                            MessageBox.Show("La actualizacion no se pudo hacer", "Error actualizando");
                        }
                        else
                        {
                            borrarTweetsViejos();
                        }
                    }
                    if (hayFoto)
                    {
                        String path = Control_query.querySelect("SELECT direccion_foto FROM cliente WHERE id = " + id + ";").Rows[0][0].ToString();
                        path = path.Substring(0, path.Length - 4) + "_next.jpg";
                        Control_query.query("UPDATE cliente SET direccion_foto='" + path + "' WHERE id=" + id + ";");
                        Bitmap paGuardar = new Bitmap(picture.Image);
                        paGuardar.Save("Imagenes\\" + path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    this.Close();
                }
            }
        }

        private void borrarTweetsViejos()
        {
            if (!datosCliente[12].Equals(textBoxes[12].Text))
            {
                string handle = datosCliente[12];
                Control_query.borrarTweets(handle);
            }
        }

        private void insertarTweetsNuevos()
        {
            if(!datosCliente[12].Equals(textBoxes[12].Text))
            {
                string handle = textBoxes[12].Text;
                Control_query.agregarTweet(Tweet_control.getTweets(handle));
            }
        }

        private void actualizarComboBoxes()
        {
            ciudad = new ComboBox();
            empleo = new ComboBox();

            //CIUDAD

            //Obtener los datos
            DataTable origen = Control_query.querySelect("SELECT * FROM ciudad;");

            //Generar lo que se va a mostrar
            DataTable datos = new DataTable();
            datos.Columns.Add("indice");
            datos.Columns.Add("nombre");

            //Obtener el default
            int numSelect = (Int32)Control_query.querySelect("SELECT id_ciudad FROM cliente WHERE id = " + id + ";").Rows[0][0];


            //Llenar los datos
            foreach (DataRow row in origen.Rows)
            {
                datos.Rows.Add(row["id"], row["nombre_ciudad"] + ", " + row["pais"]);
            }

            //Configurar el comboBox
            ciudad.DataSource = datos;
            ciudad.DisplayMember = "nombre";
            ciudad.Location = new Point(150, 25 * 4);
            ciudad.DropDownStyle = ComboBoxStyle.DropDownList;

            //EMPLEO

            //Obtener los datos
            origen = Control_query.querySelect("SELECT * FROM empleo;");

            //Generar lo que se va a mostrar
            datos = new DataTable();
            datos.Columns.Add("indice");
            datos.Columns.Add("nombre");

            //Obtener el default
            numSelect = (Int32)Control_query.querySelect("SELECT id_ciudad FROM cliente WHERE id = " + id + ";").Rows[0][0];


            //Llenar los datos
            foreach (DataRow row in origen.Rows)
            {
                datos.Rows.Add(row["id"], row["nombre_puesto"] + ", " + row["nombre_compañia"]);

            }

            //Configurar el comboBox
            empleo.DataSource = datos;
            empleo.DisplayMember = "nombre";
            empleo.Location = new Point(150, 25 * 9);
            empleo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnCiudad_Click(object sender, EventArgs e)
        {
            AgregarCiudad agregar = new AgregarCiudad("");
            agregar.ShowDialog();
            while (agregar.Visible)
            {
                //Solo se espera
            }
            panel.Controls.Remove(ciudad);

            actualizarComboBoxes();
            ciudad.Refresh();
            panel.Controls.Add(ciudad);
            this.Update();
        }

        private void btnEmpleo_Click(object sender, EventArgs e)
        {
            AgregarEmpleo agregar = new AgregarEmpleo("");
            agregar.ShowDialog();
            while (agregar.Visible)
            {
                //Solo se espera
            }
            panel.Controls.Remove(empleo);

            actualizarComboBoxes();
            empleo.Refresh();
            panel.Controls.Add(empleo);
            this.Update();
        }

        private void btnFoto_Click(object sender, EventArgs e)
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
                    picture.Image = imagen.Image;
                    hayFoto = true;
                }
                catch (Exception error)
                {
                    MessageBox.Show("El archivo seleccionado no era una archivo de imagen valido.", "Error al cargar imagen", MessageBoxButtons.OK);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
