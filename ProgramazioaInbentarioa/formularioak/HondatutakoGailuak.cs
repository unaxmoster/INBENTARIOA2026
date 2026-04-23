using Inbentarioa;
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
    public partial class HondatutakoGailuak : FormBase
    {
        public HondatutakoGailuak()
        {
            InitializeComponent();
            // Gertaera lotu karga ziurtatzeko
            dgvHondatutakoak.DataBindingComplete += dgvHondatutakoak_DataBindingComplete;
            KargatuGailuHondatuak();
        }

        private void HondatutakoGailuak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
        }

        private void konfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                btnEgoeraAldatu.Enabled = false;
                
            }
            else if (rola == "MintegiBurua")
            {
                btnEgoeraAldatu.Enabled = false;
            }
        }

        private void KargatuGailuHondatuak()
        {
            try
            {
                // 1. Garbitu grid-a (zutabeak bikoiztu ez daitezen)
                dgvHondatutakoak.Columns.Clear();
                dgvHondatutakoak.DataSource = null;

                // 2. Eskatu datuak klaseari
                DataTable dt = DBGailuak.GetHondatutakoGailuak();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Ez dago hondatutako gailurik datu-basean.");
                    return;
                }

                // 3. Lehenik ComboBox zutabea sortu (eskuz)
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.HeaderText = "Egoera Berria";
                comboCol.Name = "EgoeraCombo";
                comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                dgvHondatutakoak.Columns.Add(comboCol);

                // 4. Lotu datuak
                dgvHondatutakoak.DataSource = dt;
                //4-5. Zutabe zuriak ezkutatu (ComboBox-a bakarrik utzi editagarri)
                        // 1. Grid osoari editatzen utzi (hau garrantzitsua da)
                        dgvHondatutakoak.ReadOnly = false;

                        // 2. Erabiltzaileak lerro berriak eskuz gehitzea desgaitu (azken lerro zuri hori kentzeko)
                        dgvHondatutakoak.AllowUserToAddRows = false;

                        // 3. Datuak lotu ondoren, zutabeka kontrolatu baimenak
                        foreach (DataGridViewColumn col in dgvHondatutakoak.Columns)
                        {
                            if (col.Name == "EgoeraCombo")
                            {
                                // ComboBox-a denez, editagarri utzi
                                col.ReadOnly = false;
                                col.Visible = true;
                            }
                        }
                if (dgvHondatutakoak.Columns.Contains("EgoeraCombo"))
                {
                    // DisplayIndex-ari balio altu bat emanez (adibidez, zutabe kopurua - 1),
                    // azkenengo tokira mugitzen da automatikoki.
                    dgvHondatutakoak.Columns["EgoeraCombo"].DisplayIndex = dgvHondatutakoak.Columns.Count - 1;
                }

                // 5. Ezkutatu ID-a eta egoera balioa (Datu-baseko zutabeak)
                if (dgvHondatutakoak.Columns.Contains("ID")) dgvHondatutakoak.Columns["ID"].Visible = false;
                if (dgvHondatutakoak.Columns.Contains("egoera_balioa")) dgvHondatutakoak.Columns["egoera_balioa"].Visible = false;

                dgvHondatutakoak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea kargatzean: " + ex.Message);
            }
        }

        

        private void KargatuEgoeraBalioakGrid()
        {
            foreach (DataGridViewRow row in dgvHondatutakoak.Rows)
            {
                if (row.Cells["egoera_balioa"].Value != null)
                {
                    int index = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                    if (index >= 0 && index <= 2)
                    {
                        row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxCell)row.Cells["EgoeraCombo"]).Items[index];
                    }
                }
            }
        }
        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void dgvHondatutakoak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            KargatuEgoeraBalioakGrid();
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dgvHondatutakoak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dgvHondatutakoak.SelectedRows[0];
                    int idGailua = Convert.ToInt32(row.Cells["ID"].Value);
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value?.ToString();

                    int egoeraBerria = 0; // Ondo
                    if (hautatutakoEgoera == "Matxuratuta") egoeraBerria = 1;
                    else if (hautatutakoEgoera == "Konpontzen") egoeraBerria = 2;

                    string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                    using (MySqlConnection conn = new MySqlConnection(konexioa))
                    {
                        conn.Open();
                        string query = "UPDATE Gailuak SET egoera = @egoera WHERE id_gailua = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@egoera", egoeraBerria);
                        cmd.Parameters.AddWithValue("@id", idGailua);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Egoera aldatu da. Zerrendatik desagertuko da 'Ondo' edo 'Konpontzen' bada.");
                        KargatuGailuHondatuak(); // Freskatu (desagertu egingo da egoera 1 ez bada)
                    }
                }
                catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
            }
            else { MessageBox.Show("Hautatu gailu bat."); }
        }
    }
}
