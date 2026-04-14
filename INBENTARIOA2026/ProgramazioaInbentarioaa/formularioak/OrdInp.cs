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
            this.Hide();
            OrdeBerriaSortu mintegiak = new OrdeBerriaSortu();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            GailuGuztiak mintegiak = new GailuGuztiak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            InpBerriaSortu mintegiak = new InpBerriaSortu();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
