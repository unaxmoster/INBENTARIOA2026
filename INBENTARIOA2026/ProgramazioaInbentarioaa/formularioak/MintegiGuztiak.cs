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
                    dvgOrdenagailuak.DataSource = dt;

                    // --- KONFIGURAZIOA (Edizioa desgaitu eta diseinua) ---
                    dvgOrdenagailuak.ReadOnly = true;
                    dvgOrdenagailuak.AllowUserToAddRows = false;
                    dvgOrdenagailuak.AllowUserToDeleteRows = false;

                    // ID-aren zabalera eta lerrokatzea
                    if (dvgOrdenagailuak.Columns.Contains("ID"))
                    {
                        dvgOrdenagailuak.Columns["ID"].Width = 45;
                        dvgOrdenagailuak.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Hautatzean lerro osoa markatzea gomendatzen dizut (ikuspegi hobea)
                    dvgOrdenagailuak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
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
    }
}
