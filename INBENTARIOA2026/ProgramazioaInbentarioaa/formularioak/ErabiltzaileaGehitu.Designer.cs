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
            ATZERA = new Button();
            button1 = new Button();
            lblizena = new Label();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            SuspendLayout();
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = SystemColors.ButtonHighlight;
            ATZERA.Location = new Point(784, 419);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(250, 130);
            ATZERA.TabIndex = 26;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(517, 419);
            button1.Name = "button1";
            button1.Size = new Size(250, 130);
            button1.TabIndex = 27;
            button1.Text = "GORDE";
            button1.UseVisualStyleBackColor = false;
            // 
            // lblizena
            // 
            lblizena.AutoSize = true;
            lblizena.BackColor = Color.Transparent;
            lblizena.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            lblizena.ForeColor = Color.Navy;
            lblizena.Location = new Point(-41, 63);
            lblizena.Name = "lblizena";
            lblizena.Size = new Size(859, 113);
            lblizena.TabIndex = 29;
            lblizena.Text = "Erabiltzaile berria:";
            lblizena.Click += lblizena_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Black", 48F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(363, 173);
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
            label2.Location = new Point(447, 286);
            label2.Name = "label2";
            label2.Size = new Size(371, 113);
            label2.TabIndex = 31;
            label2.Text = "Ardura:";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 28.2F);
            textBox2.Location = new Point(812, 98);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(435, 70);
            textBox2.TabIndex = 33;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(812, 208);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(435, 70);
            comboBox1.TabIndex = 34;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(812, 321);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(435, 70);
            comboBox2.TabIndex = 35;
            // 
            // ErabiltzaileaGehitu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1486, 739);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblizena);
            Controls.Add(button1);
            Controls.Add(ATZERA);
            Name = "ErabiltzaileaGehitu";
            Text = "ErabiltzaileaGehitu";
            Load += ErabiltzaileaGehitu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ATZERA;
        private Button button1;
        private Label lblizena;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
    }
}