namespace Inbentarioa.formularioak
{
    partial class Erabiltzaileak
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
            dataGridView1 = new DataGridView();
            EZABATUTAKOAK = new Label();
            ATZERA = new Button();
            ErabBerriaSortu = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(599, 275);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(721, 295);
            dataGridView1.TabIndex = 1;
            // 
            // EZABATUTAKOAK
            // 
            EZABATUTAKOAK.AutoSize = true;
            EZABATUTAKOAK.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            EZABATUTAKOAK.Location = new Point(700, 164);
            EZABATUTAKOAK.Name = "EZABATUTAKOAK";
            EZABATUTAKOAK.Size = new Size(509, 81);
            EZABATUTAKOAK.TabIndex = 24;
            EZABATUTAKOAK.Text = "ERABILTZAILEAK";
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = SystemColors.ButtonHighlight;
            ATZERA.Location = new Point(1112, 603);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(250, 130);
            ATZERA.TabIndex = 25;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // ErabBerriaSortu
            // 
            ErabBerriaSortu.BackColor = Color.Navy;
            ErabBerriaSortu.Font = new Font("Arial", 18F, FontStyle.Bold);
            ErabBerriaSortu.ForeColor = SystemColors.ButtonHighlight;
            ErabBerriaSortu.Location = new Point(555, 603);
            ErabBerriaSortu.Name = "ErabBerriaSortu";
            ErabBerriaSortu.Size = new Size(250, 130);
            ErabBerriaSortu.TabIndex = 26;
            ErabBerriaSortu.Text = "Berria sortu";
            ErabBerriaSortu.UseVisualStyleBackColor = false;
            ErabBerriaSortu.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Navy;
            button2.Font = new Font("Arial", 18F, FontStyle.Bold);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(835, 603);
            button2.Name = "button2";
            button2.Size = new Size(250, 130);
            button2.TabIndex = 27;
            button2.Text = "Ezabatu";
            button2.UseVisualStyleBackColor = false;
            // 
            // Erabiltzaileak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1385, 1007);
            Controls.Add(button2);
            Controls.Add(ErabBerriaSortu);
            Controls.Add(ATZERA);
            Controls.Add(EZABATUTAKOAK);
            Controls.Add(dataGridView1);
            Name = "Erabiltzaileak";
            Text = "Erabiltzaileak";
            Load += Erabiltzaileak_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label EZABATUTAKOAK;
        private Button ATZERA;
        private Button ErabBerriaSortu;
        private Button button2;
    }
}