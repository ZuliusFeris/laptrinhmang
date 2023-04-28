using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;

namespace luuMail
{
    public partial class frmSystemMail : Form
    {
        UserCredential credential;
        GmailService service;
        public frmSystemMail()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            frmSendMail sendmail = new frmSendMail();
            sendmail.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // Xác thực người dùng
            using (var stream = new FileStream("######", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { GmailService.Scope.GmailReadonly },
                    "user",
                    CancellationToken.None).Result;
            }

            // Khởi tạo service
            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Gmail API Example"
            });

            MessageBox.Show("Đăng nhập thành công");

            hienthithongtinmail();
        }

        private void hienthithongtinmail()
        {
            // Truy vấn danh sách mail
            var request = service.Users.Messages.List("me");
            var response = request.Execute();
            var messages = response.Messages;

            // Tạo DataTable và thêm các dòng thông tin mail vào
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("From");
            dt.Columns.Add("Subject");

            foreach (var message in messages)
            {
                var msg = service.Users.Messages.Get("me", message.Id).Execute();
                var headers = msg.Payload.Headers;

                string id = message.Id;
                string from = "";
                string subject = "";

                foreach (var header in headers)
                {
                    if (header.Name == "From")
                        from = header.Value;
                    else if (header.Name == "Subject")
                        subject = header.Value;
                }

                dt.Rows.Add(id, from, subject);
            }

            // Hiển thị DataTable lên DataGridView
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["ID"].Visible = false;
        }
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Lấy ID của email được chọn
                string emailId = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                // Truy xuất thông tin về email
                var email = await service.Users.Messages.Get("me", emailId).ExecuteAsync();

                // Lấy nội dung HTML của email
                string htmlBody = "";
                if (email.Payload.Parts != null)
                {
                    foreach (var part in email.Payload.Parts)
                    {
                        if (part.MimeType == "text/html")
                        {
                            byte[] data = Convert.FromBase64String(part.Body.Data.Replace('-', '+').Replace('_', '/'));
                            htmlBody = Encoding.UTF8.GetString(data);
                            break;
                        }
                    }
                }

                // Tạo file HTML với nội dung email
                string html = "<html><head><style>" +
                              "body { font-family: Arial; font-size: 14px; }" +
                              "h2 { margin: 20px 0 5px 0; font-size: 16px; }" +
                              "p { margin: 5px 0; }" +
                              "</style></head><body>" +
                              "<h2>" + email.Payload.Headers.FirstOrDefault(x => x.Name == "From")?.Value + "</h2>" +
                              "<h2>" + email.Payload.Headers.FirstOrDefault(x => x.Name == "Subject")?.Value + "</h2>" +
                              htmlBody +
                              "</body></html>";

                // Hiển thị file HTML trên WebBrowser control
                string path = Path.Combine(Path.GetTempPath(), "email.html");
                File.WriteAllText(path, html);
                webBrowser1.Navigate(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FrmSystemMail_Load(object sender, EventArgs e)
        {
            txtTK.Text = "";
            txtMK.Text = "";
        }
    }
}
