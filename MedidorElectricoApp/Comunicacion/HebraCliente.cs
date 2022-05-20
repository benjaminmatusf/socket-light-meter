using ModeloClases.DAL;
using ModeloClases.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedidorElectricoApp.Comunicacion
{
    class HebraCliente
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese medidor :");
            string medidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese valor de consumo :");
            string consumo = clienteCom.Leer();

            if (String.IsNullOrEmpty(medidor) || String.IsNullOrEmpty(consumo))
            {
                clienteCom.Escribir("ERROR: NO PUEDE HABER CAMPOS VACIOS \nCerrando conexion");
                clienteCom.Desconectar();
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
                            string fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                            clienteCom.Escribir("Registrando fecha y hora... \nCerrando conexion");

                            Lectura lectura = new Lectura()
                            {
                                Medidor = medidor,
                                Fecha = fecha,
                                Consumo = consumo
                            };
                            lock (lecturasDAL)
                            {
                                lecturasDAL.AgregarLectura(lectura);
                            }

                            clienteCom.Desconectar();

                        }
                        else
                        {
                            clienteCom.Escribir("ERROR: DEBE INGRESAR UN CONSUMO POSITIVO");
                            clienteCom.Escribir("DESCONECTANDO...");
                            clienteCom.Desconectar();
                        }

                    }
                    else
                    {
                        clienteCom.Escribir("ERROR: DEBE INGRESAR UN MEDIDOR VALIDO");
                        clienteCom.Escribir("DESCONECTANDO...");
                        clienteCom.Desconectar();
                    }

                }
                else
                {
                    clienteCom.Escribir("ERROR: LOS DATOS DEBEN SER NÚMEROS");
                    clienteCom.Escribir("DESCONECTANDO...");
                    clienteCom.Desconectar();
                }

            }
        }
    }
}
