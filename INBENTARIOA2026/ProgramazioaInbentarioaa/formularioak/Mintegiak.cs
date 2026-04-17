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
    public partial class Mintegiak : Form
    {
        public Mintegiak()
        {
            InitializeComponent();
        }

        private void Mintegiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Mintegiak kargatzeko metodoari deia=>
            KargatuMintegiak();
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

        private void KargatuMintegiak()
        {
            try
            {
                // Konexioa lortu
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // SQL kontsulta: id_mintegia eta izena bakarrik
                    string query = @"SELECT M.id_mintegia AS 'ID', 
                        M.izena AS 'Mintegiaren Izena', 
                        E.erabiltzailea AS 'Arduraduna' 
                      FROM mintegiak M
                      LEFT JOIN erabiltzaileak E ON M.id_arduraduna = E.id_erabiltzailea";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Datuak Grid-ean kargatu
                    dgvMintegiLista.DataSource = dt;

                    // --- Formatu txukuna emateko ---
                    dgvMintegiLista.ReadOnly = true;
                    dgvMintegiLista.AllowUserToAddRows = false;
                    dgvMintegiLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // ID zutabeari tamaina zehatza eman (txikiagoa)
                    if (dgvMintegiLista.Columns.Contains("ID"))
                    {
                        dgvMintegiLista.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvMintegiLista.Columns["ID"].Width = 60;
                        dgvMintegiLista.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message);
            }
        }
        private void IRTEN_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void GAILUAK_Click(object sender, EventArgs e)
        {
            this.Hide();
            MintegiLista mintegiak = new MintegiLista();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void EZABATUTAKOAK_Click(object sender, EventArgs e)
        {
            this.Hide();
            MintegiaGehitu mintegiak = new MintegiaGehitu();
            mintegiak.ShowDialog();
            this.Close();
        }

 

        private void dgvMintegiLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
