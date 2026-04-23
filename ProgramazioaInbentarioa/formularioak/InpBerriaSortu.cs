using Inbentarioa.DatuBasie;
using Inbentarioa.formularioak;
using Inventarioa.Objetuak;
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
    public partial class InpBerriaSortu : FormBase
    {
        public InpBerriaSortu()
        {
            InitializeComponent();
        }

        private void InpBerriaSortu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            KargatuMintegiakCombo(); 
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
            this.Hide(); // Ezkutatu bakarrik
            Menua menua = new Menua();
            menua.ShowDialog();
            this.Show(); // Menua itxi ondoren, berriz erakutsi
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Balidazio basikoa (adibidez)
                if (string.IsNullOrWhiteSpace(textInpIdentifikatzailea.Text))
                {
                    MessageBox.Show("Identifikatzailea beharrezkoa da.");
                    return;
                }

                // 2. OBJEKTUA SORTU (Inprimagailua klasea erabiliz)
                // koloretakoa: txtTinta-ren arabera (true/false)
                bool isKoloretakoa = (txtTinta.Text == "Koloretakoa");

                Inprimagailua inprimagailuBerria = new Inprimagailua(
                    textInpIdentifikatzailea.Text,
                    txtMarka.Text,
                    Convert.ToInt32(cmbMintegia.SelectedValue),
                    isKoloretakoa
                );

                inprimagailuBerria.Egoera = 0; // Hasierako egoera: Ondo

                // 3. Klaseari deitu gordetzeko
                if (DBGailuak.GehituInprimagailuaPOO(inprimagailuBerria))
                {
                    MessageBox.Show("Inprimagailua ondo gorde da!");

                    // 4. Leihoak kudeatu
                    this.Hide();
                    InprimagailuGuztiak leihoa = new InprimagailuGuztiak();
                    leihoa.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea inprimagailua sortzean: " + ex.Message);
            }
        }
    }
}
