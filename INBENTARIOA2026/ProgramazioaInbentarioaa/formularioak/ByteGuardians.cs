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
    public partial class ByteGuardians : Form
    {
        public ByteGuardians(string Mezua)
        {
            InitializeComponent();
            labela.Text = Mezua;
        }

        private void ByteGuardians_Load(object sender, EventArgs e)
        {

        }
    }
}
