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
            ERABILTZAILEAK.BackColor = Color.LightGray;
            ERABILTZAILEAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ERABILTZAILEAK.Location = new Point(389, 192);
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
            SARRERA.Location = new Point(379, 55);
            SARRERA.Name = "SARRERA";
            SARRERA.Size = new Size(302, 81);
            SARRERA.TabIndex = 22;
            SARRERA.Text = "SARRERA";
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.LightGray;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            IRTEN.Location = new Point(551, 347);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(250, 130);
            IRTEN.TabIndex = 21;
            IRTEN.Text = "IRTEN";
            IRTEN.UseVisualStyleBackColor = false;
            // 
            // MINTEGIAK
            // 
            MINTEGIAK.BackColor = Color.LightGray;
            MINTEGIAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            MINTEGIAK.Location = new Point(712, 192);
            MINTEGIAK.Name = "MINTEGIAK";
            MINTEGIAK.Size = new Size(250, 130);
            MINTEGIAK.TabIndex = 20;
            MINTEGIAK.Text = "MINTEGIAK";
            MINTEGIAK.UseVisualStyleBackColor = false;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.BackColor = Color.LightGray;
            EZABATUTAKOAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            EZABATUTAKOAK.Location = new Point(258, 347);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(250, 130);
            EZABATUTAKOAK.TabIndex = 19;
            EZABATUTAKOAK.Text = "EZABATUTAKO GAILUAK";
            EZABATUTAKOAK.UseVisualStyleBackColor = false;
            // 
            // GAILUAK
            // 
            GAILUAK.BackColor = Color.LightGray;
            GAILUAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            GAILUAK.Location = new Point(95, 192);
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
            ClientSize = new Size(1057, 532);
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