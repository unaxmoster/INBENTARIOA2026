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
            btnEzabatu = new Button();
            button1 = new Button();
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
            dvgGailuak.CellContentClick += dataGridView1_CellContentClick;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
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
            btnEzabatu.Click += btnEzabatu_Click_1;
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(482, 754);
            button1.Name = "button1";
            button1.Size = new Size(320, 172);
            button1.TabIndex = 28;
            button1.Text = "BERRIA SORTU";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // GailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
            Controls.Add(button1);
            Controls.Add(btnEzabatu);
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
        private Button btnEzabatu;
        private Button button1;
    }
}