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
    public partial class OrdeBerriaSortu : Form
    {
        public OrdeBerriaSortu()
        {
            InitializeComponent();
        }

        private void OrdeBerriaSortu_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            // Mintegiak kargatzeko metodoari deia
            KargatuMintegiakCombo();
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

        private void KargatuMintegiakCombo()
        {
            try
            {
                // Konexioa lortu zure DbKonexioa klasea erabiliz
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    // Mintegiak taulatik id-a eta izena lortzeko SQL-a
                    string sql = "SELECT id_mintegia, izena FROM mintegiak";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // ComboBox-a konfiguratu
                    cmbMintegia.DataSource = dt;
                    cmbMintegia.DisplayMember = "izena";    // Erabiltzaileak ikusiko duen testua
                    cmbMintegia.ValueMember = "id_mintegia"; // Atzean gordeko den ID zenbakia

                    // Aukera huts batekin hasteko (hautazkoa)
                    cmbMintegia.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message);
            }
        }
        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void btmOrdBerria_Click(object sender, EventArgs e)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                try
                {
                    conn.Open();
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. TXERTATU GAILUAK TAULAN (Gurasoa)
                            // Ez dugu id_gailua jartzen, AUTO_INCREMENT delako orain.
                            string sqlGailua = @"INSERT INTO Gailuak (marka_modeloa, id_mintegia, eroste_data, egoera) 
                                         VALUES (@marka, @mintegia, @data, @egoera);
                                         SELECT LAST_INSERT_ID();";

                            MySqlCommand cmdGailua = new MySqlCommand(sqlGailua, conn, trans);
                            cmdGailua.Parameters.AddWithValue("@marka", txtMarka.Text);
                            cmdGailua.Parameters.AddWithValue("@mintegia", cmbMintegia.SelectedValue);
                            cmdGailua.Parameters.AddWithValue("@data", DateTime.Now);
                            cmdGailua.Parameters.AddWithValue("@egoera", 0); // Defektuz 0 = Ondo

                            // MySQL-k sortu duen ID berria jasotzen dugu
                            int berriaId = Convert.ToInt32(cmdGailua.ExecuteScalar());

                            // 2. TXERTATU ORDENAGAILUAK TAULAN (Umea)
                            // Hemen gurasoan sortutako ID berbera erabiltzen dugu lotura egiteko
                            string sqlOrdenagailua = @"INSERT INTO Ordenagailuak (id_gailua, ram, rom, cpu) 
                                               VALUES (@id, @ram, @rom, @cpu)";

                            MySqlCommand cmdOrd = new MySqlCommand(sqlOrdenagailua, conn, trans);
                            cmdOrd.Parameters.AddWithValue("@id", berriaId);
                            cmdOrd.Parameters.AddWithValue("@ram", cmbRAM.Text);
                            cmdOrd.Parameters.AddWithValue("@rom", txtROM.Text);
                            cmdOrd.Parameters.AddWithValue("@cpu", txtCPU.Text);

                            cmdOrd.ExecuteNonQuery();

                            // Dena ondo badoa, aldaketak gorde
                            trans.Commit();
                            MessageBox.Show("Ordenagailua ondo gorde da! (ID: " + berriaId + ")");
                            // Formularioa kargatzeko eguneratutako datuekin
                            this.DialogResult = DialogResult.OK;

                            this.Close(); // Formularioa itxi
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex; // Kanpoko catch-era bidali
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea gordetzean: " + ex.Message);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMintegia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
