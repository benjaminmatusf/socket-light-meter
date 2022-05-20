using ModeloClases.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloClases.DAL
{
    public class LecturasDALArchivos : ILecturasDAL
    {
        private LecturasDALArchivos() {
        
        }

        private static LecturasDALArchivos instancia;
 
        public static ILecturasDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturasDALArchivos();
            }
            return instancia;
        }


        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";

        public void AgregarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(lectura.Medidor + "|" + lectura.Fecha + "|" + lectura.Consumo);
                    write.Flush();
                }
            }
            catch (Exception)
            {

            }

        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lista = new List<Lectura>();
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
                            Lectura lectura = new Lectura()
                            {
                                Medidor = arr[0],
                                Fecha = arr[1],
                                Consumo = arr[2],
                            };
                            lista.Add(lectura);
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
