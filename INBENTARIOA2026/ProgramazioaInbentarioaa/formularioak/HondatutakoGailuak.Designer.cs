namespace Inventarioa.formularioak
{
    partial class HondatutakoGailuak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HondatutakoGailuak));
            ATZERA = new Button();
            EZABATUTAKOAK = new Label();
            dgvHondatuak = new DataGridView();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHondatuak).BeginInit();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = SystemColors.ButtonHighlight;
            ATZERA.Location = new Point(958, 797);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(250, 160);
            ATZERA.TabIndex = 30;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.Location = new Point(575, 43);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(761, 81);
            EZABATUTAKOAK.TabIndex = 29;
            EZABATUTAKOAK.Text = "HONDATUTAKO GAILUAK";
            // 
            // dgvHondatuak
            // 
            dgvHondatuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHondatuak.Location = new Point(497, 146);
            dgvHondatuak.Name = "dgvHondatuak";
            dgvHondatuak.RowHeadersWidth = 51;
            dgvHondatuak.Size = new Size(894, 610);
            dgvHondatuak.TabIndex = 28;
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(670, 797);
            button1.Name = "button1";
            button1.Size = new Size(243, 160);
            button1.TabIndex = 31;
            button1.Text = "GORDE";
            button1.UseVisualStyleBackColor = false;
            // 
            // HondatutakoGailuak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1589, 976);
            Controls.Add(button1);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dgvHondatuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HondatutakoGailuak";
            Text = "HondatutakoGailuak";
            Load += HondatutakoGailuak_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHondatuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dgvHondatuak;
        private Button button1;
    }
}