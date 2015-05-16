using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class EsquemaCRM
    {
        List<EsquemaTabla> tablas;

        public EsquemaCRM() 
        {
            tablas = new List<EsquemaTabla>();

            //Query para obtener las tablas en la BD
            DataTable dt = Control_query.querySelect("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';");

            //Agregar cada tabla
            foreach(DataRow row in dt.Rows){
                EsquemaTabla tabla = new EsquemaTabla(row[0].ToString());
                tablas.Add(tabla);
            }
        }
    }
}
