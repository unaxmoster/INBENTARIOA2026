using Inbentarioa.formularioak;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventarioa.formularioak
{
    public partial class FormMezua : FormBase
    {
        // Konstruktorea mezua jasotzeko
        public FormMezua(string testua)
        {
             InitializeComponent();
             lblMezua.Text = testua; // Label-ean mezua jarri
            this.StartPosition = FormStartPosition.CenterParent; // Pantaila erdian agertzeko
        }

            // Botoiei klik egitean leihoa itxi dadin (DialogResult konfiguratuta baduzu ez da beharrezkoa, 
            // baina badaezpada botoi bakoitzean Double-Click egin eta hau jarri dezakezu):
        private void btnBai_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Yes; }
        private void btnEz_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.No; }
    }
}
