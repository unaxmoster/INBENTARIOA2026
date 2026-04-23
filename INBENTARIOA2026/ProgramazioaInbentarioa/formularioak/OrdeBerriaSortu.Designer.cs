namespace Inventarioa.formularioak
{
    partial class OrdeBerriaSortu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdeBerriaSortu));
            btmOrdBerria = new Button();
            ATZERA = new Button();
            label1 = new Label();
            label3 = new Label();
            txtMarka = new TextBox();
            cmbMintegia = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            cmbRAM = new ComboBox();
            txtROM = new TextBox();
            txtCPU = new TextBox();
            label2 = new Label();
            txtIdentifikazioa = new TextBox();
            SuspendLayout();
            // 
            // btmOrdBerria
            // 
            btmOrdBerria.BackColor = Color.Navy;
            btmOrdBerria.Font = new Font("Arial", 18F, FontStyle.Bold);
            btmOrdBerria.ForeColor = Color.Transparent;
            btmOrdBerria.Location = new Point(1208, 312);
            btmOrdBerria.Name = "btmOrdBerria";
            btmOrdBerria.Size = new Size(320, 172);
            btmOrdBerria.TabIndex = 32;
            btmOrdBerria.Text = "BERRIA SORTU";
            btmOrdBerria.UseVisualStyleBackColor = false;
            btmOrdBerria.Click += btmOrdBerria_Click;
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(1208, 554);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 31;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(116, 188);
            label1.Name = "label1";
            label1.Size = new Size(368, 62);
            label1.TabIndex = 33;
            label1.Text = "Marka/modeloa:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(263, 302);
            label3.Name = "label3";
            label3.Size = new Size(221, 62);
            label3.TabIndex = 35;
            label3.Text = "Mintegia:";
            // 
            // txtMarka
            // 
            txtMarka.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMarka.Location = new Point(514, 185);
            txtMarka.Name = "txtMarka";
            txtMarka.Size = new Size(633, 70);
            txtMarka.TabIndex = 38;
            // 
            // cmbMintegia
            // 
            cmbMintegia.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbMintegia.FormattingEnabled = true;
            cmbMintegia.Location = new Point(514, 312);
            cmbMintegia.Name = "cmbMintegia";
            cmbMintegia.Size = new Size(447, 70);
            cmbMintegia.TabIndex = 40;
            cmbMintegia.SelectedIndexChanged += cmbMintegia_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(304, 445);
            label5.Name = "label5";
            label5.Size = new Size(180, 62);
            label5.TabIndex = 42;
            label5.Text = "RAM-a:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(140, 575);
            label6.Name = "label6";
            label6.Size = new Size(344, 62);
            label6.TabIndex = 43;
            label6.Text = "ROM/memoria:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(162, 710);
            label7.Name = "label7";
            label7.Size = new Size(322, 62);
            label7.TabIndex = 44;
            label7.Text = "Prozesagailua:";
            // 
            // cmbRAM
            // 
            cmbRAM.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRAM.FormattingEnabled = true;
            cmbRAM.Items.AddRange(new object[] { "4", "8", "16", "32", "64" });
            cmbRAM.Location = new Point(514, 442);
            cmbRAM.Name = "cmbRAM";
            cmbRAM.Size = new Size(447, 70);
            cmbRAM.TabIndex = 45;
            // 
            // txtROM
            // 
            txtROM.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtROM.Location = new Point(514, 572);
            txtROM.Name = "txtROM";
            txtROM.Size = new Size(633, 70);
            txtROM.TabIndex = 46;
            txtROM.TextChanged += textBox2_TextChanged;
            // 
            // txtCPU
            // 
            txtCPU.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCPU.Location = new Point(514, 707);
            txtCPU.Name = "txtCPU";
            txtCPU.Size = new Size(633, 70);
            txtCPU.TabIndex = 47;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(116, 67);
            label2.Name = "label2";
            label2.Size = new Size(362, 62);
            label2.TabIndex = 48;
            label2.Text = "Identifikatzailea:";
            // 
            // txtIdentifikazioa
            // 
            txtIdentifikazioa.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdentifikazioa.Location = new Point(514, 64);
            txtIdentifikazioa.Name = "txtIdentifikazioa";
            txtIdentifikazioa.Size = new Size(633, 70);
            txtIdentifikazioa.TabIndex = 49;
            // 
            // OrdeBerriaSortu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1559, 1055);
            Controls.Add(txtIdentifikazioa);
            Controls.Add(label2);
            Controls.Add(txtCPU);
            Controls.Add(txtROM);
            Controls.Add(cmbRAM);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(cmbMintegia);
            Controls.Add(txtMarka);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btmOrdBerria);
            Controls.Add(ATZERA);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OrdeBerriaSortu";
            Text = "OrdeBerriaSortu";
            Load += OrdeBerriaSortu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btmOrdBerria;
        private Button ATZERA;
        private Label label1;
        private Label label3;
        private TextBox txtMarka;
        private ComboBox cmbMintegia;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cmbRAM;
        private TextBox txtROM;
        private TextBox txtCPU;
        private Label label2;
        private TextBox txtIdentifikazioa;
    }
}