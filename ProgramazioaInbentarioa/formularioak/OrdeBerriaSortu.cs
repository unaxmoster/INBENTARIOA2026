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
using Inventarioa.Objetuak; // Ziurtatu namespace hau zure klasearena dela

namespace Inventarioa.formularioak
{
    public partial class OrdeBerriaSortu : FormBase
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
            try
            {
                // 1. Datuak balidatu
                if (string.IsNullOrWhiteSpace(txtIdentifikazioa.Text))
                {
                    MessageBox.Show("Identifikazio kodea ezin da hutsik egon.");
                    return;
                }

                if (cmbMintegia.SelectedValue == null || cmbMintegia.SelectedIndex == -1)
                {
                    MessageBox.Show("Mesedez, aukeratu mintegi bat.");
                    return;
                }

                // 2. Objektua sortu datuekin (POO erara)
                Ordenagailua ordeBerria = new Ordenagailua(
                    txtIdentifikazioa.Text,
                    txtMarka.Text,
                    Convert.ToInt32(cmbMintegia.SelectedValue),
                    cmbRAM.Text,
                    txtROM.Text,
                    txtCPU.Text
                );

                // 3. Logika klaseari deitu objektu osoa bidaliz
                if (DBGailuak.GehituOrdenagailuaPOO(ordeBerria))
                {
                    MessageBox.Show("Ordenagailua ondo gorde da!");

                    // Formulario hau itxi (DialogResult OK itzuliz)
                    this.DialogResult = DialogResult.OK;
                    this.Close(); // Honek formularioa itxiko du eta OrdenagailuGuztiak-era bueltatuko da
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea ordenagailua gehitzean: " + ex.Message);
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
