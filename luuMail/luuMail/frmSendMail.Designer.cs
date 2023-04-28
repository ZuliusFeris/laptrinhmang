namespace luuMail
{
    partial class frmSendMail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSuject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtToMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtYourMail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ricContent = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSuject);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtToMail);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.txtYourMail);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 339);
            this.panel1.TabIndex = 0;
            // 
            // txtSuject
            // 
            this.txtSuject.Location = new System.Drawing.Point(60, 101);
            this.txtSuject.Name = "txtSuject";
            this.txtSuject.Size = new System.Drawing.Size(194, 20);
            this.txtSuject.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Suject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Thông tin người nhận";
            // 
            // txtToMail
            // 
            this.txtToMail.Location = new System.Drawing.Point(60, 75);
            this.txtToMail.Name = "txtToMail";
            this.txtToMail.Size = new System.Drawing.Size(194, 20);
            this.txtToMail.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To:";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(179, 148);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // txtYourMail
            // 
            this.txtYourMail.Location = new System.Drawing.Point(60, 21);
            this.txtYourMail.Name = "txtYourMail";
            this.txtYourMail.Size = new System.Drawing.Size(194, 20);
            this.txtYourMail.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your Mail";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ricContent);
            this.panel2.Location = new System.Drawing.Point(284, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 339);
            this.panel2.TabIndex = 1;
            // 
            // ricContent
            // 
            this.ricContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ricContent.Location = new System.Drawing.Point(0, 0);
            this.ricContent.Name = "ricContent";
            this.ricContent.Size = new System.Drawing.Size(473, 339);
            this.ricContent.TabIndex = 0;
            this.ricContent.Text = "";
            // 
            // frmSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 363);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmSendMail";
            this.Text = "frmSendMail";
            this.Load += new System.EventHandler(this.FrmSendMail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSuject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtToMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtYourMail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox ricContent;
    }
}