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
            ATZERA = new Button();
            EZABATUTAKOAK = new Label();
            dvgOrdenagailuak = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dvgOrdenagailuak).BeginInit();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(871, 758);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 29;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.Location = new Point(547, 63);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(906, 81);
            EZABATUTAKOAK.TabIndex = 28;
            EZABATUTAKOAK.Text = "ORDENAGAILU GUZTIAK IKUSI";
            // 
            // dvgOrdenagailuak
            // 
            dvgOrdenagailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgOrdenagailuak.Location = new Point(486, 147);
            dvgOrdenagailuak.Name = "dvgOrdenagailuak";
            dvgOrdenagailuak.RowHeadersWidth = 51;
            dvgOrdenagailuak.Size = new Size(1033, 588);
            dvgOrdenagailuak.TabIndex = 27;
            dvgOrdenagailuak.CellContentClick += dvgOrdenagailuak_CellContentClick;
            // 
            // OrdenagailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgOrdenagailuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OrdenagailuGuztiak";
            Text = "OrdenagailuGuztiak";
            Load += OrdenagailuGuztiak_Load;
            ((System.ComponentModel.ISupportInitialize)dvgOrdenagailuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dvgOrdenagailuak;
    }
}