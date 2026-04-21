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
    public partial class HondatutakoGailuak : Form
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

        private void KargatuGailuHondatuak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // SQL: Bakarrik egoera = 1 (Matxuratuta) dutenak kargatzen ditu
                    string query = @"SELECT G.id_gailua AS 'ID', 
                                    G.marka_modeloa AS 'Modeloa', 
                                    M.izena AS 'Mintegia', 
                                    G.egoera AS 'egoera_balioa' 
                                    FROM Gailuak G 
                                    JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia 
                                    WHERE G.egoera = 1";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // ComboBox zutabea sortu (baldin eta existitzen ez bada)
                    if (!dgvHondatutakoak.Columns.Contains("EgoeraCombo"))
                    {
                        DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                        comboCol.HeaderText = "Egoera Berria";
                        comboCol.Name = "EgoeraCombo";
                        comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                        dgvHondatutakoak.Columns.Add(comboCol);
                    }

                    // Datuak lotu
                    dgvHondatutakoak.DataSource = dt;

                    // DISEINUA ETA ORDENA
                    dgvHondatutakoak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // ComboBox-a azken zutabera (eskuinera) mugitu
                    dgvHondatutakoak.Columns["EgoeraCombo"].DisplayIndex = dgvHondatutakoak.Columns.Count - 1;

                    // Zenbakia ezkutatu
                    dgvHondatutakoak.Columns["egoera_balioa"].Visible = false;

                    // Edizioa mugatu (ComboBox-a bakarrik utzi editagarri)
                    dgvHondatutakoak.ReadOnly = false;
                    foreach (DataGridViewColumn col in dgvHondatutakoak.Columns)
                    {
                        if (col.Name != "EgoeraCombo") col.ReadOnly = true;
                    }

                    dgvHondatutakoak.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Errorea kargatzean: " + ex.Message); }
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
