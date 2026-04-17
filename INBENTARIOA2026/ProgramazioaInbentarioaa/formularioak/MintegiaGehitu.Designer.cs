namespace Inbentarioa.formularioak
{
    partial class MintegiaGehitu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MintegiaGehitu));
            lblizena = new Label();
            txtMintegiIzena = new TextBox();
            label1 = new Label();
            btnGehitu = new Button();
            IRTEN = new Button();
            cmbArduraduna = new ComboBox();
            SuspendLayout();
            // 
            // lblizena
            // 
            lblizena.AutoSize = true;
            lblizena.BackColor = Color.Transparent;
            lblizena.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            lblizena.ForeColor = Color.Navy;
            lblizena.Location = new Point(380, 331);
            lblizena.Name = "lblizena";
            lblizena.Size = new Size(854, 113);
            lblizena.TabIndex = 8;
            lblizena.Text = "Mintegiaren izena:";
            // 
            // txtMintegiIzena
            // 
            txtMintegiIzena.Font = new Font("Segoe UI", 28.2F);
            txtMintegiIzena.Location = new Point(1224, 366);
            txtMintegiIzena.Name = "txtMintegiIzena";
            txtMintegiIzena.Size = new Size(473, 70);
            txtMintegiIzena.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(651, 473);
            label1.Name = "label1";
            label1.Size = new Size(583, 113);
            label1.TabIndex = 10;
            label1.Text = "Arduraduna:";
            // 
            // btnGehitu
            // 
            btnGehitu.BackColor = Color.Navy;
            btnGehitu.Font = new Font("Arial", 18F, FontStyle.Bold);
            btnGehitu.ForeColor = SystemColors.ButtonHighlight;
            btnGehitu.Location = new Point(816, 623);
            btnGehitu.Name = "btnGehitu";
            btnGehitu.Size = new Size(250, 130);
            btnGehitu.TabIndex = 26;
            btnGehitu.Text = "GEHITU";
            btnGehitu.UseVisualStyleBackColor = false;
            btnGehitu.Click += EZABATUTAKOAK_Click;
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.Crimson;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold);
            IRTEN.ForeColor = SystemColors.ButtonHighlight;
            IRTEN.Location = new Point(1091, 623);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(291, 130);
            IRTEN.TabIndex = 28;
            IRTEN.Text = "ATZERA";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click;
            // 
            // cmbArduraduna
            // 
            cmbArduraduna.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbArduraduna.FormattingEnabled = true;
            cmbArduraduna.Location = new Point(1224, 508);
            cmbArduraduna.Name = "cmbArduraduna";
            cmbArduraduna.Size = new Size(473, 70);
            cmbArduraduna.TabIndex = 41;
            // 
            // MintegiaGehitu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 893);
            Controls.Add(cmbArduraduna);
            Controls.Add(IRTEN);
            Controls.Add(btnGehitu);
            Controls.Add(label1);
            Controls.Add(txtMintegiIzena);
            Controls.Add(lblizena);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MintegiaGehitu";
            Text = "MintegiaGehitu";
            Load += MintegiaGehitu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblizena;
        private TextBox txtMintegiIzena;
        private Label label1;
        private Button btnGehitu;
        private Button IRTEN;
        private ComboBox cmbArduraduna;
    }
}