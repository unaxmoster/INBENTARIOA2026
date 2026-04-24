namespace Inventarioa.formularioak
{
    partial class ErabiltzaileaGehitu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErabiltzaileaGehitu));
            ATZERA = new Button();
            btnGorde = new Button();
            lblizena = new Label();
            label1 = new Label();
            label2 = new Label();
            txtErIzena = new TextBox();
            cbMintegia = new ComboBox();
            cbArduraduna = new ComboBox();
            txtPass = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = SystemColors.ButtonHighlight;
            ATZERA.Location = new Point(1187, 599);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(250, 130);
            ATZERA.TabIndex = 26;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.Navy;
            btnGorde.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnGorde.ForeColor = SystemColors.ButtonHighlight;
            btnGorde.Location = new Point(869, 599);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(250, 130);
            btnGorde.TabIndex = 27;
            btnGorde.Text = "GORDE";
            btnGorde.UseVisualStyleBackColor = false;
            btnGorde.Click += btnGorde_Click_1;
            // 
            // lblizena
            // 
            lblizena.AutoSize = true;
            lblizena.BackColor = Color.Transparent;
            lblizena.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            lblizena.ForeColor = Color.Navy;
            lblizena.Location = new Point(292, 136);
            lblizena.Name = "lblizena";
            lblizena.Size = new Size(827, 113);
            lblizena.TabIndex = 29;
            lblizena.Text = "Erabiltzaile izena:";
            lblizena.Click += lblizena_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(676, 353);
            label1.Name = "label1";
            label1.Size = new Size(455, 113);
            label1.TabIndex = 30;
            label1.Text = "Mintegia:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(760, 466);
            label2.Name = "label2";
            label2.Size = new Size(371, 113);
            label2.TabIndex = 31;
            label2.Text = "Ardura:";
            // 
            // txtErIzena
            // 
            txtErIzena.Font = new Font("Segoe UI", 28.2F);
            txtErIzena.Location = new Point(1125, 171);
            txtErIzena.Name = "txtErIzena";
            txtErIzena.Size = new Size(435, 70);
            txtErIzena.TabIndex = 33;
            // 
            // cbMintegia
            // 
            cbMintegia.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMintegia.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbMintegia.FormattingEnabled = true;
            cbMintegia.Location = new Point(1125, 388);
            cbMintegia.Name = "cbMintegia";
            cbMintegia.Size = new Size(435, 70);
            cbMintegia.TabIndex = 34;
            cbMintegia.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // cbArduraduna
            // 
            cbArduraduna.DropDownStyle = ComboBoxStyle.DropDownList;
            cbArduraduna.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbArduraduna.FormattingEnabled = true;
            cbArduraduna.Items.AddRange(new object[] { "IKT arduraduna", "Mintegi burua", "Irakaslea" });
            cbArduraduna.Location = new Point(1125, 501);
            cbArduraduna.Name = "cbArduraduna";
            cbArduraduna.Size = new Size(435, 70);
            cbArduraduna.TabIndex = 35;
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 28.2F);
            txtPass.Location = new Point(1125, 282);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(435, 70);
            txtPass.TabIndex = 36;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(619, 247);
            label3.Name = "label3";
            label3.Size = new Size(500, 113);
            label3.TabIndex = 37;
            label3.Text = "Pasahitza:";
            label3.Click += label3_Click;
            // 
            // ErabiltzaileaGehitu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1841, 1055);
            Controls.Add(label3);
            Controls.Add(txtPass);
            Controls.Add(cbArduraduna);
            Controls.Add(cbMintegia);
            Controls.Add(txtErIzena);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblizena);
            Controls.Add(btnGorde);
            Controls.Add(ATZERA);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ErabiltzaileaGehitu";
            Text = "ErabiltzaileaGehitu";
            Load += ErabiltzaileaGehitu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Button btnGorde;
        private Label lblizena;
        private Label label1;
        private Label label2;
        private TextBox txtErIzena;
        private ComboBox cbMintegia;
        private ComboBox cbArduraduna;
        private TextBox txtPass;
        private Label label3;
    }
}