using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedidorElectricoApp.Comunicacion
{
    public class HebraServidor
    {
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket server = new ServerSocket(puerto);
            Console.WriteLine("S: Iniciando servidor en puerto {0}", puerto);
            if (server.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("\nS: Esperando Cliente...");
                    Socket cliente = server.ObtenerCliente();
                    Console.WriteLine("\nS: Cliente recibido \n ");

                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("No se puede levantar server en {0}", puerto);
            }
        }
    }
}
