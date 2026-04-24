namespace Inventarioa.formularioak
{
    partial class InpBerriaSortu
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
            button1 = new Button();
            ATZERA = new Button();
            txtTinta = new ComboBox();
            txtMarka = new TextBox();
            label2 = new Label();
            label1 = new Label();
            cmbMintegia = new ComboBox();
            label4 = new Label();
            labelIdent = new Label();
            textInpIdentifikatzailea = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(395, 710);
            button1.Name = "button1";
            button1.Size = new Size(320, 172);
            button1.TabIndex = 3;
            button1.Text = "BERRIA SORTU";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(826, 710);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 4;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // txtTinta
            // 
            txtTinta.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTinta.FormattingEnabled = true;
            txtTinta.Items.AddRange(new object[] { "Koloretakoa", "Zuri-beltza" });
            txtTinta.Location = new Point(557, 401);
            txtTinta.Name = "txtTinta";
            txtTinta.Size = new Size(447, 70);
            txtTinta.TabIndex = 43;
            // 
            // txtMarka
            // 
            txtMarka.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMarka.Location = new Point(554, 235);
            txtMarka.Name = "txtMarka";
            txtMarka.Size = new Size(633, 70);
            txtMarka.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(229, 404);
            label2.Name = "label2";
            label2.Size = new Size(286, 62);
            label2.TabIndex = 41;
            label2.Text = "Koloretakoa:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(147, 238);
            label1.Name = "label1";
            label1.Size = new Size(368, 62);
            label1.TabIndex = 40;
            label1.Text = "Marka/modeloa:";
            // 
            // cmbMintegia
            // 
            cmbMintegia.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbMintegia.FormattingEnabled = true;
            cmbMintegia.Location = new Point(557, 566);
            cmbMintegia.Name = "cmbMintegia";
            cmbMintegia.Size = new Size(447, 70);
            cmbMintegia.TabIndex = 47;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(294, 566);
            label4.Name = "label4";
            label4.Size = new Size(221, 62);
            label4.TabIndex = 46;
            label4.Text = "Mintegia:";
            // 
            // labelIdent
            // 
            labelIdent.AutoSize = true;
            labelIdent.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelIdent.Location = new Point(147, 87);
            labelIdent.Name = "labelIdent";
            labelIdent.Size = new Size(362, 62);
            labelIdent.TabIndex = 48;
            labelIdent.Text = "Identifikatzailea:";
            // 
            // textInpIdentifikatzailea
            // 
            textInpIdentifikatzailea.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textInpIdentifikatzailea.Location = new Point(554, 84);
            textInpIdentifikatzailea.Name = "textInpIdentifikatzailea";
            textInpIdentifikatzailea.Size = new Size(633, 70);
            textInpIdentifikatzailea.TabIndex = 1;
            // 
            // InpBerriaSortu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1564, 1020);
            Controls.Add(textInpIdentifikatzailea);
            Controls.Add(labelIdent);
            Controls.Add(cmbMintegia);
            Controls.Add(label4);
            Controls.Add(txtTinta);
            Controls.Add(txtMarka);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(ATZERA);
            Name = "InpBerriaSortu";
            Text = "InpBerriaSortu";
            Load += InpBerriaSortu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button ATZERA;
        private ComboBox txtTinta;
        private TextBox txtMarka;
        private Label label2;
        private Label label1;
        private ComboBox cmbMintegia;
        private Label label4;
        private Label labelIdent;
        private TextBox textInpIdentifikatzailea;
    }
}