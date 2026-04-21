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
            dgvHondatutakoak = new DataGridView();
            button1 = new Button();
            btnEgoeraAldatu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHondatutakoak).BeginInit();
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
            // dgvHondatutakoak
            // 
            dgvHondatutakoak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHondatutakoak.Location = new Point(497, 146);
            dgvHondatutakoak.Name = "dgvHondatutakoak";
            dgvHondatutakoak.RowHeadersWidth = 51;
            dgvHondatutakoak.Size = new Size(894, 610);
            dgvHondatutakoak.TabIndex = 28;
            dgvHondatutakoak.DataBindingComplete += dgvHondatutakoak_DataBindingComplete;
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
            // btnEgoeraAldatu
            // 
            btnEgoeraAldatu.BackColor = Color.Navy;
            btnEgoeraAldatu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEgoeraAldatu.ForeColor = Color.Transparent;
            btnEgoeraAldatu.Location = new Point(1438, 338);
            btnEgoeraAldatu.Name = "btnEgoeraAldatu";
            btnEgoeraAldatu.Size = new Size(320, 172);
            btnEgoeraAldatu.TabIndex = 33;
            btnEgoeraAldatu.Text = "EGOERA ALDATU";
            btnEgoeraAldatu.UseVisualStyleBackColor = false;
            btnEgoeraAldatu.Click += btnEgoeraAldatu_Click;
            // 
            // HondatutakoGailuak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 976);
            Controls.Add(btnEgoeraAldatu);
            Controls.Add(button1);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dgvHondatutakoak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HondatutakoGailuak";
            Text = "HondatutakoGailuak";
            Load += HondatutakoGailuak_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHondatutakoak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dgvHondatutakoak;
        private Button button1;
        private Button btnEgoeraAldatu;
    }
}