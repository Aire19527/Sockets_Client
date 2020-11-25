using System;

using System.Net;               //   Paso 1
using System.Net.NetworkInformation;
using System.Net.Sockets;       //   Paso 1
using System.Text;
using System.Threading;

namespace Sockes_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Conectar();
        }

        public static void Conectar()

        {

            Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // paso 2 - creamos el socket
            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse("192.168.1.36"), 1234);

            //paso 3 - Acá debemos poner la Ip del servidor, y el puerto de escucha del servidor

            //Yo puse esa porque corrí las dos aplicaciones en la misma pc

            try

            {

                miPrimerSocket.Connect(miDireccion); // Conectamos     
                Console.WriteLine("Conectado con exito");
                             

                while (true)
                {
                    
                    Console.WriteLine(" *** Inicia el consumo **");

                    //ipstat = properties.GetIPv4GlobalStatistics();
                    long beginValue = IPGlobalProperties.GetIPGlobalProperties().GetIPv4GlobalStatistics().ReceivedPackets;
                    DateTime beginTime = DateTime.Now;
                    Console.WriteLine("Recibidos 1: " + beginValue + ' ' + beginTime);
                    // do something

                    Thread.Sleep(1000);
                    long endValue = IPGlobalProperties.GetIPGlobalProperties().GetIPv4GlobalStatistics().ReceivedPackets;
                    DateTime endTime = DateTime.Now;
                    Console.WriteLine("Recibidos 2: " + endValue + ' ' + endTime);


                    long recievedBytes = endValue - beginValue;
                    double totalSeconds = (endTime - beginTime).TotalSeconds;

                    var bytesPerSecond = recievedBytes / totalSeconds;
                    Console.WriteLine("BytePorSegundos: " + Decimal.Round(Convert.ToDecimal(bytesPerSecond),2));

                    Console.WriteLine(" *** Finaliza el consumo **");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    

                    byte[] infoEnviar = Encoding.Default.GetBytes(Decimal.Round(Convert.ToDecimal(bytesPerSecond), 2).ToString());
                    miPrimerSocket.Send(infoEnviar, 0, infoEnviar.Length, 0);
                  
                }


                miPrimerSocket.Close();

            }

            catch (Exception error)

            {

                Console.WriteLine("Error: {0}",error.ToString());

            }

            Console.WriteLine("Presione cualquier tecla para terminar");

            Console.ReadLine();

        }

    }
}
