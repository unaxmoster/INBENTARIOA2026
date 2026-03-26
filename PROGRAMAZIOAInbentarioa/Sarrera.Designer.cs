namespace Inbentarioa
{
    partial class Sarrera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sarrera));
            lblizena = new Label();
            lblpass = new Label();
            SartuBotoia = new Button();
            textpass = new TextBox();
            textizena = new TextBox();
            SuspendLayout();
            // 
            // lblizena
            // 
            lblizena.AutoSize = true;
            lblizena.BackColor = Color.Transparent;
            lblizena.Font = new Font("Arial Black", 48F, FontStyle.Bold, GraphicsUnit.Point);
            lblizena.ForeColor = Color.Navy;
            lblizena.Location = new Point(728, 334);
            lblizena.Name = "lblizena";
            lblizena.Size = new Size(308, 113);
            lblizena.TabIndex = 0;
            lblizena.Text = "Izena:";
            lblizena.Click += label1_Click;
            // 
            // lblpass
            // 
            lblpass.AutoSize = true;
            lblpass.BackColor = Color.Transparent;
            lblpass.Font = new Font("Arial Black", 48F, FontStyle.Bold, GraphicsUnit.Point);
            lblpass.ForeColor = Color.Navy;
            lblpass.Location = new Point(536, 437);
            lblpass.Name = "lblpass";
            lblpass.Size = new Size(500, 113);
            lblpass.TabIndex = 1;
            lblpass.Text = "Pasahitza:";
            // 
            // SartuBotoia
            // 
            SartuBotoia.BackColor = Color.Navy;
            SartuBotoia.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point);
            SartuBotoia.ForeColor = SystemColors.ButtonHighlight;
            SartuBotoia.Location = new Point(1042, 577);
            SartuBotoia.Name = "SartuBotoia";
            SartuBotoia.Size = new Size(255, 88);
            SartuBotoia.TabIndex = 2;
            SartuBotoia.Text = "SARTU";
            SartuBotoia.UseVisualStyleBackColor = false;
            SartuBotoia.Click += button1_Click;
            // 
            // textpass
            // 
            textpass.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point);
            textpass.Location = new Point(1042, 472);
            textpass.Name = "textpass";
            textpass.PasswordChar = '*';
            textpass.Size = new Size(255, 70);
            textpass.TabIndex = 4;
            // 
            // textizena
            // 
            textizena.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point);
            textizena.Location = new Point(1042, 369);
            textizena.Name = "textizena";
            textizena.Size = new Size(255, 70);
            textizena.TabIndex = 5;
            // 
            // Sarrera
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1751, 896);
            Controls.Add(textizena);
            Controls.Add(textpass);
            Controls.Add(SartuBotoia);
            Controls.Add(lblpass);
            Controls.Add(lblizena);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Sarrera";
            Text = "Hasiera";
            Load += Sarrera_kargatu;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblizena;
        private Label lblpass;
        private Button SartuBotoia;
        private TextBox textpass;
        private TextBox textizena;
    }
}
