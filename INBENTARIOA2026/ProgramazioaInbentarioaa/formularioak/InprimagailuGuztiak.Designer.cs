namespace Inventarioa.formularioak
{
    partial class InprimagailuGuztiak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InprimagailuGuztiak));
            ATZERA = new Button();
            EZABATUTAKOAK = new Label();
            dvgInprimagailuak = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dvgInprimagailuak).BeginInit();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(701, 775);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 32;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.Location = new Point(392, 54);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(917, 81);
            EZABATUTAKOAK.TabIndex = 31;
            EZABATUTAKOAK.Text = "INPRIMAGAILU GUZTIAK IKUSI";
            // 
            // dvgInprimagailuak
            // 
            dvgInprimagailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgInprimagailuak.Location = new Point(414, 169);
            dvgInprimagailuak.Name = "dvgInprimagailuak";
            dvgInprimagailuak.RowHeadersWidth = 51;
            dvgInprimagailuak.Size = new Size(871, 573);
            dvgInprimagailuak.TabIndex = 30;
            // 
            // InprimagailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1549, 986);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgInprimagailuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InprimagailuGuztiak";
            Text = "InprimagailuGuztiak";
            Load += InprimagailuGuztiak_Load;
            ((System.ComponentModel.ISupportInitialize)dvgInprimagailuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dvgInprimagailuak;
    }
}