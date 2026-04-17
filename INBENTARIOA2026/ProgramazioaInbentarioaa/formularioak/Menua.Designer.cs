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
            btnErabiltzaileak = new Button();
            SARRERA = new Label();
            IRTEN = new Button();
            btnMintegiak = new Button();
            btnEzabatuak = new Button();
            btnGailuak = new Button();
            imageList2 = new ImageList(components);
            btnHondatuak = new Button();
            SuspendLayout();
            // 
            // btnErabiltzaileak
            // 
            btnErabiltzaileak.BackColor = Color.Navy;
            btnErabiltzaileak.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnErabiltzaileak.ForeColor = Color.AliceBlue;
            btnErabiltzaileak.Location = new Point(833, 433);
            btnErabiltzaileak.Name = "btnErabiltzaileak";
            btnErabiltzaileak.Size = new Size(351, 130);
            btnErabiltzaileak.TabIndex = 29;
            btnErabiltzaileak.Text = "ERABILTZAILEAK";
            btnErabiltzaileak.UseVisualStyleBackColor = false;
            btnErabiltzaileak.Click += ERABILTZAILEAK_Click_1;
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
            IRTEN.Location = new Point(1208, 588);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(276, 172);
            IRTEN.TabIndex = 27;
            IRTEN.Text = "IRTEN";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click_1;
            // 
            // btnMintegiak
            // 
            btnMintegiak.BackColor = Color.Navy;
            btnMintegiak.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnMintegiak.ForeColor = SystemColors.ButtonFace;
            btnMintegiak.Location = new Point(1208, 433);
            btnMintegiak.Name = "btnMintegiak";
            btnMintegiak.Size = new Size(281, 130);
            btnMintegiak.TabIndex = 26;
            btnMintegiak.Text = "MINTEGIAK";
            btnMintegiak.UseVisualStyleBackColor = false;
            btnMintegiak.Click += MINTEGIAK_Click_1;
            // 
            // btnEzabatuak
            // 
            btnEzabatuak.BackColor = Color.Navy;
            btnEzabatuak.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEzabatuak.ForeColor = SystemColors.ButtonHighlight;
            btnEzabatuak.Location = new Point(464, 588);
            btnEzabatuak.Name = "btnEzabatuak";
            btnEzabatuak.Size = new Size(346, 172);
            btnEzabatuak.TabIndex = 25;
            btnEzabatuak.Text = "EZABATUTAKO GAILUAK";
            btnEzabatuak.UseVisualStyleBackColor = false;
            btnEzabatuak.Click += EZABATUTAKOAK_Click_1;
            // 
            // btnGailuak
            // 
            btnGailuak.BackColor = Color.Navy;
            btnGailuak.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnGailuak.ForeColor = Color.AliceBlue;
            btnGailuak.Location = new Point(464, 433);
            btnGailuak.Name = "btnGailuak";
            btnGailuak.Size = new Size(346, 130);
            btnGailuak.TabIndex = 24;
            btnGailuak.Text = "GAILUAK";
            btnGailuak.UseVisualStyleBackColor = false;
            btnGailuak.Click += GAILUAK_Click_1;
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
            imageList2.Images.SetKeyName(4, "5.png");
            // 
            // btnHondatuak
            // 
            btnHondatuak.BackColor = Color.Navy;
            btnHondatuak.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnHondatuak.ForeColor = Color.Transparent;
            btnHondatuak.Location = new Point(833, 588);
            btnHondatuak.Name = "btnHondatuak";
            btnHondatuak.Size = new Size(361, 172);
            btnHondatuak.TabIndex = 30;
            btnHondatuak.Text = "HONDATUTAKO GAILUAK";
            btnHondatuak.UseVisualStyleBackColor = false;
            btnHondatuak.Click += btnHondatuak_Click;
            // 
            // Menua
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1784, 1055);
            Controls.Add(btnHondatuak);
            Controls.Add(btnErabiltzaileak);
            Controls.Add(SARRERA);
            Controls.Add(IRTEN);
            Controls.Add(btnMintegiak);
            Controls.Add(btnEzabatuak);
            Controls.Add(btnGailuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Menua";
            Text = "Menua";
            Load += Menua_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnErabiltzaileak;
        private Label SARRERA;
        private Button IRTEN;
        private Button btnMintegiak;
        private Button btnEzabatuak;
        private Button btnGailuak;
        private ImageList imageList2;
        private Button btnHondatuak;
    }
}