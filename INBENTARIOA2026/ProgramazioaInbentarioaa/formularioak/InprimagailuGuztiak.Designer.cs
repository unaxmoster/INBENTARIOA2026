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
            BtnEzabatu2 = new Button();
            button1 = new Button();
            btnEgoeraAldatu = new Button();
            ((System.ComponentModel.ISupportInitialize)dvgInprimagailuak).BeginInit();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(1125, 775);
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
            EZABATUTAKOAK.BackColor = Color.Transparent;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.ForeColor = Color.WhiteSmoke;
            EZABATUTAKOAK.Location = new Point(482, 61);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(917, 81);
            EZABATUTAKOAK.TabIndex = 31;
            EZABATUTAKOAK.Text = "INPRIMAGAILU GUZTIAK IKUSI";
            // 
            // dvgInprimagailuak
            // 
            dvgInprimagailuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgInprimagailuak.Location = new Point(453, 176);
            dvgInprimagailuak.Name = "dvgInprimagailuak";
            dvgInprimagailuak.RowHeadersWidth = 51;
            dvgInprimagailuak.Size = new Size(965, 573);
            dvgInprimagailuak.TabIndex = 30;
            dvgInprimagailuak.DataBindingComplete += dvgInprimagailuak_DataBindingComplete;
            // 
            // BtnEzabatu2
            // 
            BtnEzabatu2.BackColor = Color.Maroon;
            BtnEzabatu2.Font = new Font("Arial", 18F, FontStyle.Bold);
            BtnEzabatu2.ForeColor = Color.Transparent;
            BtnEzabatu2.Location = new Point(776, 775);
            BtnEzabatu2.Name = "BtnEzabatu2";
            BtnEzabatu2.Size = new Size(320, 172);
            BtnEzabatu2.TabIndex = 33;
            BtnEzabatu2.Text = "EZABATU";
            BtnEzabatu2.UseVisualStyleBackColor = false;
            BtnEzabatu2.Click += BtnEzabatu_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(428, 775);
            button1.Name = "button1";
            button1.Size = new Size(320, 172);
            button1.TabIndex = 34;
            button1.Text = "BERRIA SORTU";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnEgoeraAldatu
            // 
            btnEgoeraAldatu.BackColor = Color.Navy;
            btnEgoeraAldatu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEgoeraAldatu.ForeColor = Color.Transparent;
            btnEgoeraAldatu.Location = new Point(1488, 388);
            btnEgoeraAldatu.Name = "btnEgoeraAldatu";
            btnEgoeraAldatu.Size = new Size(320, 172);
            btnEgoeraAldatu.TabIndex = 35;
            btnEgoeraAldatu.Text = "EGOERA ALDATU";
            btnEgoeraAldatu.UseVisualStyleBackColor = false;
            btnEgoeraAldatu.Click += btnEgoeraAldatu_Click;
            // 
            // InprimagailuGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 986);
            Controls.Add(btnEgoeraAldatu);
            Controls.Add(button1);
            Controls.Add(BtnEzabatu2);
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
        private Button BtnEzabatu2;
        private Button button1;
        private Button btnEgoeraAldatu;
    }
}