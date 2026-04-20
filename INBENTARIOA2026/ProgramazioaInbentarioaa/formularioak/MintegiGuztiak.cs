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
    public partial class MintegiGuztiak : Form
    {
        public MintegiGuztiak()
        {
            InitializeComponent();
            KargatuMintegiakCombo(); // ComboBox-a betetzen du
            KargatuGailuak(0);       // Hasieran denak erakusten ditu
        }

        private void MintegiGuztiak_Load(object sender, EventArgs e)
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
        private void KargatuMintegiakCombo()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string query = "SELECT id_mintegia, izena FROM Mintegiak";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // "Guztiak" aukera sortu eta DataTable-aren hasieran sartu
                    DataRow row = dt.NewRow();
                    row["id_mintegia"] = 0; // 0 erabiliko dugu ID bezala "Guztiak" denerako
                    row["izena"] = "Mintegi guztietako gailuak";
                    dt.Rows.InsertAt(row, 0);

                    cbMintegiak.DataSource = dt;
                    cbMintegiak.DisplayMember = "izena";      // Erabiltzaileak ikusiko duena
                    cbMintegiak.ValueMember = "id_mintegia"; // Kodeak barnean erabiliko duena
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message);
            }
        }
        private void KargatuGailuak(int idMintegia)
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // SQL Kontsulta: Gailu mota CASE bidez eta LEFT JOIN-ak erabiliz
                    string query = "SELECT G.id_gailua AS 'ID', " +
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

                    // Iragazkia gehitu idMintegia 0 ez bada
                    if (idMintegia > 0)
                    {
                        query += " WHERE G.id_mintegia = @idMintegia";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    if (idMintegia > 0)
                    {
                        cmd.Parameters.AddWithValue("@idMintegia", idMintegia);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Datuak kargatu
                    dvgMintegika.DataSource = dt;

                    // --- KONFIGURAZIOA (Edizioa desgaitu eta diseinua) ---
                    dvgMintegika.ReadOnly = true;
                    dvgMintegika.AllowUserToAddRows = false;
                    dvgMintegika.AllowUserToDeleteRows = false;

                    // LERRO HAU: Zutabe guztiak grid-aren zabalerara egokitzeko
                    dvgMintegika.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // ID-aren zabalera eta lerrokatzea (Hau Fill-aren ondoren egitea hobeto)
                    if (dvgMintegika.Columns.Contains("ID"))
                    {
                        // ID-a txiki geratzea nahi baduzu, AutoSizeMode aldatu behar zaio berari bakarrik
                        dvgMintegika.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dvgMintegika.Columns["ID"].Width = 45;
                        dvgMintegika.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Modeloa bereziki zabala izatea nahi baduzu (besteak baino gehiago)
                    if (dvgMintegika.Columns.Contains("Modeloa"))
                    {
                        dvgMintegika.Columns["Modeloa"].FillWeight = 150; // Zenbat eta altuago, leku gehiago hartuko du
                    }
                    // Hautatzean lerro osoa markatzea gomendatzen dizut (ikuspegi hobea)
                    // dvgOrdenagailuak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: ");
            }
        }

        private void cbMintegiak_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ziurtatu zerbait hautatuta dagoela eta balioa zenbakia dela
            if (cbMintegiak.SelectedValue != null && int.TryParse(cbMintegiak.SelectedValue.ToString(), out int idHautatua))
            {
                KargatuGailuak(idHautatua);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdInp mintegiak = new OrdInp();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void BtnEzabatu_Click(object sender, EventArgs e)
        {
            if (dvgMintegika.SelectedRows.Count > 0)
            {
                var erantzuna = MessageBox.Show("Ziur zaude gailu hau ezabatu nahi duzula?", "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        // 1. Hartu gailuaren IDa (Grid-eko "ID" zutabetik)
                        int idGailua = Convert.ToInt32(dvgMintegika.SelectedRows[0].Cells["ID"].Value);
                        string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                        using (MySqlConnection conn = new MySqlConnection(konexioa))
                        {
                            conn.Open();
                            // Gailua ezabatzean, herentziagatik (CASCADE baduzu) umea ere ezabatuko da
                            // Ez baduzu CASCADE, lehenago umea ezabatu beharko zenuke (Ordenagailuak/Inprimagailuak)
                            string sql = "DELETE FROM Gailuak WHERE id_gailua = @id";

                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", idGailua);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Gailua ondo ezabatu da.");
                            }
                        }

                        // 2. Freskatu grid-a orain hautatuta dagoen mintegi berdinarekin
                        int idHautatua = Convert.ToInt32(cbMintegiak.SelectedValue);
                        KargatuGailuak(idHautatua);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea ezabatzean: ");
                    }
                }
            }
            else
            {
                MessageBox.Show("Mesedez, hautatu gailu bat zerrendatik.");
            }
        }
    }
}
