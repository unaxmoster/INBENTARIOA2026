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
            dvgMintegika = new DataGridView();
            button1 = new Button();
            BtnEzabatu = new Button();
            ((System.ComponentModel.ISupportInitialize)dvgMintegika).BeginInit();
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
            ATZERA.Location = new Point(1153, 775);
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
            // dvgMintegika
            // 
            dvgMintegika.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgMintegika.Location = new Point(465, 224);
            dvgMintegika.Name = "dvgMintegika";
            dvgMintegika.RowHeadersWidth = 51;
            dvgMintegika.Size = new Size(1008, 530);
            dvgMintegika.TabIndex = 34;
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(465, 775);
            button1.Name = "button1";
            button1.Size = new Size(320, 172);
            button1.TabIndex = 38;
            button1.Text = "BERRIA SORTU";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // BtnEzabatu
            // 
            BtnEzabatu.BackColor = Color.Maroon;
            BtnEzabatu.Font = new Font("Arial", 18F, FontStyle.Bold);
            BtnEzabatu.ForeColor = Color.Transparent;
            BtnEzabatu.Location = new Point(812, 775);
            BtnEzabatu.Name = "BtnEzabatu";
            BtnEzabatu.Size = new Size(320, 172);
            BtnEzabatu.TabIndex = 39;
            BtnEzabatu.Text = "EZABATU";
            BtnEzabatu.UseVisualStyleBackColor = false;
            BtnEzabatu.Click += BtnEzabatu_Click;
            // 
            // MintegiGuztiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1831, 994);
            Controls.Add(BtnEzabatu);
            Controls.Add(button1);
            Controls.Add(cbMintegiak);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dvgMintegika);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MintegiGuztiak";
            Text = "MintegiGuztiak";
            Load += MintegiGuztiak_Load;
            ((System.ComponentModel.ISupportInitialize)dvgMintegika).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbMintegiak;
        private Button ATZERA;
        private Label EZABATUTAKOAK;
        private DataGridView dvgMintegika;
        private Button button1;
        private Button BtnEzabatu;
    }
}