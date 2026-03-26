namespace Inbentarioa.formularioak
{
    partial class Gailuak
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
            GEHITU = new Button();
            ALDATU = new Button();
            EZABATU = new Button();
            IKUSI = new Button();
            GAILUA = new Label();
            ATZERA = new Button();
            SuspendLayout();
            // 
            // GEHITU
            // 
            GEHITU.BackColor = Color.LightGray;
            GEHITU.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            GEHITU.Location = new Point(109, 260);
            GEHITU.Name = "GEHITU";
            GEHITU.Size = new Size(250, 130);
            GEHITU.TabIndex = 0;
            GEHITU.Text = "GEHITU";
            GEHITU.UseVisualStyleBackColor = false;
            // 
            // ALDATU
            // 
            ALDATU.BackColor = Color.LightGray;
            ALDATU.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ALDATU.Location = new Point(109, 396);
            ALDATU.Name = "ALDATU";
            ALDATU.Size = new Size(250, 130);
            ALDATU.TabIndex = 1;
            ALDATU.Text = "ALDATU";
            ALDATU.UseVisualStyleBackColor = false;
            // 
            // EZABATU
            // 
            EZABATU.BackColor = Color.LightGray;
            EZABATU.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            EZABATU.Location = new Point(379, 260);
            EZABATU.Name = "EZABATU";
            EZABATU.Size = new Size(250, 130);
            EZABATU.TabIndex = 2;
            EZABATU.Text = "EZABATU";
            EZABATU.UseVisualStyleBackColor = false;
            // 
            // IKUSI
            // 
            IKUSI.BackColor = Color.LightGray;
            IKUSI.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            IKUSI.Location = new Point(379, 398);
            IKUSI.Name = "IKUSI";
            IKUSI.Size = new Size(250, 130);
            IKUSI.TabIndex = 3;
            IKUSI.Text = "IKUSI";
            IKUSI.UseVisualStyleBackColor = false;
            // 
            // GAILUA
            // 
            GAILUA.AutoSize = true;
            GAILUA.Font = new Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point);
            GAILUA.Location = new Point(422, 121);
            GAILUA.Name = "GAILUA";
            GAILUA.Size = new Size(228, 62);
            GAILUA.TabIndex = 4;
            GAILUA.Text = "GAILUAK";
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.LightGray;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ATZERA.Location = new Point(685, 324);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(250, 130);
            ATZERA.TabIndex = 5;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            // 
            // Gailuak
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1027, 604);
            Controls.Add(ATZERA);
            Controls.Add(GAILUA);
            Controls.Add(IKUSI);
            Controls.Add(EZABATU);
            Controls.Add(ALDATU);
            Controls.Add(GEHITU);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Gailuak";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gailuak";
            Load += Gailuak_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button GEHITU;
        private Button ALDATU;
        private Button EZABATU;
        private Button IKUSI;
        private Label GAILUA;
        private Button ATZERA;
    }
}