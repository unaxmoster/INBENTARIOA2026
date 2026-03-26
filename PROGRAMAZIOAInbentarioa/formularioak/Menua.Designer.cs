namespace Inbentarioa.formularioak
{
    partial class Menua
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
            MINTEGIAK = new Button();
            EZABATUTAKOAK = new Button();
            GAILUAK = new Button();
            SuspendLayout();
            // 
            // ERABILTZAILEAK
            // 
            ERABILTZAILEAK.BackColor = Color.Navy;
            ERABILTZAILEAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ERABILTZAILEAK.ForeColor = Color.AliceBlue;
            ERABILTZAILEAK.Location = new Point(789, 450);
            ERABILTZAILEAK.Name = "ERABILTZAILEAK";
            ERABILTZAILEAK.Size = new Size(279, 130);
            ERABILTZAILEAK.TabIndex = 23;
            ERABILTZAILEAK.Text = "ERABILTZAILEAK";
            ERABILTZAILEAK.UseVisualStyleBackColor = false;
            // 
            // SARRERA
            // 
            SARRERA.AutoSize = true;
            SARRERA.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            SARRERA.Location = new Point(779, 313);
            SARRERA.Name = "SARRERA";
            SARRERA.Size = new Size(302, 81);
            SARRERA.TabIndex = 22;
            SARRERA.Text = "SARRERA";
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.Crimson;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            IRTEN.ForeColor = SystemColors.ButtonHighlight;
            IRTEN.Location = new Point(951, 605);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(250, 130);
            IRTEN.TabIndex = 21;
            IRTEN.Text = "IRTEN";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click;
            // 
            // MINTEGIAK
            // 
            MINTEGIAK.BackColor = Color.Navy;
            MINTEGIAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            MINTEGIAK.ForeColor = SystemColors.ButtonFace;
            MINTEGIAK.Location = new Point(1112, 450);
            MINTEGIAK.Name = "MINTEGIAK";
            MINTEGIAK.Size = new Size(250, 130);
            MINTEGIAK.TabIndex = 20;
            MINTEGIAK.Text = "MINTEGIAK";
            MINTEGIAK.UseVisualStyleBackColor = false;
            MINTEGIAK.Click += MINTEGIAK_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.BackColor = Color.Navy;
            EZABATUTAKOAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            EZABATUTAKOAK.ForeColor = SystemColors.ButtonHighlight;
            EZABATUTAKOAK.Location = new Point(658, 605);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(250, 130);
            EZABATUTAKOAK.TabIndex = 19;
            EZABATUTAKOAK.Text = "EZABATUTAKO GAILUAK";
            EZABATUTAKOAK.UseVisualStyleBackColor = false;
            EZABATUTAKOAK.Click += EZABATUTAKOAK_Click;
            // 
            // GAILUAK
            // 
            GAILUAK.BackColor = Color.Navy;
            GAILUAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            GAILUAK.ForeColor = Color.AliceBlue;
            GAILUAK.Location = new Point(495, 450);
            GAILUAK.Name = "GAILUAK";
            GAILUAK.Size = new Size(250, 130);
            GAILUAK.TabIndex = 18;
            GAILUAK.Text = "GAILUAK";
            GAILUAK.UseVisualStyleBackColor = false;
            // 
            // Menua
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1784, 1055);
            Controls.Add(ERABILTZAILEAK);
            Controls.Add(SARRERA);
            Controls.Add(IRTEN);
            Controls.Add(MINTEGIAK);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(GAILUAK);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Menua";
            Text = "Menua";
            Load += Menua_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ERABILTZAILEAK;
        private Label SARRERA;
        private Button IRTEN;
        private Button MINTEGIAK;
        private Button EZABATUTAKOAK;
        private Button GAILUAK;
    }
}