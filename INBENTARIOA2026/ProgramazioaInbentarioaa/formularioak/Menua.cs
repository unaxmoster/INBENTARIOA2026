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
    public partial class Menua : Form
    {
        public Menua()
        {
            InitializeComponent();
        }

        private void Menua_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Definir los colores del degradado usando códigos hexadecimales
            Color colorInicio = ColorTranslator.FromHtml("#C2CBED"); // Azul claro
            Color colorFin = ColorTranslator.FromHtml("#003FA1");    // Azul oscuro

            // Crear un pincel con degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, // Área donde se aplicará el degradado
                colorInicio,         // Color inicial
                colorFin,            // Color final
                LinearGradientMode.Horizontal)) // Dirección del degradado (horizontal)
            {
                // Rellenar el fondo del formulario con el degradado
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
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
            this.Hide();
            Erabiltzaileak mintegiak = new Erabiltzaileak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void MINTEGIAK_Click_1(object sender, EventArgs e)
        {   
            this.Hide();
            Mintegiak mintegiak = new Mintegiak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void IRTEN_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
