using MedidorElectricoApp.Comunicacion;
using ModeloClases.DAL;
using ModeloClases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedidorElectricoApp
{
    class Program
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();
        private static IMedidoresDAL medidoresDAL = MedidoresDALArchivos.GetInstancia();
        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("\n \n ¡B I E N V E N I D O \n     A L   M E N U ! \n \n Ingrese una opcion \n ");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar \n 3. Listar medidores \n 0. Salir \n");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "3":
                    MostrarMedidores();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine(" \n Opción inválida \n Volviendo al MENU...");
                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();

            while (Menu()) ;
        }

        static void Ingresar()
        {

                Console.WriteLine("Ingrese código del medidor :"); 
                string medidor = Console.ReadLine().Trim();
                Console.WriteLine("Ingrese valor de consumo : "); 
                string consumo = Console.ReadLine().Trim();

          


                string fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (String.IsNullOrEmpty(medidor) || String.IsNullOrEmpty(consumo))
            {
                Console.WriteLine("ERROR: NO PUEDE HABER CAMPOS VACIOS");
                Console.WriteLine("VOLVIENDO AL MENU...");
                Menu();
            }
            else
            {

                if (medidor.All(char.IsDigit) && consumo.All(char.IsDigit))
                {
                    int imedidor = Int32.Parse(medidor);
                    int iconsumo = Int32.Parse(consumo);

                    if (imedidor > 0 && imedidor < 51)
                    {
                        if (iconsumo > 0)
                        {
                            Console.WriteLine("\n Registrando fecha y hora... \n Volviendo al MENU \n ");

                            Lectura lectura = new Lectura()
                            {
                                Medidor = medidor,
                                Fecha = fecha,
                                Consumo = consumo,
                            };
                            lock (lecturasDAL)
                            {
                                lecturasDAL.AgregarLectura(lectura);
                            }

                        }
                        else
                        {
                            Console.WriteLine("ERROR: DEBE INGRESAR UN CONSUMO POSITIVO");
                            Console.WriteLine("VOLVIENDO AL MENU...");
                            Menu();
                        }

                    }
                    else
                    {
                        Console.WriteLine("ERROR: DEBE INGRESAR UN MEDIDOR VALIDO");
                        Console.WriteLine("VOLVIENDO AL MENU...");
                        Menu();
                    }

                }
                else
                {
                    Console.WriteLine("ERROR: LOS DATOS DEBEN SER NÚMEROS");
                    Console.WriteLine("VOLVIENDO AL MENU...");
                    Menu();
                }
            }
                

        }

        static void Mostrar()
        {
            List<Lectura> lecturas = null;
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }
            foreach (Lectura lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }

        }

        static void MostrarMedidores()
        {
            List<Medidor> medidores = null;
            lock (medidoresDAL)
            {
                medidores = medidoresDAL.ObtenerMedidores();
            }
            foreach (Medidor medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
        }

    }
}
