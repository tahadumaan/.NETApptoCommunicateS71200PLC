
namespace MagnaUruguay
{
    partial class km1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(km1));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnectPlc = new System.Windows.Forms.Button();
            this.pnlPlcConnectionStatus = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBoxStation1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBoxStation2 = new System.Windows.Forms.TextBox();
            this.timerSync = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBoxLog = new System.Windows.Forms.TextBox();
            this.timerConn = new System.Windows.Forms.Timer(this.components);
            this.timerTryConn = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(35, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "PLC Connection";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnectPlc);
            this.groupBox1.Controls.Add(this.pnlPlcConnectionStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // btnConnectPlc
            // 
            this.btnConnectPlc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConnectPlc.Location = new System.Drawing.Point(188, 29);
            this.btnConnectPlc.Name = "btnConnectPlc";
            this.btnConnectPlc.Size = new System.Drawing.Size(123, 25);
            this.btnConnectPlc.TabIndex = 2;
            this.btnConnectPlc.Text = "Connect to PLC";
            this.btnConnectPlc.UseVisualStyleBackColor = true;
            this.btnConnectPlc.Visible = false;
            this.btnConnectPlc.Click += new System.EventHandler(this.btnConnectPlc_Click);
            // 
            // pnlPlcConnectionStatus
            // 
            this.pnlPlcConnectionStatus.Location = new System.Drawing.Point(11, 33);
            this.pnlPlcConnectionStatus.Name = "pnlPlcConnectionStatus";
            this.pnlPlcConnectionStatus.Size = new System.Drawing.Size(18, 17);
            this.pnlPlcConnectionStatus.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::MagnaUruguay.Properties.Resources.anıt_logo;
            this.pictureBox1.Location = new System.Drawing.Point(571, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDate.Location = new System.Drawing.Point(386, 24);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(90, 25);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "3.05.2021";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTime.Location = new System.Drawing.Point(387, 49);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(56, 25);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "22:01";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBoxStation1);
            this.groupBox2.Location = new System.Drawing.Point(12, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 283);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Statiton 1";
            // 
            // txtBoxStation1
            // 
            this.txtBoxStation1.Location = new System.Drawing.Point(11, 22);
            this.txtBoxStation1.Multiline = true;
            this.txtBoxStation1.Name = "txtBoxStation1";
            this.txtBoxStation1.ReadOnly = true;
            this.txtBoxStation1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxStation1.Size = new System.Drawing.Size(309, 255);
            this.txtBoxStation1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxStation2);
            this.groupBox3.Location = new System.Drawing.Point(375, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(339, 283);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Station 2";
            // 
            // txtBoxStation2
            // 
            this.txtBoxStation2.Location = new System.Drawing.Point(20, 22);
            this.txtBoxStation2.Multiline = true;
            this.txtBoxStation2.Name = "txtBoxStation2";
            this.txtBoxStation2.ReadOnly = true;
            this.txtBoxStation2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxStation2.Size = new System.Drawing.Size(310, 255);
            this.txtBoxStation2.TabIndex = 1;
            // 
            // timerSync
            // 
            this.timerSync.Interval = 1000;
            this.timerSync.Tick += new System.EventHandler(this.timerSync_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBoxLog);
            this.groupBox4.Location = new System.Drawing.Point(12, 383);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(702, 202);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Logs";
            // 
            // txtBoxLog
            // 
            this.txtBoxLog.Location = new System.Drawing.Point(11, 22);
            this.txtBoxLog.Multiline = true;
            this.txtBoxLog.Name = "txtBoxLog";
            this.txtBoxLog.ReadOnly = true;
            this.txtBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxLog.Size = new System.Drawing.Size(673, 170);
            this.txtBoxLog.TabIndex = 2;
            // 
            // timerConn
            // 
            this.timerConn.Interval = 1000;
            this.timerConn.Tick += new System.EventHandler(this.timerConn_Tick);
            // 
            // timerTryConn
            // 
            this.timerTryConn.Interval = 3000;
            this.timerTryConn.Tick += new System.EventHandler(this.timerTryConn_Tick);
            // 
            // km1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 591);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "km1";
            this.Text = "Uruguay Paketleme Arşiv";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.km1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnectPlc;
        private System.Windows.Forms.Panel pnlPlcConnectionStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBoxStation1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBoxStation2;
        private System.Windows.Forms.Timer timerSync;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtBoxLog;
        private System.Windows.Forms.Timer timerConn;
        private System.Windows.Forms.Timer timerTryConn;
    }
}

