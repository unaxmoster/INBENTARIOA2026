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
        //Elementuak kodez zentratzeko modua
        private void InprimagailuGuztiak_Resize(object sender, EventArgs e)
        {
            // Titulua zentratu
            EZABATUTAKOAK.Left = (this.ClientSize.Width - EZABATUTAKOAK.Width) / 2;

            // Grid-a zentratu eta tamaina egokitu (marjinak utziz)
            dvgInprimagailuak.Width = (int)(this.ClientSize.Width * 0.8); // Pantailaren %80
            dvgInprimagailuak.Left = (this.ClientSize.Width - dvgInprimagailuak.Width) / 2;

            // Botoia zentratu
            ATZERA.Left = (this.ClientSize.Width - ATZERA.Width) / 2;
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

                    // LERRO HAU: Zutabe guztiak grid-aren zabalerara egokitzeko
                    dvgInprimagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // --- ID-aren zabalera eta lerrokatzea ---
                    // ID-a txiki geratzea nahi baduzu, AutoSizeMode aldatu behar zaio berari bakarrik
                    dvgInprimagailuak.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dvgInprimagailuak.Columns["ID"].Width = 45;
                    dvgInprimagailuak.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }

        private void BtnEzabatu_Click(object sender, EventArgs e)
        {
            if (dvgInprimagailuak.SelectedRows.Count > 0)
            {
                var erantzuna = MessageBox.Show("Gailu hau betirako ezabatu eta historikora mugitu nahi duzu?", "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dvgInprimagailuak.SelectedRows[0].Cells["ID"].Value);
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
                                                            'Inprimagailua', -- Hemen zuzenean jartzen dugu balioa
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
                                    if (mota == "Inprimagailua") deleteUmeaQuery = "DELETE FROM Inprimagailuak WHERE id_gailua = @id";
                                    else if (mota == "Ordenagailua") deleteUmeaQuery = "DELETE FROM Ordenagailuak WHERE id_gailua = @id";

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            InpBerriaSortu mintegiak = new InpBerriaSortu();
            mintegiak.ShowDialog();
            this.Close();
        }
    }
}
