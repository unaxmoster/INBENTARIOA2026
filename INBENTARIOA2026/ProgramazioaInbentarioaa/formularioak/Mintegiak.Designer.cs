namespace Inbentarioa.formularioak
{
    partial class Mintegiak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mintegiak));
            btnEzabatu = new Button();
            SARRERA = new Label();
            IRTEN = new Button();
            EZABATUTAKOAK = new Button();
            dgvMintegiLista = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMintegiLista).BeginInit();
            SuspendLayout();
            // 
            // btnEzabatu
            // 
            btnEzabatu.BackColor = Color.Navy;
            btnEzabatu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnEzabatu.ForeColor = Color.AliceBlue;
            btnEzabatu.Location = new Point(798, 671);
            btnEzabatu.Name = "btnEzabatu";
            btnEzabatu.Size = new Size(291, 130);
            btnEzabatu.TabIndex = 29;
            btnEzabatu.Text = "EZABATU";
            btnEzabatu.UseVisualStyleBackColor = false;
            btnEzabatu.Click += btnEzabatu_Click;
            // 
            // SARRERA
            // 
            SARRERA.AutoSize = true;
            SARRERA.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            SARRERA.Location = new Point(798, 30);
            SARRERA.Name = "SARRERA";
            SARRERA.Size = new Size(368, 81);
            SARRERA.TabIndex = 28;
            SARRERA.Text = "MINTEGIAK";
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.Crimson;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold);
            IRTEN.ForeColor = SystemColors.ButtonHighlight;
            IRTEN.Location = new Point(1127, 671);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(291, 130);
            IRTEN.TabIndex = 27;
            IRTEN.Text = "ATZERA";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.BackColor = Color.Navy;
            EZABATUTAKOAK.Font = new Font("Arial", 18F, FontStyle.Bold);
            EZABATUTAKOAK.ForeColor = SystemColors.ButtonHighlight;
            EZABATUTAKOAK.Location = new Point(504, 671);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(250, 130);
            EZABATUTAKOAK.TabIndex = 25;
            EZABATUTAKOAK.Text = "GEHITU";
            EZABATUTAKOAK.UseVisualStyleBackColor = false;
            EZABATUTAKOAK.Click += EZABATUTAKOAK_Click;
            // 
            // dgvMintegiLista
            // 
            dgvMintegiLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMintegiLista.Location = new Point(684, 114);
            dgvMintegiLista.Name = "dgvMintegiLista";
            dgvMintegiLista.RowHeadersWidth = 51;
            dgvMintegiLista.Size = new Size(600, 530);
            dgvMintegiLista.TabIndex = 35;
            dgvMintegiLista.CellContentClick += dgvMintegiLista_CellContentClick;
            // 
            // Mintegiak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1055);
            Controls.Add(dgvMintegiLista);
            Controls.Add(btnEzabatu);
            Controls.Add(SARRERA);
            Controls.Add(IRTEN);
            Controls.Add(EZABATUTAKOAK);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Mintegiak";
            Text = "Mintegiak";
            Load += Mintegiak_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMintegiLista).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnEzabatu;
        private Label SARRERA;
        private Button IRTEN;
        private Button EZABATUTAKOAK;
        private DataGridView dgvMintegiLista;
    }
}