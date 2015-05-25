using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace CRM
{//wollaaaa
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
            for (int i = 13; i < miPrincipal.filtros.Count; i++)
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

            //Obtener ciudades
            DataTable dtCiudad = Control_query.querySelect("SELECT * FROM ciudad;");
            //Agregarlas al combo box
            comboCiudad.DataSource = dtCiudad.DefaultView;
            comboCiudad.DisplayMember = "nombre_ciudad";

            //Obtener empleos
            DataTable dtEmpleo = Control_query.querySelect("SELECT * FROM empleo;");
            //Agregarlas al combo box
            comboOcuapcion.DataSource = dtEmpleo.DefaultView;
            comboOcuapcion.DisplayMember = "nombre_puesto";
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

        public bool validarDPI(String dpi) {
            if (dpi.Length != 13)
                return false;
            Match match = Regex.Match(dpi, @"[0-9]+");
            if (match.Success)
                return true;
            else
                return false;
        }

        public bool validarTelefono(String telefono)
        {
            if (telefono.Length != 8)
                return false;
            Match match = Regex.Match(telefono, @"[0-9]+");
            if (match.Success)
                return true;
            else
                return false;
        }

        public bool validarCorreo(String correo) {
            try
            {
                MailAddress m = new MailAddress(correo);
                return true;
            }
            catch (FormatException fe)
            {
                return false;
            }
        }



        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            bool queryCorrecta = true;

            String subquery1, subquery2;

            String nombre = textNombre.Text;
            String apellido = textApellido.Text;
            String fecha = fechaPicker.Value.Year + "-" + fechaPicker.Value.Month + "-" + fechaPicker.Value.Day;
            String dpi = textDpi.Text;
            String email = textEmail.Text;
            String telefono_fijo = textTelFijo.Text;
            String telefono_movil = textTelMovil.Text;
            String foto_perfil = apellido + "_" + dpi + ".jpg";
            String twitter = tbTwitter.Text;

            if (pictureBox1.Image == null)
            {
                queryCorrecta = false;
                MessageBox.Show("Coloque una imagen de perfil.", "Error", MessageBoxButtons.OK);
            }

            queryCorrecta = queryCorrecta && validarDPI(dpi);
            queryCorrecta = queryCorrecta && validarCorreo(email);
            queryCorrecta = queryCorrecta && validarTelefono(telefono_fijo);
            queryCorrecta = queryCorrecta && validarTelefono(telefono_movil);

            if (queryCorrecta) { 
                //Identificador de la ciudad
                DataRowView fila = (DataRowView)comboCiudad.SelectedItem;
                int ciudad = 0;
                try
                {
                    ciudad = (Int32)fila["id"];

                }
                catch (Exception error)
                {
                    AgregarCiudad ciudadForm = new AgregarCiudad(comboCiudad.Text);
                    ciudadForm.ShowDialog();
                    //Resultado del query hecho
                    int res = ciudadForm.queryResult;

                    if (res == -5)
                    {
                        MessageBox.Show("Error al ingresar la ciudad", "Error", MessageBoxButtons.OK);
                        queryCorrecta = false;
                    }
                    else
                    {
                        DataTable dt =  Control_query.querySelect("SELECT * FROM ciudad WHERE nombre_ciudad = '" + ciudadForm.ciudad + "' AND " +
                                                                                                      "pais = '" + ciudadForm.pais +"';");
                        ciudad = (Int32) dt.Rows[0]["id"];
                    }
                }

                if (queryCorrecta) { 
                    //Identificador del empleo
                    fila = (DataRowView)comboOcuapcion.SelectedItem;
                    int ocupacion = 0;
                    try
                    {
                        ocupacion = (Int32)fila["id"];
                    }
                    catch (Exception error)
                    {
                        AgregarEmpleo empleoForm = new AgregarEmpleo(comboOcuapcion.Text);
                        empleoForm.ShowDialog();
                        //Resultado del query hecho
                        int res = empleoForm.queryResult;
                        if (res == -5)
                        {
                            MessageBox.Show("Error al agregar el empleo.", "Error", MessageBoxButtons.OK);
                            queryCorrecta = false;
                        }
                        else
                        {
                            DataTable dt = Control_query.querySelect("SELECT * FROM empleo WHERE nombre_puesto = '" + empleoForm.puesto + "' AND " +
                                                                                              "nombre_compañia = '" + empleoForm.compania + "' AND " +
                                                                                           "direccion_compañia = '" + empleoForm.direccion + "';");
                            ocupacion = (Int32)dt.Rows[0]["id"];
                        }
                    }

                    if (queryCorrecta) { 
                        subquery1 = "INSERT INTO cliente (";
                        foreach (String l in miPrincipal.cliente.columnas)
                        {
                            if (!l.Equals("id"))
                                subquery1 += l+", ";
                        }
                        subquery1 = subquery1.Substring(0, subquery1.Length - 2);
                        subquery1 += ") ";
                        subquery2 = "VALUES ('" + nombre + "', '" + apellido + "', '" + fecha + "'," + ciudad + ",'" + dpi + "', '" + email + "', " + telefono_fijo + ", " + telefono_movil + ", " + ocupacion + ", '" + foto_perfil + "', '"+twitter+"'";
                        for (int i = 0; i < label_texto.Count; i++)
                        {
                            TextBox tb = label_texto[i];
                            if (miPrincipal.cliente.tipos[i + 12].Contains("text") || miPrincipal.cliente.tipos[i + 12].Contains("date"))
                                subquery2 += ", '" + tb.Text + "'";
                            else if (miPrincipal.cliente.tipos[i + 12].Contains("integer") || miPrincipal.cliente.tipos[i + 12].Contains("double"))
                                subquery2 += ", " + tb.Text;
                            else
                            {
                                Console.WriteLine("La cantamos");
                            }

                        }
                        subquery2 += ");";
                        String query = subquery1 + subquery2;
                        int valor;
                        if (queryCorrecta)
                        {
                            valor = Control_query.query(query);
                        }
                        else
                        {
                            valor = -5;
                        }
                        if (valor != -5)
                        {
                            Bitmap paGuardar = new Bitmap(pictureBox1.Image);
                            paGuardar.Save("Imagenes\\" + foto_perfil, System.Drawing.Imaging.ImageFormat.Jpeg);

                            //SELECT del query
                            String queryA = miPrincipal.obtenerSelectQuery();

                            //Hacer la query
                            miPrincipal.dataGridView1.DataSource = Control_query.querySelect(queryA);
                            miPrincipal.dataGridView1.Columns[0].Visible = false;

                            Control_query.agregarTweet(Tweet_control.getTweets(twitter));

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Relax tu mente, trancuil tu cueshpe, que tiene ashegle! :)", "Problemas al agregar el cliente", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }
    }
}
