using Inbentarioa.DatuBasie;
using MySql.Data.MySqlClient; // <-- HAU GEHITU DUT: MySql erroreak kentzeko
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
    public partial class GailuGuztiak : Form
    {
        public GailuGuztiak()
        {
            InitializeComponent();
            KargatuGailuak(); // <-- Hemen deitu behar diogu datuak kargatzeko!
        }

        private void GailuGuztiak_Load(object sender, EventArgs e)
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

        // Gailuak ikusteko funtzioa
        private void KargatuGailuak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string query = "SELECT G.id_gailua AS 'ID', " +
                                   //Id-a 3 karaktere azken lerroekin bat etor dadin, eta gailu mota ikusteko, JOIN-ak erabiliz.
                                   // BIGARREN ZUTABEA: Gailu mota
                                   "CASE " +
                                   "  WHEN O.id_gailua IS NOT NULL THEN 'Ordenagailua' " +
                                   "  WHEN I.id_gailua IS NOT NULL THEN 'Inprimagailua' " +
                                   "  ELSE 'Besterik' " +
                                   "END AS 'Gailu mota', " +
                                   "G.marka_modeloa AS 'Modeloa', " +
                                   "M.izena AS 'Mintegia', G.eroste_data AS 'Data', " +
                                   "CASE " +
                                   "  WHEN G.egoera = '0' THEN 'Ondo' " +
                                   "  WHEN G.egoera = '1' THEN 'Matxuratuta' " +
                                   "  WHEN G.egoera = '2' THEN 'Konpontzen' " +
                                   "  ELSE 'Ezezaguna' " +
                                   "END AS 'Egoera' " +
                                   "FROM Gailuak G " +
                                   "JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia " +
                                   "LEFT JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua " +
                                   "LEFT JOIN Inprimagailuak I ON G.id_gailua = I.id_gailua";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Datuak kargatu ostean
                    dvgGailuak.DataSource = dt;

                    // --- Zutabeak ez aldatu ahal izateko=>> ---

                    // Erabiltzaileak ezin du gelaxketan idatzi (Irakurtzeko soilik)
                    dvgGailuak.ReadOnly = true;

                    // Erabiltzaileak ezin ditu lerro berriak eskuz gehitu grid-aren behealdean
                    dvgGailuak.AllowUserToAddRows = false;

                    // Erabiltzaileak ezin ditu lerroak ezabatu (Supr sakatuta adibidez)
                    dvgGailuak.AllowUserToDeleteRows = false;

                    // --- ID-aren zabalera eta lerrokatzea ---
                    dvgGailuak.Columns["ID"].Width = 45;
                    dvgGailuak.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Hemen zerbait egin nahi baduzu gelaxka baten gainean klik egitean
        }


        private void btnEzabatu_Click_1(object sender, EventArgs e)
        {
            if (dvgGailuak.SelectedRows.Count > 0)
            {
                var erantzuna = MessageBox.Show("Gailu hau ezabatu eta 'Ezabatutakoak' zerrendara mugitu nahi duzu?", "Berretsi ezabatzea", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dvgGailuak.SelectedRows[0].Cells["ID"].Value);
                        string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                        using (MySqlConnection conn = new MySqlConnection(konexioa))
                        {
                            conn.Open();
                            // Transakzioa hasten dugu bi mugimenduak batera egiteko
                            using (MySqlTransaction trans = conn.BeginTransaction())
                            {
                                try
                                {
                                    // 1. KOPIATU datuak Ezabatutakoak taulara
                                    // Oharra: Ziurtatu EzabatutakoGailuak taulak Gailuak taularen egitura bera duela
                                    string insertQuery = "INSERT INTO Ezabatutakoak (id_gailua, marka_modeloa, id_mintegia, eroste_data, egoera) " +
                                                         "SELECT id_gailua, marka_modeloa, id_mintegia, eroste_data, egoera " +
                                                         "FROM Gailuak WHERE id_gailua = @id";

                                    MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn, trans);
                                    cmdInsert.Parameters.AddWithValue("@id", id);
                                    cmdInsert.ExecuteNonQuery();

                                    // 2. EZABATU jatorrizko taulatik
                                    string deleteQuery = "DELETE FROM Gailuak WHERE id_gailua = @id";
                                    MySqlCommand cmdDelete = new MySqlCommand(deleteQuery, conn, trans);
                                    cmdDelete.Parameters.AddWithValue("@id", id);
                                    cmdDelete.ExecuteNonQuery();

                                    // Dena ondo badago, aldaketak berretsi
                                    trans.Commit();
                                    MessageBox.Show("Gailua ondo mugitu da Ezabatutakoen zerrendara.");
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback(); // Errorea badago, dena lehen bezala utzi
                                    throw ex;
                                }
                            }
                        }
                        KargatuGailuak(); // Grid-a freskatu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea prozesuan: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Mesedez, hautatu lerro oso bat.");
            }
        }
    }
}