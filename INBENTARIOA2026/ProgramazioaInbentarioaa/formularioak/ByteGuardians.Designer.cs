namespace Inventarioa.formularioak
{
    partial class ByteGuardians
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ByteGuardians));
            btnAdos = new Button();
            labela = new Label();
            SuspendLayout();
            // 
            // btnAdos
            // 
            btnAdos.BackColor = Color.MediumBlue;
            btnAdos.DialogResult = DialogResult.OK;
            btnAdos.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdos.ForeColor = SystemColors.ControlLightLight;
            btnAdos.Location = new Point(139, 117);
            btnAdos.Name = "btnAdos";
            btnAdos.Size = new Size(299, 79);
            btnAdos.TabIndex = 3;
            btnAdos.Text = "jarraitu";
            btnAdos.UseVisualStyleBackColor = false;
            // 
            // labela
            // 
            labela.AutoSize = true;
            labela.Font = new Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labela.ForeColor = Color.MediumBlue;
            labela.Location = new Point(44, 36);
            labela.Name = "labela";
            labela.Size = new Size(295, 62);
            labela.TabIndex = 2;
            labela.Text = "Ongi etorri, ";
            // 
            // ByteGuardians
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(577, 224);
            Controls.Add(btnAdos);
            Controls.Add(labela);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ByteGuardians";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ByteGuardians";
            Load += ByteGuardians_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdos;
        private Label labela;
    }
}