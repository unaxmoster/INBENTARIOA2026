using Inbentarioa.DatuBasie;
using Inbentarioa.formularioak;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inventarioa.formularioak
{
    public partial class OrdenagailuGuztiak : Form
    {
        // BAKARRA utzi dugu, kargatzeko funtzioari deituz
        public OrdenagailuGuztiak()
        {
            InitializeComponent();
            KargatuGailuak();
        }

        private void OrdenagailuGuztiak_Load(object sender, EventArgs e)
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
                                   "  WHEN G.egoera = '0' THEN 'Ondo' " +
                                   "  WHEN G.egoera = '1' THEN 'Matxuratuta' " +
                                   "  WHEN G.egoera = '2' THEN 'Konpontzen' " +
                                   "  ELSE 'Ezezaguna' " +
                                   "END AS 'Egoera' " +
                                   "FROM Gailuak G " +
                                   "JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia " +
                                   "LEFT JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua"; // <-- Ordenagailuak iragazteko

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dvgOrdenagailuak.DataSource = dt;
                    // Erabiltzaileak ezin du gelaxketan idatzi (Irakurtzeko soilik)
                    dvgOrdenagailuak.ReadOnly = true;

                    // Erabiltzaileak ezin ditu lerro berriak eskuz gehitu grid-aren behealdean
                    dvgOrdenagailuak.AllowUserToAddRows = false;

                    // Erabiltzaileak ezin ditu lerroak ezabatu (Supr sakatuta adibidez)
                    dvgOrdenagailuak.AllowUserToDeleteRows = false;

                    // LERRO HAU: Zutabe guztiak grid-aren zabalerara egokitzeko
                    dvgOrdenagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // --- ID-aren zabalera eta lerrokatzea ---
                    // ID-a txiki geratzea nahi baduzu, AutoSizeMode aldatu behar zaio berari bakarrik
                    dvgOrdenagailuak.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dvgOrdenagailuak.Columns["ID"].Width = 45;
                    dvgOrdenagailuak.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();

        }

        private void dvgOrdenagailuak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Sortu formularioa 'using' baten barruan (memoria garbitzeko)
            using (OrdeBerriaSortu f = new OrdeBerriaSortu())
            {
                // 2. Ireki leihoa eta itxaron itxi arte
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // 3. ONDO JOAN BADA: Hemen deitu grid-a kargatzen duen metodoari
                    KargatuGailuak();
                }
            }
        }

        private void BtnEzabatu_Click(object sender, EventArgs e)
        {
            if (dvgOrdenagailuak.SelectedRows.Count > 0)
            {
                var erantzuna = MessageBox.Show("Gailu hau betirako ezabatu eta historikora mugitu nahi duzu?", "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dvgOrdenagailuak.SelectedRows[0].Cells["ID"].Value);
                        string mota = "Ordenagailua";
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
                                    // CASE barik, zuzenean 'Ordenagailua' testua idazten dugu 'mota' zutaberako
                                    string insertQuery = @"
                                                        INSERT INTO Ezabatutakoak (id_gailua, marka_modeloa, mota, eroste_data, id_mintegia) 
                                                        SELECT 
                                                            id_gailua, 
                                                            marka_modeloa, 
                                                            'Ordenagailua', -- Hemen zuzenean jartzen dugu balioa
                                                            eroste_data, 
                                                            id_mintegia
                                                        FROM Gailuak
                                                        WHERE id_gailua = @id";

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
    }
}