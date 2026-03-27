namespace Inbentarioa.formularioak
{
    partial class IKUSI
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
            MINTEGI = new Button();
            ORDENAGAILUA = new Button();
            INPRIMAGAILUA = new Button();
            BESTEAK = new Button();
            GUZTIAK = new Button();
            ATZERA = new Button();
            EZABATUTAKOAK = new Label();
            SuspendLayout();
            // 
            // MINTEGI
            // 
            MINTEGI.BackColor = Color.Navy;
            MINTEGI.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            MINTEGI.ForeColor = Color.Transparent;
            MINTEGI.Location = new Point(500, 338);
            MINTEGI.Name = "MINTEGI";
            MINTEGI.Size = new Size(320, 172);
            MINTEGI.TabIndex = 0;
            MINTEGI.Text = "MINTEGIKA IKUSI";
            MINTEGI.UseVisualStyleBackColor = false;
            // 
            // ORDENAGAILUA
            // 
            ORDENAGAILUA.BackColor = Color.Navy;
            ORDENAGAILUA.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ORDENAGAILUA.ForeColor = Color.Transparent;
            ORDENAGAILUA.Location = new Point(832, 338);
            ORDENAGAILUA.Name = "ORDENAGAILUA";
            ORDENAGAILUA.Size = new Size(332, 172);
            ORDENAGAILUA.TabIndex = 1;
            ORDENAGAILUA.Text = "ORDENAGAILUA";
            ORDENAGAILUA.UseVisualStyleBackColor = false;
            // 
            // INPRIMAGAILUA
            // 
            INPRIMAGAILUA.BackColor = Color.Navy;
            INPRIMAGAILUA.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            INPRIMAGAILUA.ForeColor = Color.Transparent;
            INPRIMAGAILUA.Location = new Point(500, 529);
            INPRIMAGAILUA.Name = "INPRIMAGAILUA";
            INPRIMAGAILUA.Size = new Size(320, 172);
            INPRIMAGAILUA.TabIndex = 2;
            INPRIMAGAILUA.Text = "INPRIMAGAILUA";
            INPRIMAGAILUA.UseVisualStyleBackColor = false;
            // 
            // BESTEAK
            // 
            BESTEAK.BackColor = Color.Navy;
            BESTEAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            BESTEAK.ForeColor = Color.Transparent;
            BESTEAK.Location = new Point(832, 529);
            BESTEAK.Name = "BESTEAK";
            BESTEAK.Size = new Size(332, 172);
            BESTEAK.TabIndex = 3;
            BESTEAK.Text = "BESTE GAILUAK";
            BESTEAK.UseVisualStyleBackColor = false;
            // 
            // GUZTIAK
            // 
            GUZTIAK.BackColor = Color.Navy;
            GUZTIAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            GUZTIAK.ForeColor = Color.Transparent;
            GUZTIAK.Location = new Point(1180, 338);
            GUZTIAK.Name = "GUZTIAK";
            GUZTIAK.Size = new Size(320, 172);
            GUZTIAK.TabIndex = 4;
            GUZTIAK.Text = "GUZTIAK";
            GUZTIAK.UseVisualStyleBackColor = false;
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(1180, 529);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 5;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point);
            EZABATUTAKOAK.Location = new Point(765, 230);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(463, 81);
            EZABATUTAKOAK.TabIndex = 24;
            EZABATUTAKOAK.Text = "GAILUAK IKUSI";
            // 
            // IKUSI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1701, 1055);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(ATZERA);
            Controls.Add(GUZTIAK);
            Controls.Add(BESTEAK);
            Controls.Add(INPRIMAGAILUA);
            Controls.Add(ORDENAGAILUA);
            Controls.Add(MINTEGI);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IKUSI";
            Text = "GailuakIkusi";
            Load += GailuakIkusi_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MINTEGI;
        private Button ORDENAGAILUA;
        private Button INPRIMAGAILUA;
        private Button BESTEAK;
        private Button GUZTIAK;
        private Button ATZERA;
        private Label EZABATUTAKOAK;
    }
}