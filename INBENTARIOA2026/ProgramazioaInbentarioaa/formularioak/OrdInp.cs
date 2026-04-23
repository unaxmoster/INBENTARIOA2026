using Inbentarioa.formularioak;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventarioa.formularioak
{
    public partial class OrdInp : FormBase
    {
        public OrdInp()
        {
            InitializeComponent();
        }

        private void OrdInp_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ez egin 'this.Hide()' hemen
            using (OrdeBerriaSortu ordeForm = new OrdeBerriaSortu())
            {
                    ordeForm.ShowDialog();
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (InpBerriaSortu inpForm = new InpBerriaSortu())
            {
                DialogResult emaitza = inpForm.ShowDialog();

                if (emaitza == DialogResult.OK)
                {
                    // Inprimagailua ondo sortu da, eguneratu zerrenda edo egin zerbait
                    MessageBox.Show("Inprimagailua ondo sortu da!");
                }
                else if (emaitza == DialogResult.Cancel)
                {
                    // Erabiltzaileak atzera egin du
                    MessageBox.Show("Ezeztatu egin da sorkuntza");
                }
            }
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }


    }
}
