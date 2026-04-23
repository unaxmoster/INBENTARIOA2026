namespace Inventarioa.formularioak
{
    partial class FormMezua
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
            Bai = new Button();
            button1 = new Button();
            lblMezua = new Label();
            SuspendLayout();
            // 
            // Bai
            // 
            Bai.BackColor = Color.ForestGreen;
            Bai.DialogResult = DialogResult.Yes;
            Bai.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Bai.ForeColor = SystemColors.ControlLightLight;
            Bai.Location = new Point(206, 97);
            Bai.Name = "Bai";
            Bai.Size = new Size(118, 47);
            Bai.TabIndex = 0;
            Bai.Text = "Bai";
            Bai.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Red;
            button1.DialogResult = DialogResult.No;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(358, 97);
            button1.Name = "button1";
            button1.Size = new Size(118, 47);
            button1.TabIndex = 1;
            button1.Text = "Ez";
            button1.UseVisualStyleBackColor = false;
            // 
            // lblMezua
            // 
            lblMezua.Location = new Point(138, 41);
            lblMezua.Name = "lblMezua";
            lblMezua.Size = new Size(407, 25);
            lblMezua.TabIndex = 2;
            lblMezua.Text = "label1";
            lblMezua.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormMezua
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(653, 188);
            Controls.Add(lblMezua);
            Controls.Add(button1);
            Controls.Add(Bai);
            Name = "FormMezua";
            Text = "FormMezua";
            ResumeLayout(false);
        }

        #endregion

        private Button Bai;
        private Button button1;
        private Label lblMezua;
    }
}