using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace luuMail
{
    public partial class frmSendMail : Form
    {
        public frmSendMail()
        {
            InitializeComponent();
        }

        private void FrmSendMail_Load(object sender, EventArgs e)
        {
            txtYourMail.Text = "";
            txtToMail.Text = "";
            txtSuject.Text = "text smpt";
            ricContent.Text = "Lập trình mạng gữi mail 1 chiều!!!!!!!";
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            TcpClient zClient = new TcpClient();
            System.Text.StringBuilder zLog = new StringBuilder();
            string zData_Log;
            string zData_Send;
            string zData_Response = "";

            zData_Log = "Connect To" + "smtp.gmail.com" + ":" + "465";
            zClient.Connect("smtp.gmail.com", int.Parse("465"));
            zLog.AppendLine("Client -> :" + zData_Log);

            SslStream zSSL_Stream = new SslStream(zClient.GetStream());
            zSSL_Stream.AuthenticateAsClient("smtp.gmail.com");
            StreamWriter zWriter = new StreamWriter(zSSL_Stream);
            StreamReader zReader = new StreamReader(zSSL_Stream);

            zData_Response += zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);

            ///say
            zData_Send = "EHLO " + Dns.GetHostName();
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client -> :" + zData_Send);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);
            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);
            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);
            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);
            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);
            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);
            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <- :" + zData_Response);

            ///check
            zData_Send = "AUTH LOGIN";
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <-:" + zData_Response);

            byte[] zByte_Encoding = Encoding.UTF8.GetBytes("//");
            zData_Send = Convert.ToBase64String(zByte_Encoding);
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <-:" + zData_Response);

            zByte_Encoding = Encoding.UTF8.GetBytes("amxxuoqbxtfcpnok");
            zData_Send = Convert.ToBase64String(zByte_Encoding);
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <-:" + zData_Response);

            ///send mails
            zData_Send = "MAIL FROM: <" + txtYourMail.Text + ">";

            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);
            zLog.AppendLine("Server <-:" + zReader.ReadLine());

            zData_Send = "RCPT TO: <" + txtToMail.Text + ">";
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <-:" + zData_Response);

            zData_Send = "DATA";
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();

            zLog.AppendLine("Client ->:" + zData_Send);
            zLog.AppendLine("Server <-:" + zReader.ReadLine());
            zData_Send = "Subject: " + txtSuject.Text + "\r\n";
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);

            zData_Send = ricContent.Text;
            zData_Send += "\r\n" + ".";

            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zLog.AppendLine("Client ->:" + zData_Send);

            zData_Response = zReader.ReadLine();
            zLog.AppendLine("Server <-:" + zData_Response);

            ///quit
            zData_Send = "QUIT";
            zWriter.WriteLine(zData_Send);
            zWriter.Flush();
            zData_Response = zReader.ReadLine();

            zLog.AppendLine("Client ->:" + zData_Send);
            zLog.AppendLine("Server <-:" + zData_Response);


            zReader.Close();
            zWriter.Close();
            zClient.Close();
        }
    }
}
