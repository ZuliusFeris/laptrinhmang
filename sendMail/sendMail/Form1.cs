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

namespace sendMail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            txtlog.Text += ">>>>>>>>>>>>>>>>>>>[ SMPT protocol send mail ]>>>>>>>>>>>\r\n";
            TcpClient zclient = new TcpClient();
            System.Text.StringBuilder zLog = new StringBuilder();
            string zdata_log;
            string zdata_send;
            string zdata_respone="";

            //Connect
            zdata_log = "Connect to " + txtSMTPadd.Text + " : " + txtSMTPport.Text;
            zclient.Connect(txtSMTPadd.Text, int.Parse(txtSMTPport.Text));
            zLog.AppendLine("Client->: " + zdata_log);

            SslStream sslStream = new SslStream(zclient.GetStream());
            sslStream.AuthenticateAsClient(txtSMTPadd.Text);
            StreamWriter zWriter = new StreamWriter(sslStream);
            StreamReader zReader = new StreamReader(sslStream);

            zdata_respone += zReader.ReadLine();
            zLog.AppendLine("Server<-: " + zdata_respone);

            // say hello 
            zdata_send = "EHLO " + Dns.GetHostName();
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);
            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);
            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);
            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);
            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);
            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);
            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Servver<-: " + zdata_respone);

            // check authenticity

            zdata_send = "AUTH LOGIN";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Server<-: " + zdata_respone);

            byte[] zByte = Encoding.UTF8.GetBytes(txtuser.Text);
            zdata_send = Convert.ToBase64String(zByte);
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Server<-: " + zdata_respone);

            zByte = Encoding.UTF8.GetBytes(txtpass.Text);
            zdata_send = Convert.ToBase64String(zByte);
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Server<-: " + zdata_respone);

            // Send mail
            zdata_send = "MAIL FROM: <" + txtYourmail.Text + ">";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);
            zLog.AppendLine("Server<-: " + zReader.ReadLine());

            zdata_send = "RCPT TO: <" + txttoemail.Text + ">";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Server<-: " + zdata_respone);

            zdata_send = "DATA";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();

            zLog.AppendLine("Client->: " + zdata_send);
            zLog.AppendLine("Server<-: " + zReader.ReadLine());

            zdata_send = "SUBJECT: " + txtsuject.Text + "\r\n";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_send = txtcontent.Text;
            zdata_send += "\r\n" + ".";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zLog.AppendLine("Client->: " + zdata_send);

            zdata_respone = zReader.ReadLine();
            zLog.AppendLine("Server<-: " + zdata_respone);

            //close connect
            zdata_send = "QUIT";
            zWriter.WriteLine(zdata_send);
            zWriter.Flush();
            zdata_respone = zReader.ReadLine();

            zLog.AppendLine("Client->: " + zdata_send);
            zLog.AppendLine("Server<-: " + zdata_respone);

            txtlog.Text += zLog.ToString();

            zReader.Close();
            zWriter.Close();
            zclient.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtpass.Text = "wmffyeatvviaypkw";
            txtSMTPadd.Text = "smtp.gmail.com";
            txtSMTPport.Text = "465";
            txtYourmail.Text = "";
            txtsuject.Text = "smtpobj";
            txtcontent.Text = "as;ldfhkashdf;lj";
            txttoemail.Text = "";
            txtuser.Text = "";
        }
    }
}
