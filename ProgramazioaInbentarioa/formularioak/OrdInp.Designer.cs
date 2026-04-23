namespace Inventarioa.formularioak
{
    partial class OrdInp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdInp));
            button1 = new Button();
            ATZERA = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Navy;
            button1.Font = new Font("Arial", 18F, FontStyle.Bold);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(660, 344);
            button1.Name = "button1";
            button1.Size = new Size(320, 172);
            button1.TabIndex = 32;
            button1.Text = "ORDENAGAILUA";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ATZERA
            // 
            ATZERA.BackColor = Color.Crimson;
            ATZERA.Font = new Font("Arial", 18F, FontStyle.Bold);
            ATZERA.ForeColor = Color.Transparent;
            ATZERA.Location = new Point(856, 581);
            ATZERA.Name = "ATZERA";
            ATZERA.Size = new Size(320, 172);
            ATZERA.TabIndex = 31;
            ATZERA.Text = "ATZERA";
            ATZERA.UseVisualStyleBackColor = false;
            ATZERA.Click += ATZERA_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Navy;
            button2.Font = new Font("Arial", 18F, FontStyle.Bold);
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(1037, 344);
            button2.Name = "button2";
            button2.Size = new Size(320, 172);
            button2.TabIndex = 33;
            button2.Text = "INPRIMAGAILUA";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // OrdInp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1564, 1055);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(ATZERA);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OrdInp";
            Text = "OrdInp";
            Load += OrdInp_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button ATZERA;
        private Button button2;
    }
}