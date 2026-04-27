namespace Inbentarioa.formularioak
{
    partial class GailuGuztiak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GailuGuztiak));
            dvgGailuak = new DataGridView();
            EZABATUTAKOAK = new Label();
            ATZERA = new Button();
            btnEzabatu = new Button();
            btnEgoeraAldatu = new Button();
            btnBerriaSortu = new Button();
            ((System.ComponentModel.ISupportInitialize)dvgGailuak).BeginInit();
            SuspendLayout();
            // 
            // dvgGailuak
            // 
            dvgGailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgGailuak.Location = new Point(503, 124);
            dvgGailuak.Name = "dvgGailuak";
            dvgGailuak.RowHeadersWidth = 51;
            dvgGailuak.Size = new Size(962, 595);
            dvgGailuak.TabIndex = 0;
            dvgGailuak.DataBindingComplete += dvgGailuak_DataBindingComplete;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.BackColor = Color.Transparent;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.ForeColor = Color.Transparent;
            EZABATUTAKOAK.Location = new Point(661, 27);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(658, 81);
            EZABATUTAKOAK.TabIndex = 25;
            EZABATUTAKOAK.Text = "GAILU GUZTIAK IKUSI";
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(1169, 754);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 26;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // btnEzabatu
            // 
            btnEzabatu.BackColor = Color.Firebrick;
            btnEzabatu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEzabatu.ForeColor = Color.Transparent;
            btnEzabatu.Location = new Point(823, 754);
            btnEzabatu.Name = "btnEzabatu";
            btnEzabatu.Size = new Size(320, 172);
            btnEzabatu.TabIndex = 27;
            btnEzabatu.Text = "EZABATU";
            btnEzabatu.UseVisualStyleBackColor = false;
            btnEzabatu.Click += btnEzabatu_Click;
            // 
            // btnEgoeraAldatu
            // 
            btnEgoeraAldatu.BackColor = Color.Navy;
            btnEgoeraAldatu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEgoeraAldatu.ForeColor = Color.Transparent;
            btnEgoeraAldatu.Location = new Point(1519, 375);
            btnEgoeraAldatu.Name = "btnEgoeraAldatu";
            btnEgoeraAldatu.Size = new Size(320, 172);
            btnEgoeraAldatu.TabIndex = 29;
            btnEgoeraAldatu.Text = "EGOERA ALDATU";
            btnEgoeraAldatu.UseVisualStyleBackColor = false;
            btnEgoeraAldatu.Click += btnEgoeraAldatu_Click;
            // 
            // btnBerriaSortu
            // 
            btnBerriaSortu.BackColor = Color.Navy;
            btnBerriaSortu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnBerriaSortu.ForeColor = Color.Transparent;
            btnBerriaSortu.Location = new Point(469, 754);
            btnBerriaSortu.Name = "btnBerriaSortu";
            btnBerriaSortu.Size = new Size(320, 172);
            btnBerriaSortu.TabIndex = 30;
            btnBerriaSortu.Text = "BERRIA SORTU";
            btnBerriaSortu.UseVisualStyleBackColor = false;
            btnBerriaSortu.Click += btnBerriaSortu_Click;
            // 
            // GailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
            Controls.Add(btnBerriaSortu);
            Controls.Add(btnEgoeraAldatu);
            Controls.Add(btnEzabatu);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgGailuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GailuGuztiak";
            Text = "GailuGuztiak";
            Load += GailuGuztiak_Load;
            ((System.ComponentModel.ISupportInitialize)dvgGailuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dvgGailuak;
        private Label EZABATUTAKOAK;
        private Button ATZERA;
        private Button btnEzabatu;
        private Button btnEgoeraAldatu;
        private Button btnBerriaSortu;
    }
}