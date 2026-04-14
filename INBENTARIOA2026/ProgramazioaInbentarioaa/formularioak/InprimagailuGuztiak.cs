using Inbentarioa.DatuBasie;
using Inbentarioa.formularioak;
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

namespace Inventarioa.formularioak
{
    public partial class InprimagailuGuztiak : Form
    {
        public InprimagailuGuztiak()
        {
            InitializeComponent();
            KargatuGailuak();
        }
        private void InprimagailuGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }
        private void KargatuGailuak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // Ordenagailuak bakarrik ikusteko, gogoratu JOIN Ordenagailuak egitea
                    // Hemen jarri dizut kontsulta, hardware datuak ere ikusteko:
                    string query = "SELECT G.id_gailua AS 'ID', G.marka_modeloa AS 'Modeloa', " +
                                   "M.izena AS 'Mintegia', G.eroste_data AS 'Data', " +
                                   "CASE " +
                                   "  WHEN G.egoera = '0' THEN 'Zuri-beltza' " +
                                   "  WHEN G.egoera = '1' THEN 'Koloretakoa' " +
                                   "  ELSE 'Ezezaguna' " +
                                   "END AS 'Kolorea', " +
                                                                      "CASE " +
                                   "  WHEN G.egoera = '0' THEN 'Ondo' " +
                                   "  WHEN G.egoera = '1' THEN 'Matxuratuta' " +
                                   "  WHEN G.egoera = '2' THEN 'Konpontzen' " +
                                   "  ELSE 'Ezezaguna' " +
                                   "END AS 'Egoera' " +
                                   "FROM Gailuak G " +
                                   "JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia " +
                                   "JOIN Inprimagailuak I ON G.id_gailua = I.id_gailua"; // <-- Inprimagailuak iragazteko

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dvgInprimagailuak.DataSource = dt;
                    // Erabiltzaileak ezin du gelaxketan idatzi (Irakurtzeko soilik)
                    dvgInprimagailuak.ReadOnly = true;

                    // Erabiltzaileak ezin ditu lerro berriak eskuz gehitu grid-aren behealdean
                    dvgInprimagailuak.AllowUserToAddRows = false;

                    // Erabiltzaileak ezin ditu lerroak ezabatu (Supr sakatuta adibidez)
                    dvgInprimagailuak.AllowUserToDeleteRows = false;

                    // --- ID-aren zabalera eta lerrokatzea ---
                    dvgInprimagailuak.Columns["ID"].Width = 45;
                    dvgInprimagailuak.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }
    }
}
