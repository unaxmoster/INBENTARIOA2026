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
    public partial class Menua : FormBase
    {
        public Menua()
        {
            InitializeComponent();
            this.FormClosing += Menua_FormClosing; // Event-a gehitu
        }

        // Goiko X botoia (FormClosing event-a)
        private void Menua_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Aplikazioa guztiz ixteko
            Application.Exit();
        }

        private void Menua_Load(object sender, EventArgs e)
        {   //pantalla osoan ikusteko
            this.WindowState = FormWindowState.Maximized;
            //Botoietan marrazkiak ikusteko
            btnGailuak.ImageList = imageList2;
            btnGailuak.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGailuak.ImageIndex = 0;        // 0 = lehen argazkia
            //___________ERABILTZAILEAK___________________
            btnErabiltzaileak.ImageList = imageList2;
            btnErabiltzaileak.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnErabiltzaileak.ImageIndex = 1;        // 1 = bigarren argazkia
            //_____________MINTEGIAK______________________
            btnMintegiak.ImageList = imageList2;
            btnMintegiak.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMintegiak.ImageIndex = 2;        // 3= hirugarren argazkia
            //__________EZABATUTAKOAK____________________
            btnEzabatuak.ImageList = imageList2;
            btnEzabatuak.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEzabatuak.ImageIndex = 3;        // 3 = Laugarren argazkia
            //__________hondatutakoak____________________
            btnHondatuak.ImageList = imageList2;
            btnHondatuak.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHondatuak.ImageIndex = 4;        // 4 = bostgarren argazkia

        }

        private void GAILUAK_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void EZABATUTAKOAK_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            EzabatutakoGailuak ezabatutakoak = new EzabatutakoGailuak();
            ezabatutakoak.ShowDialog();
            this.Close();
        }

        private void ERABILTZAILEAK_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Menua ezkutatu
            Erabiltzaileak erabForm = new Erabiltzaileak();

            erabForm.ShowDialog(); // Erabiltzaileen zerrenda ireki

            this.Show(); // Zerrenda itxi denean, Menua berriro erakutsi
        }

        private void MINTEGIAK_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Menua ezkutatu
            Mintegiak erabForm = new Mintegiak();

            erabForm.ShowDialog(); // Erabiltzaileen zerrenda ireki

            this.Show(); // Zerrenda itxi denean, Menua berriro erakutsi
        }

        private void IRTEN_Click_1(object sender, EventArgs e)
        {
            // Aplikazio osoa itxi
            Application.Exit();
        }

        private void btnHondatuak_Click(object sender, EventArgs e)
        {
            this.Hide();
            HondatutakoGailuak mintegiak = new HondatutakoGailuak();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
