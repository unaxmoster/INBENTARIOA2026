namespace Inbentarioa.formularioak
{
    partial class Mintegiak
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
            ERABILTZAILEAK = new Button();
            SARRERA = new Label();
            IRTEN = new Button();
            EZABATUTAKOAK = new Button();
            GAILUAK = new Button();
            SuspendLayout();
            // 
            // ERABILTZAILEAK
            // 
            ERABILTZAILEAK.BackColor = Color.Navy;
            ERABILTZAILEAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ERABILTZAILEAK.ForeColor = Color.AliceBlue;
            ERABILTZAILEAK.Location = new Point(1056, 567);
            ERABILTZAILEAK.Name = "ERABILTZAILEAK";
            ERABILTZAILEAK.Size = new Size(291, 130);
            ERABILTZAILEAK.TabIndex = 29;
            ERABILTZAILEAK.Text = "EZABATU";
            ERABILTZAILEAK.UseVisualStyleBackColor = false;
            // 
            // SARRERA
            // 
            SARRERA.AutoSize = true;
            SARRERA.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            SARRERA.Location = new Point(869, 297);
            SARRERA.Name = "SARRERA";
            SARRERA.Size = new Size(368, 81);
            SARRERA.TabIndex = 28;
            SARRERA.Text = "MINTEGIAK";
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.Crimson;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            IRTEN.ForeColor = SystemColors.ButtonHighlight;
            IRTEN.Location = new Point(1056, 423);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(291, 130);
            IRTEN.TabIndex = 27;
            IRTEN.Text = "ATZERA";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.BackColor = Color.Navy;
            EZABATUTAKOAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            EZABATUTAKOAK.ForeColor = SystemColors.ButtonHighlight;
            EZABATUTAKOAK.Location = new Point(748, 567);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(250, 130);
            EZABATUTAKOAK.TabIndex = 25;
            EZABATUTAKOAK.Text = "GEHITU";
            EZABATUTAKOAK.UseVisualStyleBackColor = false;
            EZABATUTAKOAK.Click += EZABATUTAKOAK_Click;
            // 
            // GAILUAK
            // 
            GAILUAK.BackColor = Color.Navy;
            GAILUAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            GAILUAK.ForeColor = Color.AliceBlue;
            GAILUAK.Location = new Point(748, 423);
            GAILUAK.Name = "GAILUAK";
            GAILUAK.Size = new Size(250, 130);
            GAILUAK.TabIndex = 24;
            GAILUAK.Text = "IKUSI";
            GAILUAK.UseVisualStyleBackColor = false;
            GAILUAK.Click += GAILUAK_Click;
            // 
            // Mintegiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1526, 843);
            Controls.Add(ERABILTZAILEAK);
            Controls.Add(SARRERA);
            Controls.Add(IRTEN);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(GAILUAK);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Mintegiak";
            Text = "Mintegiak";
            Load += Mintegiak_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ERABILTZAILEAK;
        private Label SARRERA;
        private Button IRTEN;
        private Button EZABATUTAKOAK;
        private Button GAILUAK;
    }
}