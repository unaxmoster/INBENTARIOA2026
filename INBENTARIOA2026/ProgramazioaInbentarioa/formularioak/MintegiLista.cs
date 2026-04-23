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
    public partial class MintegiLista : FormBase
    {
        public MintegiLista()
        {
            InitializeComponent();
        }

        private void SARRERA_Click(object sender, EventArgs e)
        {

        }

        private void MintegiLista_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void IRTEN_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mintegiak mintegiak = new Mintegiak();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
