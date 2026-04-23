using Inventarioa.formularioak;
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

namespace Inbentarioa.formularioak
{
    public partial class IKUSI : FormBase
    {
        public IKUSI()
        {
            InitializeComponent();
        }

        private void GailuakIkusi_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void GUZTIAK_Click(object sender, EventArgs e)
        {
            this.Hide();
            GailuGuztiak mintegiak = new GailuGuztiak();
            mintegiak.ShowDialog();
            this.Close();

        }

        private void ORDENAGAILUA_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdenagailuGuztiak mintegiak = new OrdenagailuGuztiak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void INPRIMAGAILUA_Click(object sender, EventArgs e)
        {
            this.Hide();
            InprimagailuGuztiak mintegiak = new InprimagailuGuztiak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void MINTEGI_Click(object sender, EventArgs e)
        {
            this.Hide();
            MintegiGuztiak mintegiak = new MintegiGuztiak();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
