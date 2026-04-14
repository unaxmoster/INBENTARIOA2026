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
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            comboBox2 = new ComboBox();
            label3 = new Label();
            comboBox3 = new ComboBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(1219, 238);
            button1.Name = "button1";
            button1.Size = new Size(320, 172);
            button1.TabIndex = 34;
            button1.Text = "BERRIA SORTU";
            button1.UseVisualStyleBackColor = false;
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(1219, 484);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 33;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(557, 340);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(447, 70);
            comboBox1.TabIndex = 43;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(554, 235);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(633, 70);
            textBox1.TabIndex = 42;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(229, 338);
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
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(554, 450);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(447, 70);
            comboBox2.TabIndex = 45;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(333, 453);
            label3.Name = "label3";
            label3.Size = new Size(182, 62);
            label3.TabIndex = 44;
            label3.Text = "Egoera:";
            // 
            // comboBox3
            // 
            comboBox3.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(557, 566);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(447, 70);
            comboBox3.TabIndex = 47;
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
            // InpBerriaSortu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1564, 1020);
            Controls.Add(comboBox3);
            Controls.Add(label4);
            Controls.Add(comboBox2);
            Controls.Add(label3);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
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
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private ComboBox comboBox2;
        private Label label3;
        private ComboBox comboBox3;
        private Label label4;
    }
}