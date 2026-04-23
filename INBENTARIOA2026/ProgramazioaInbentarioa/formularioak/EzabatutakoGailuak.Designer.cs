namespace Inbentarioa.formularioak
{
    partial class EzabatutakoGailuak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EzabatutakoGailuak));
            ATZERA = new Button();
            EZABATUTAKOAK = new Label();
            dgvEzabatuak = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvEzabatuak).BeginInit();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = SystemColors.ButtonHighlight;
            ATZERA.Location = new Point(868, 845);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(250, 130);
            ATZERA.TabIndex = 27;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click_1;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.BackColor = Color.Transparent;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.ForeColor = Color.WhiteSmoke;
            EZABATUTAKOAK.Location = new Point(626, 99);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(728, 81);
            EZABATUTAKOAK.TabIndex = 26;
            EZABATUTAKOAK.Text = "EZABATUTAKO GAILUAK";
            // 
            // dgvEzabatuak
            // 
            dgvEzabatuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEzabatuak.Location = new Point(545, 202);
            dgvEzabatuak.Name = "dgvEzabatuak";
            dgvEzabatuak.RowHeadersWidth = 51;
            dgvEzabatuak.Size = new Size(894, 610);
            dgvEzabatuak.TabIndex = 25;
            // 
            // EzabatutakoGailuak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dgvEzabatuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EzabatutakoGailuak";
            Text = "EzabatutakoGailuak";
            Load += EzabatutakoGailuak_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEzabatuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dgvEzabatuak;
    }
}