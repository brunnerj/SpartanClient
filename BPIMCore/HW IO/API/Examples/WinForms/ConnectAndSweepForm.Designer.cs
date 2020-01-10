namespace BPIM_API_Use___WinForms_Examples
{
    partial class ConnectAndSweepForm
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
            this.apiToken = new System.Windows.Forms.TextBox();
            this.hostUrl = new System.Windows.Forms.TextBox();
            this.hostPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.getSweep = new System.Windows.Forms.Button();
            this.responseText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // apiToken
            // 
            this.apiToken.Enabled = false;
            this.apiToken.Location = new System.Drawing.Point(12, 67);
            this.apiToken.Multiline = true;
            this.apiToken.Name = "apiToken";
            this.apiToken.Size = new System.Drawing.Size(199, 198);
            this.apiToken.TabIndex = 0;
            this.apiToken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hostUrl
            // 
            this.hostUrl.Location = new System.Drawing.Point(12, 12);
            this.hostUrl.Name = "hostUrl";
            this.hostUrl.Size = new System.Drawing.Size(146, 20);
            this.hostUrl.TabIndex = 0;
            this.hostUrl.Text = "localhost";
            // 
            // hostPort
            // 
            this.hostPort.Location = new System.Drawing.Point(164, 12);
            this.hostPort.Name = "hostPort";
            this.hostPort.Size = new System.Drawing.Size(47, 20);
            this.hostPort.TabIndex = 0;
            this.hostPort.Text = "80";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 38);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(199, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // getSweep
            // 
            this.getSweep.Enabled = false;
            this.getSweep.Location = new System.Drawing.Point(217, 271);
            this.getSweep.Name = "getSweep";
            this.getSweep.Size = new System.Drawing.Size(101, 23);
            this.getSweep.TabIndex = 1;
            this.getSweep.Text = "Get Sweep Data";
            this.getSweep.UseVisualStyleBackColor = true;
            this.getSweep.Click += new System.EventHandler(this.getSweep_Click);
            // 
            // responseText
            // 
            this.responseText.Enabled = false;
            this.responseText.Location = new System.Drawing.Point(217, 12);
            this.responseText.Multiline = true;
            this.responseText.Name = "responseText";
            this.responseText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.responseText.Size = new System.Drawing.Size(429, 253);
            this.responseText.TabIndex = 2;
            // 
            // ConnectAndSweepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 302);
            this.Controls.Add(this.responseText);
            this.Controls.Add(this.getSweep);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.hostPort);
            this.Controls.Add(this.hostUrl);
            this.Controls.Add(this.apiToken);
            this.Name = "ConnectAndSweepForm";
            this.Text = "BPIM Connect & Sweep";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox apiToken;
        private System.Windows.Forms.TextBox hostUrl;
        private System.Windows.Forms.TextBox hostPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button getSweep;
        private System.Windows.Forms.TextBox responseText;
    }
}

