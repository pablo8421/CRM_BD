using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class EsquemaTabla
    {
        string nombre;

        List<String> columnas;
        List<String> tipos;

        public EsquemaTabla(string nombre)
        {
            this.nombre = nombre;
            columnas = new List<string>();
            tipos = new List<string>();

            //Query para obtener las columnas de la tabla en la BD
            DataTable dt = Control_query.querySelect("SELECT * FROM information_schema.columns WHERE table_name ='" + nombre + "';");
            
            foreach (DataRow row in dt.Rows)
            {
                //Guardar el nombre de la columna
                columnas.Add(row["column_name"].ToString());

                //Obtener el tipo de la columna
                string tipo = row["data_type"].ToString();
                if (tipo.Equals("character varying"))
                {
                    tipo = "varchar, " + row["character_maximum_length"].ToString() + "";
                }

                //Guardar el tipo de la columna
                tipos.Add(tipo);
                
            }

            mostrarEnConsola();
        }

        public void mostrarEnConsola() {

            Console.WriteLine(nombre);
            for (int i = 0; i < columnas.Count; i++)
            {
                Console.WriteLine("\t" + columnas[i] + " - " + tipos[i]);
            }
        }
    }
}
