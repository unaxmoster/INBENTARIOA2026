namespace Inventarioa.formularioak
{
    partial class OrdenagailuGuztiak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdenagailuGuztiak));
            btnEgoeraAldatuOr = new Button();
            btnBerriaSortuOr = new Button();
            BtnEzabatuOr = new Button();
            btnAtzeraOr = new Button();
            EZABATUTAKOAK = new Label();
            dvgOrdenagailuak = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dvgOrdenagailuak).BeginInit();
            SuspendLayout();
            // 
            // btnEgoeraAldatuOr
            // 
            btnEgoeraAldatuOr.BackColor = Color.Navy;
            btnEgoeraAldatuOr.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEgoeraAldatuOr.ForeColor = Color.Transparent;
            btnEgoeraAldatuOr.Location = new Point(1470, 360);
            btnEgoeraAldatuOr.Name = "btnEgoeraAldatuOr";
            btnEgoeraAldatuOr.Size = new Size(320, 172);
            btnEgoeraAldatuOr.TabIndex = 41;
            btnEgoeraAldatuOr.Text = "EGOERA ALDATU";
            btnEgoeraAldatuOr.UseVisualStyleBackColor = false;
            btnEgoeraAldatuOr.Click += btnEgoeraAldatuOr_Click;
            // 
            // btnBerriaSortuOr
            // 
            btnBerriaSortuOr.BackColor = Color.Navy;
            btnBerriaSortuOr.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnBerriaSortuOr.ForeColor = Color.Transparent;
            btnBerriaSortuOr.Location = new Point(410, 747);
            btnBerriaSortuOr.Name = "btnBerriaSortuOr";
            btnBerriaSortuOr.Size = new Size(320, 172);
            btnBerriaSortuOr.TabIndex = 40;
            btnBerriaSortuOr.Text = "BERRIA SORTU";
            btnBerriaSortuOr.UseVisualStyleBackColor = false;
            btnBerriaSortuOr.Click += btnBerriaSortuOr_Click;
            // 
            // BtnEzabatuOr
            // 
            BtnEzabatuOr.BackColor = Color.Maroon;
            BtnEzabatuOr.Font = new Font("Arial", 18F, FontStyle.Bold);
            BtnEzabatuOr.ForeColor = Color.Transparent;
            BtnEzabatuOr.Location = new Point(758, 749);
            BtnEzabatuOr.Name = "BtnEzabatuOr";
            BtnEzabatuOr.Size = new Size(320, 172);
            BtnEzabatuOr.TabIndex = 39;
            BtnEzabatuOr.Text = "EZABATU";
            BtnEzabatuOr.UseVisualStyleBackColor = false;
            BtnEzabatuOr.Click += BtnEzabatuOr_Click;
            // 
            // btnAtzeraOr
            // 
            btnAtzeraOr.BackColor = Color.Crimson;
            btnAtzeraOr.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnAtzeraOr.ForeColor = Color.Transparent;
            btnAtzeraOr.Location = new Point(1112, 747);
            btnAtzeraOr.Name = "btnAtzeraOr";
            btnAtzeraOr.Size = new Size(320, 172);
            btnAtzeraOr.TabIndex = 38;
            btnAtzeraOr.Text = "ATZERA";
            btnAtzeraOr.UseVisualStyleBackColor = false;
            btnAtzeraOr.Click += btnAtzeraOr_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.BackColor = Color.Transparent;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.ForeColor = Color.WhiteSmoke;
            EZABATUTAKOAK.Location = new Point(464, 33);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(906, 81);
            EZABATUTAKOAK.TabIndex = 37;
            EZABATUTAKOAK.Text = "ORDENAGAILU GUZTIAK IKUSI";
            // 
            // dvgOrdenagailuak
            // 
            dvgOrdenagailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgOrdenagailuak.Location = new Point(435, 148);
            dvgOrdenagailuak.Name = "dvgOrdenagailuak";
            dvgOrdenagailuak.RowHeadersWidth = 51;
            dvgOrdenagailuak.Size = new Size(965, 573);
            dvgOrdenagailuak.TabIndex = 36;
            dvgOrdenagailuak.DataBindingComplete += dvgOrdenagailuak_DataBindingComplete;
            // 
            // OrdenagailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1004);
            Controls.Add(btnEgoeraAldatuOr);
            Controls.Add(btnBerriaSortuOr);
            Controls.Add(BtnEzabatuOr);
            Controls.Add(btnAtzeraOr);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgOrdenagailuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OrdenagailuGuztiak";
            Text = "ORDENAGAILU GUZTIAK IKUSI";
            Load += OrdenagailuGuztiak_Load;
            ((System.ComponentModel.ISupportInitialize)dvgOrdenagailuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnEgoeraAldatuOr;
        private Button btnBerriaSortuOr;
        private Button BtnEzabatuOr;
        private Button btnAtzeraOr;
        private Label EZABATUTAKOAK;
        private DataGridView dvgOrdenagailuak;
    }
}