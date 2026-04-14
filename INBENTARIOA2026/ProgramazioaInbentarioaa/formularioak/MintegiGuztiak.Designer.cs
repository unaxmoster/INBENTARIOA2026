namespace Inventarioa.formularioak
{
    partial class MintegiGuztiak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MintegiGuztiak));
            cbMintegiak = new ComboBox();
            ATZERA = new Button();
            EZABATUTAKOAK = new Label();
            dvgOrdenagailuak = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dvgOrdenagailuak).BeginInit();
            SuspendLayout();
            // 
            // cbMintegiak
            // 
            cbMintegiak.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbMintegiak.FormattingEnabled = true;
            cbMintegiak.Location = new Point(655, 136);
            cbMintegiak.Name = "cbMintegiak";
            cbMintegiak.Size = new Size(602, 70);
            cbMintegiak.TabIndex = 37;
            cbMintegiak.SelectedIndexChanged += cbMintegiak_SelectedIndexChanged;
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(793, 775);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 36;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.Location = new Point(547, 52);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(813, 81);
            EZABATUTAKOAK.TabIndex = 35;
            EZABATUTAKOAK.Text = "MINTEGIKA GAILUAK IKUSI";
            // 
            // dvgOrdenagailuak
            // 
            dvgOrdenagailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgOrdenagailuak.Location = new Point(465, 224);
            dvgOrdenagailuak.Name = "dvgOrdenagailuak";
            dvgOrdenagailuak.RowHeadersWidth = 51;
            dvgOrdenagailuak.Size = new Size(1008, 530);
            dvgOrdenagailuak.TabIndex = 34;
            // 
            // MintegiGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1831, 994);
            Controls.Add(cbMintegiak);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgOrdenagailuak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MintegiGuztiak";
            Text = "MintegiGuztiak";
            Load += MintegiGuztiak_Load;
            ((System.ComponentModel.ISupportInitialize)dvgOrdenagailuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbMintegiak;
        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dvgOrdenagailuak;
    }
}