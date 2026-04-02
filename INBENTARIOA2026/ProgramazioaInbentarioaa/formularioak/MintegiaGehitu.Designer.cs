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
            textBox1 = new TextBox();
            lblizena = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            EZABATUTAKOAK = new Button();
            IRTEN = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(1224, 508);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(435, 70);
            textBox1.TabIndex = 7;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // lblizena
            // 
            lblizena.AutoSize = true;
            lblizena.BackColor = Color.Transparent;
            lblizena.Font = new Font("Arial Black", 48F, FontStyle.Bold, GraphicsUnit.Point);
            lblizena.ForeColor = Color.Navy;
            lblizena.Location = new Point(380, 331);
            lblizena.Name = "lblizena";
            lblizena.Size = new Size(854, 113);
            lblizena.TabIndex = 8;
            lblizena.Text = "Mintegiaren izena:";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(1224, 356);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(435, 70);
            textBox2.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Black", 48F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(651, 473);
            label1.Name = "label1";
            label1.Size = new Size(583, 113);
            label1.TabIndex = 10;
            label1.Text = "Arduraduna:";
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.BackColor = Color.Navy;
            EZABATUTAKOAK.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            EZABATUTAKOAK.ForeColor = SystemColors.ButtonHighlight;
            EZABATUTAKOAK.Location = new Point(769, 623);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(250, 130);
            EZABATUTAKOAK.TabIndex = 26;
            EZABATUTAKOAK.Text = "GEHITU";
            EZABATUTAKOAK.UseVisualStyleBackColor = false;
            // 
            // IRTEN
            // 
            IRTEN.BackColor = Color.Crimson;
            IRTEN.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            IRTEN.ForeColor = SystemColors.ButtonHighlight;
            IRTEN.Location = new Point(1054, 623);
            IRTEN.Name = "IRTEN";
            IRTEN.Size = new Size(291, 130);
            IRTEN.TabIndex = 28;
            IRTEN.Text = "ATZERA";
            IRTEN.UseVisualStyleBackColor = false;
            IRTEN.Click += IRTEN_Click;
            // 
            // MintegiaGehitu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 893);
            Controls.Add(IRTEN);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(lblizena);
            Controls.Add(textBox1);
            Name = "MintegiaGehitu";
            Text = "MintegiaGehitu";
            Load += MintegiaGehitu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private Label lblizena;
        private TextBox textBox2;
        private Label label1;
        private Button EZABATUTAKOAK;
        private Button IRTEN;
    }
}