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

        private void IRTEN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MINTEGIAK_Click(object sender, EventArgs e)
        {
            // Ocultamos primero el formulario actual
            this.Hide();

            // Creamos y abrimos el nuevo formulario
            Mintegiak mintegiak = new Mintegiak();
            mintegiak.ShowDialog();

            // Cuando el usuario cierre Menua, cerramos definitivamente este formulario
            this.Close();
        }

        private void EZABATUTAKOAK_Click(object sender, EventArgs e)
        {
            // Ocultamos primero el formulario actual
            this.Hide();

            // Creamos y abrimos el nuevo formulario
            EzabatutakoGailuak ezabatutakoak = new EzabatutakoGailuak();
            ezabatutakoak.ShowDialog();

            // Cuando el usuario cierre Menua, cerramos definitivamente este formulario
            this.Close();
        }

        private void ERABILTZAILEAK_Click(object sender, EventArgs e)
        {
            this.Hide();
            Erabiltzaileak mintegiak = new Erabiltzaileak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void GAILUAK_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
