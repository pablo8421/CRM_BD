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
        public EsquemaTabla(string nombre)
        {
            //Query para obtener las columnas de la tabla en la BD
            DataTable dt = Control_query.querySelect("SELECT * FROM information_schema.columns WHERE table_name ='" + nombre + "';");

            Console.WriteLine(nombre);
            
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("\t" + row["column_name"]);
            }
        }
    }
}
