using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TCPListenner
{
    class Program
    {
        static void Main(string[] args)
        {

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
                        while (true)
                        {
                            try
                            {
                                Console.Write("Server ->: ");
                                zData_Send = Console.ReadLine();
                                zStreamSend.WriteLine(zData_Send);
                                zStreamSend.Flush();

                                zData_Received = zStreamReceive.ReadLine();
                                Console.WriteLine("Client <- : " + zData_Received);
                                
                               

                                if (zData_Received.ToUpper() == "GOODBYE")
                                {
                                    break;
                                }
                                else if (zData_Received is string && zData_Received.Length>4)
                                {
                                    string[] receivedArray = zData_Received.Split(';');
                                    for (int i = 0; i < receivedArray.Length; i++)
                                    {
                                        Console.WriteLine(receivedArray[i]);
                                    }
                                }
                                else if (zData_Send.ToUpper() == "ANH")
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        // Receive the screenshot from the client
                                        string base64String = zStreamReceive.ReadLine();

                                        // Convert the Base64 string to byte array
                                        byte[] imgBytes = Convert.FromBase64String(base64String);

                                        // Create a new Bitmap from the byte array
                                        using (MemoryStream ms = new MemoryStream(imgBytes))
                                        {
                                            Bitmap bitmap = new Bitmap(ms);

                                            // Save the Bitmap to a file
                                            string filePath = @"C:\Screenshots\screenshot" + i + ".jpg";
                                            bitmap.Save(filePath, ImageFormat.Jpeg);
                                        }

                                        Console.WriteLine("Received and saved screenshot " + i);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                break;
                            }
                        }
                        zSocketServer.Close();
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("SocketException: {0}", e);
                        Console.WriteLine("Stops listening for new client.");
                        zTCP_Server.Stop();
                        break;
                    }
                }
                Console.WriteLine("Close App");
                Console.ReadKey();
            }
        }
    }
}
