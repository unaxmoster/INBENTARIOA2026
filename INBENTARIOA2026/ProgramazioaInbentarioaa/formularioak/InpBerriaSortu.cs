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
    public partial class InpBerriaSortu : Form
    {
        public InpBerriaSortu()
        {
            InitializeComponent();
        }

        private void InpBerriaSortu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            KargatuMintegiakCombo(); // Metodo hau ordenagailuen formularioan daukagun bera da
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
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    // Mintegiak taulatik datuak ekarri
                    string sql = "SELECT id_mintegia, izena FROM mintegiak";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Garrantzitsua: Ziurtatu zure Designer-en 'cmbMintegia' izena duela
                    cmbMintegia.DataSource = dt;
                    cmbMintegia.DisplayMember = "izena";    // Erabiltzaileak ikusiko duena
                    cmbMintegia.ValueMember = "id_mintegia"; // DBrako IDa

                    cmbMintegia.SelectedIndex = -1; // Hasieran hutsik agertzeko
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
            Mintegiak mintegiak = new Mintegiak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Logika: Testua zenbaki bihurtu (0 Koloretakoa, 1 Zuri-beltza)
            // ComboBox-ean 'Koloretakoa' hautatzen bada 0 gorde, bestela 1
            int tintaZenbakia = (txtTinta.Text == "Koloretakoa") ? 0 : 1;

            string konexioaString = DbKonexioa.Instantzia.GetKonexioString();

            using (MySqlConnection conn = new MySqlConnection(konexioaString))
            {
                try
                {
                    conn.Open();
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. TXERTATU GAILUAK TAULAN
                            string sqlGailua = @"INSERT INTO Gailuak (marka_modeloa, id_mintegia, eroste_data, egoera) 
                                         VALUES (@marka, @mintegia, @data, @egoera);
                                         SELECT LAST_INSERT_ID();";

                            MySqlCommand cmdGailua = new MySqlCommand(sqlGailua, conn, trans);
                            cmdGailua.Parameters.AddWithValue("@marka", txtMarka.Text);
                            cmdGailua.Parameters.AddWithValue("@mintegia", cmbMintegia.SelectedValue);
                            cmdGailua.Parameters.AddWithValue("@data", DateTime.Now);
                            cmdGailua.Parameters.AddWithValue("@egoera", 0);

                            int berriaId = Convert.ToInt32(cmdGailua.ExecuteScalar());

                            // 2. TXERTATU INPRIMAGAILUAK TAULAN
                            // KONTUZ: Hemen 'konexio_mota' kendu dugu, zure DBan ez baitago
                            string sqlInp = @"INSERT INTO Inprimagailuak (id_gailua, koloretakoa) 
                                      VALUES (@id, @tinta)";

                            MySqlCommand cmdInp = new MySqlCommand(sqlInp, conn, trans);
                            cmdInp.Parameters.AddWithValue("@id", berriaId);
                            cmdInp.Parameters.AddWithValue("@tinta", tintaZenbakia); // 0 edo 1

                            cmdInp.ExecuteNonQuery();

                            trans.Commit();
                            MessageBox.Show("Inprimagailua ondo gorde da!");

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea gordetzean: " + ex.Message);
                }
            }
        }
    }
}
