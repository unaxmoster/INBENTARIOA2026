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
    public partial class OrdInp : Form
    {
        public OrdInp()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color colorInicio = ColorTranslator.FromHtml("#C2CBED");
            Color colorFin = ColorTranslator.FromHtml("#003FA1");

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                colorInicio,
                colorFin,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
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
                // ShowDialog-ek leihoa irekita mantentzen du eta hau blokeatzen du
                if (ordeForm.ShowDialog() == DialogResult.OK)
                {
                   // ordeForm.ShowDialog();
                }
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (InpBerriaSortu inpForm = new InpBerriaSortu())
            {
                if (inpForm.ShowDialog() == DialogResult.OK)
                {
                    //inpForm.ShowDialog();
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
