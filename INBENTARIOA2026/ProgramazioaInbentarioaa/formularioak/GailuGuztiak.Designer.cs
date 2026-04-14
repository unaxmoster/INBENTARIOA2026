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
            dvgGailuak = new DataGridView();
            EZABATUTAKOAK = new Label();
            ATZERA = new Button();
            ((System.ComponentModel.ISupportInitialize)dvgGailuak).BeginInit();
            SuspendLayout();
            // 
            // dvgGailuak
            // 
            dvgGailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgGailuak.Location = new Point(602, 192);
            dvgGailuak.Name = "dvgGailuak";
            dvgGailuak.RowHeadersWidth = 51;
            dvgGailuak.Size = new Size(753, 401);
            dvgGailuak.TabIndex = 0;
            dvgGailuak.CellContentClick += dataGridView1_CellContentClick;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.Location = new Point(657, 108);
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
            ATZERA.Location = new Point(829, 628);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 26;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // GailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgGailuak);
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
    }
}