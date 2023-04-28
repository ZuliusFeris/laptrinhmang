using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPListener
{
    class Program
    {
        static void Main(string[] args)
        {
            string zServer_Name = Dns.GetHostName();
            IPAddress[] zServer_IPs = Dns.GetHostByName(zServer_Name).AddressList;
            int zPort = 9000;

            Console.WriteLine("The server running at ");
            Console.WriteLine("    Server name: " + zServer_Name);
            Console.WriteLine("    Ip         : " + zServer_IPs[0].ToString());
            Console.WriteLine("    Port       : " + zPort);

            IPAddress zIP = zServer_IPs[0];
            TcpListener zTCP_Server = new TcpListener(zIP, zPort);

            Console.WriteLine("Listening for client request ...");
            zTCP_Server.Start();

            string zData_Send, zData_Received;
            string zClientConnected;

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for a connection .... ");
                    TcpClient zSocketServer = zTCP_Server.AcceptTcpClient();
                    zClientConnected = zSocketServer.Client.RemoteEndPoint.ToString();
                    Console.WriteLine("Accept a connection from: " + zClientConnected);

                    /// Send and receive

                    Console.WriteLine("=========================");
                    NetworkStream zNetStream = zSocketServer.GetStream();
                    StreamWriter zStreamSend = new StreamWriter(zNetStream);
                    StreamReader zStreamReceive = new StreamReader(zNetStream);
                    while(true)
                    {
                        try
                        {
                            Console.Write("Server ->: ");
                            zData_Send = Console.ReadLine();
                            zStreamSend.WriteLine(zData_Send);
                            zStreamSend.Flush();

                            zData_Received = zStreamReceive.ReadLine();
                            Console.WriteLine("Client <- : " + zData_Received);

                            //string folderPath = @"C:\Screenshots";
                            //Directory.CreateDirectory(folderPath);
                            //for (int i = 0; i < 3; i++)
                            //{
                            //    string filePath = Path.Combine(folderPath, "screenshot" + i + ".jpg");
                            //    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            //    {
                            //        int bytesReceived = 0;
                            //        byte[] data = new byte[1024];
                            //        while ((bytesReceived = zNetStream.Read(data, 0, data.Length)) > 0)
                            //        {
                            //            fs.Write(data, 0, bytesReceived);
                            //        }
                            //    }
                            //    Console.WriteLine("Received and saved screenshot " + i);
                            //}

                            if (zData_Received.ToUpper() == "GOODBYE")
                            {
                                break;
                            }
                            else if(zData_Received is string)
                            {
                                zData_Received = zStreamReceive.ReadLine();
                                string[] receivedArray = zData_Received.Split(';');
                                for(int i=0;i<receivedArray.Length;i++)
                                {
                                    Console.WriteLine(receivedArray[i]);
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            break;
                        }
                    }
                    zSocketServer.Close();
                }
                catch(SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                    Console.WriteLine("Stops listening foe new client.");
                    zTCP_Server.Stop();
                    break;
                }
            }
            Console.WriteLine("Close App");
            Console.ReadKey();
        }
    }
}
