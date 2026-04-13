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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menua));
            ERABILTZAILEAK = new Button();
            SARRERA = new Label();
            IRTEN = new Button();
            MINTEGIAK = new Button();
            EZABATUTAKOAK = new Button();
            GAILUAK = new Button();
            imageList2 = new ImageList(components);
            SuspendLayout();
            // 
            // ERABILTZAILEAK
            // 
            ERABILTZAILEAK.BackColor = Color.Navy;
            ERABILTZAILEAK.Font = new Font("Arial", 18F, FontStyle.Bold);
            ERABILTZAILEAK.ForeColor = Color.AliceBlue;
            ERABILTZAILEAK.Location = new Point(760, 433);
            ERABILTZAILEAK.Name = "ERABILTZAILEAK";
            ERABILTZAILEAK.Size = new Size(392, 130);
            ERABILTZAILEAK.TabIndex = 29;
            ERABILTZAILEAK.Text = "ERABILTZAILEAK";
            ERABILTZAILEAK.UseVisualStyleBackColor = false;
            ERABILTZAILEAK.Click += ERABILTZAILEAK_Click_1;
            // 
            // SARRERA
            // 
            SARRERA.AutoSize = true;
            SARRERA.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            SARRERA.Location = new Point(808, 291);
            SARRERA.Name = "SARRERA";
            SARRERA.Size = new Size(302, 81);
            SARRERA.TabIndex = 28;
            SARRERA.Text = "SARRERA";
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.Crimson;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold);
            IRTEN.ForeColor = SystemColors.ButtonHighlight;
            IRTEN.Location = new Point(1014, 588);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(250, 130);
            IRTEN.TabIndex = 27;
            IRTEN.Text = "IRTEN";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click_1;
            // 
            // MINTEGIAK
            // 
            MINTEGIAK.BackColor = Color.Navy;
            MINTEGIAK.Font = new Font("Arial", 18F, FontStyle.Bold);
            MINTEGIAK.ForeColor = SystemColors.ButtonFace;
            MINTEGIAK.Location = new Point(1180, 433);
            MINTEGIAK.Name = "MINTEGIAK";
            MINTEGIAK.Size = new Size(281, 130);
            MINTEGIAK.TabIndex = 26;
            MINTEGIAK.Text = "MINTEGIAK";
            MINTEGIAK.UseVisualStyleBackColor = false;
            MINTEGIAK.Click += MINTEGIAK_Click_1;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.BackColor = Color.Navy;
            EZABATUTAKOAK.Font = new Font("Arial", 18F, FontStyle.Bold);
            EZABATUTAKOAK.ForeColor = SystemColors.ButtonHighlight;
            EZABATUTAKOAK.Location = new Point(627, 588);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(346, 130);
            EZABATUTAKOAK.TabIndex = 25;
            EZABATUTAKOAK.Text = "EZABATUTAKO GAILUAK";
            EZABATUTAKOAK.UseVisualStyleBackColor = false;
            EZABATUTAKOAK.Click += EZABATUTAKOAK_Click_1;
            // 
            // GAILUAK
            // 
            GAILUAK.BackColor = Color.Navy;
            GAILUAK.Font = new Font("Arial", 18F, FontStyle.Bold);
            GAILUAK.ForeColor = Color.AliceBlue;
            GAILUAK.Location = new Point(474, 433);
            GAILUAK.Name = "GAILUAK";
            GAILUAK.Size = new Size(264, 130);
            GAILUAK.TabIndex = 24;
            GAILUAK.Text = "GAILUAK";
            GAILUAK.UseVisualStyleBackColor = false;
            GAILUAK.Click += GAILUAK_Click_1;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth32Bit;
            imageList2.ImageStream = (ImageListStreamer)resources.GetObject("imageList2.ImageStream");
            imageList2.TransparentColor = Color.Transparent;
            imageList2.Images.SetKeyName(0, "1.png");
            imageList2.Images.SetKeyName(1, "2.png");
            imageList2.Images.SetKeyName(2, "3.png");
            imageList2.Images.SetKeyName(3, "4.png");
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
        private ImageList imageList2;
    }
}