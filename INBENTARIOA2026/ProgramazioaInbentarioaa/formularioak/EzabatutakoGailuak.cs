using Inbentarioa.DatuBasie;
using MySql.Data.MySqlClient;
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
    public partial class EzabatutakoGailuak : Form
    {
        public EzabatutakoGailuak()
        {
            InitializeComponent();
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
        private void EzabatutakoGailuak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            KargatuEzabatutakoak(); // Datuak kargatzeko funtzioa deitu
        }

        private void SARRERA_Click(object sender, EventArgs e)
        {

        }

        private void ATZERA_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }
        private void KargatuEzabatutakoak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    // Zure Ezabatutakoak taulako 3 zutabeak hartzen ditugu
                    string query = "SELECT id_ezabatua AS 'ID', id_gailua AS 'ID_Gailua',mota AS 'Gailu-Mota', marka_modeloa AS 'Modeloa', eroste_data AS 'Eroste_Data', ezabatutako_eguna AS 'ezabatze_data' FROM Ezabatutakoak";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // DataGridView-ak dgvEzabatutakoak izdena du=>
                    dgvEzabatuak.DataSource = dt;
                    // LERRO HAU: Zutabe guztiak grid-aren zabalerara egokitzeko
                    dgvEzabatuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                    // Diseinu apur bat
                    dgvEzabatuak.ReadOnly = true;
                    dgvEzabatuak.AllowUserToAddRows = false;
                    dgvEzabatuak.Columns["ID"].Width = 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }
    }
}
