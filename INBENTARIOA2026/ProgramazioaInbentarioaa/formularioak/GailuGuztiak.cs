using Inbentarioa.DatuBasie;
using Inventarioa.formularioak;
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
                var erantzuna = MessageBox.Show("Gailu hau betirako ezabatu eta historikora mugitu nahi duzu?", "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dvgGailuak.SelectedRows[0].Cells["ID"].Value);
                        string mota = dvgGailuak.SelectedRows[0].Cells["Gailu mota"].Value.ToString();
                        string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                        using (MySqlConnection conn = new MySqlConnection(konexioa))
                        {
                            conn.Open();
                            using (MySqlTransaction trans = conn.BeginTransaction())
                            {
                                try
                                {
                                    // 1. KOPIATU datuak historikora (3 zutabeak)
                                    // INSERT kontsulta berria, CASE logika barruan duela:
                                    string insertQuery = @"
                                                        INSERT INTO Ezabatutakoak (id_gailua, marka_modeloa, mota, eroste_data, id_mintegia) 
                                                        SELECT 
                                                            G.id_gailua, 
                                                            G.marka_modeloa, 
                                                            CASE 
                                                                WHEN O.id_gailua IS NOT NULL THEN 'Ordenagailua' 
                                                                WHEN I.id_gailua IS NOT NULL THEN 'Inprimagailua' 
                                                                ELSE 'Besterik' 
                                                            END, -- Honek 'mota' zutabea beteko du
                                                            G.eroste_data, 
                                                            G.id_mintegia
                                                        FROM Gailuak G
                                                        LEFT JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua
                                                        LEFT JOIN Inprimagailuak I ON G.id_gailua = I.id_gailua
                                                        WHERE G.id_gailua = @id";

                                    MySqlCommand cmdInsert = new MySqlCommand(insertQuery, conn, trans);
                                    cmdInsert.Parameters.AddWithValue("@id", id);
                                    cmdInsert.ExecuteNonQuery();

                                    // 2. EZABATU UMEA (Ordenagailua edo Inprimagailua)
                                    // Lehenik 'Hondatutakoak'
                                    string deleteHondatu = "DELETE FROM Hondatutakoak WHERE id_gailua = @id";
                                    MySqlCommand cmdHondatu = new MySqlCommand(deleteHondatu, conn, trans);
                                    cmdHondatu.Parameters.AddWithValue("@id", id);
                                    cmdHondatu.ExecuteNonQuery();
                                    // Herentzia mantentzeko, umea lehenago ezabatu behar da FK-agatik
                                    string deleteUmeaQuery = "";
                                    if (mota == "Ordenagailua") deleteUmeaQuery = "DELETE FROM Ordenagailuak WHERE id_gailua = @id";
                                    else if (mota == "Inprimagailua") deleteUmeaQuery = "DELETE FROM Inprimagailuak WHERE id_gailua = @id";

                                    if (!string.IsNullOrEmpty(deleteUmeaQuery))
                                    {
                                        MySqlCommand cmdUmea = new MySqlCommand(deleteUmeaQuery, conn, trans);
                                        cmdUmea.Parameters.AddWithValue("@id", id);
                                        cmdInsert.Parameters.AddWithValue("@mota", mota); // Hemen pasatzen diogu Grid-etik hartutako testua
                                        cmdUmea.ExecuteNonQuery();
                                    }

                                    // 3. EZABATU GURASOA (Gailuak taulatik)
                                    string deleteGurasoaQuery = "DELETE FROM Gailuak WHERE id_gailua = @id";
                                    MySqlCommand cmdGurasoa = new MySqlCommand(deleteGurasoaQuery, conn, trans);
                                    cmdGurasoa.Parameters.AddWithValue("@id", id);
                                    cmdGurasoa.ExecuteNonQuery();

                                    trans.Commit();
                                    MessageBox.Show("Gailua ondo ezabatu eta historikora mugitu da.");
                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    throw ex;
                                }
                            }
                        }
                        KargatuGailuak(); // Grid-a freskatu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea ezabatzean: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdInp mintegiak = new OrdInp();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
