using ModeloClases.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloClases.DAL
{
    public class MedidoresDALArchivos : IMedidoresDAL
    {
        private MedidoresDALArchivos()
        {

        }

        private static MedidoresDALArchivos instancia;

        public static IMedidoresDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidoresDALArchivos();
            }
            return instancia;
        }


        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/medidores.txt";


        public List<Medidor> ObtenerMedidores()
        {
            List<Medidor> lista = new List<Medidor>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            Medidor medidor = new Medidor()
                            {
                                Codigo = arr[0],
                            };
                            lista.Add(medidor);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception)
            {
                lista = null;
            }
            return lista;
        }


    }
}
