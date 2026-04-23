using Inbentarioa;
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
    public partial class MintegiGuztiak : FormBase
    {
        public MintegiGuztiak()
        {
            InitializeComponent();
            //Zutabeak koloreztatu=>
            dvgMintegika.CellFormatting += dvgMintegika_CellFormatting;
            KargatuMintegiakCombo(); // ComboBox-a betetzen du
            KargatuGailuak(0);       // Hasieran denak erakusten ditu
        }


        private void MintegiGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            KonfiguratuBaimenak();
        }

        private void KonfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                button1.Enabled = false;
                BtnEzabatu.Enabled = false;
                btnEgoeraAldatu.Enabled = false;
                //_______Visible erabili beharrean {enabled}} jarriz gero (DESABILITATU bakarrik egiten du)
            }
            else if (rola == "MintegiBurua")
            {
                button1.Enabled = false;
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

                    // SQL: G.egoera zuzenean hartzen dugu (0, 1, 2), ez testu bezala
                    string query = "SELECT G.id_gailua AS 'ID', " +  // GEHITU HAU!
                                   "G.identifikazio_kodea AS 'Kodea', " +
                                   "CASE " +
                                   "  WHEN O.id_gailua IS NOT NULL THEN 'Ordenagailua' " +
                                   "  WHEN I.id_gailua IS NOT NULL THEN 'Inprimagailua' " +
                                   "  ELSE 'Besterik' " +
                                   "END AS 'Gailu mota', " +
                                   "G.marka_modeloa AS 'Modeloa', " +
                                   "M.izena AS 'Mintegia', G.eroste_data AS 'Data', " +
                                   "G.egoera AS 'egoera_balioa' " +
                                   "FROM Gailuak G " +
                                   "JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia " +
                                   "LEFT JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua " +
                                   "LEFT JOIN Inprimagailuak I ON G.id_gailua = I.id_gailua";

                    if (idMintegia > 0) query += " WHERE G.id_mintegia = @idMintegia";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    if (idMintegia > 0) cmd.Parameters.AddWithValue("@idMintegia", idMintegia);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // 1. ComboBox zutabea sortu (ez bada existitzen)
                    if (!dvgMintegika.Columns.Contains("EgoeraCombo"))
                    {
                        DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                        comboCol.HeaderText = "Egoera";
                        comboCol.Name = "EgoeraCombo";
                        comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                        dvgMintegika.Columns.Add(comboCol);
                    }

                    dvgMintegika.DataSource = dt;

                    // 2. DISEINUA: Zabalera osoa
                    dvgMintegika.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dvgMintegika.Columns["EgoeraCombo"].DisplayIndex = dvgMintegika.Columns.Count - 1;

                    // Ezkutatu egoera_balioa (lehendik zenuen)
                    dvgMintegika.Columns["egoera_balioa"].Visible = false;

                    // GAKOA: Ezkutatu ID zutabea
                    // Honela, Grid-ean existitzen da (balioa har dezakezu), baina erabiltzaileak ez du ikusten
                    if (dvgMintegika.Columns.Contains("ID"))
                    {
                        dvgMintegika.Columns["ID"].Visible = false;
                    }

                    // 3. Edizio baimenak eta babesa
                    dvgMintegika.ReadOnly = false;
                    foreach (DataGridViewColumn col in dvgMintegika.Columns)
                    {
                        if (col.Name != "EgoeraCombo") col.ReadOnly = true;
                    }

                    // ID-a txikiago mantendu
                    if (dvgMintegika.Columns.Contains("ID"))
                    {
                        dvgMintegika.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dvgMintegika.Columns["ID"].Width = 45;
                    }

                    dvgMintegika.AllowUserToAddRows = false;

                    // ComboBox-eko balioak hasieratu kargatutako datuekin
                    KargatuEgoeraBalioakGrid();
                }
            }
            catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
        }
        private void KargatuEgoeraBalioakGrid()
        {
            foreach (DataGridViewRow row in dvgMintegika.Rows)
            {
                if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                {
                    int index = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["EgoeraCombo"];
                    if (index >= 0 && index < cell.Items.Count)
                    {
                        row.Cells["EgoeraCombo"].Value = cell.Items[index];
                    }
                }
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
                DataGridViewRow row = dvgMintegika.SelectedRows[0];

                using (FormMezua mezua = new FormMezua("Gailu hau historikora mugitu nahi duzu?"))
                {
                    if (mezua.ShowDialog() == DialogResult.Yes)
                    {
                        try
                        {
                            // Gailu motaren arabera objektu bat edo bestea sortu
                            string gailuMota = row.Cells["Gailu mota"].Value.ToString();
                            bool emaitza = false;

                            if (gailuMota == "Inprimagailua")
                            {
                                Inprimagailua inp = new Inprimagailua();
                                inp.Id = Convert.ToInt32(row.Cells["ID"].Value);
                                inp.IdentifikazioKodea = row.Cells["Kodea"].Value.ToString();
                                inp.MarkaModeloa = row.Cells["Modeloa"].Value.ToString();

                                emaitza = DBGailuak.EzabatuInprimagailuaPOO(inp);
                            }
                            else if (gailuMota == "Ordenagailua")
                            {
                                // Suposatuz OrdenagailuaPOO metodoa daukazula
                                // emaitza = DBGailuak.EzabatuOrdenagailuaPOO(ord);
                                // Momentuz lehengoan utzi dezakezu edo antzeko logika aplikatu
                            }

                            if (emaitza)
                            {
                                MessageBox.Show("Gailua ondo mugitu da historikora.");
                                KargatuGailuak(Convert.ToInt32(cbMintegiak.SelectedValue));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Errorea ezabatzean: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgMintegika.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgMintegika.SelectedRows[0];

                    // 1. Balioak jaso grid-etik
                    int idGailua = Convert.ToInt32(row.Cells["ID"].Value);
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value?.ToString();

                    if (string.IsNullOrEmpty(hautatutakoEgoera)) return;

                    // 2. Testua zenbakira pasatu
                    int egoeraZenbakia = (hautatutakoEgoera == "Matxuratuta") ? 1 : (hautatutakoEgoera == "Konpontzen" ? 2 : 0);

                    // 3. POO: Gailu objektu bat sortu (identifikatzeko nahikoa dugu IDa eta Egoera)
                    // Kasu honetan Gailua klasea erabili dezakegu zuzenean oinarrizko datuak gorde behar ditugulako soilik
                    Gailua gailuEguneratua = new Gailua();
                    gailuEguneratua.Id = idGailua;
                    gailuEguneratua.Egoera = egoeraZenbakia;

                    // 4. Deitu lehenago sortutako metodoari
                    if (DBGailuak.EguneratuEgoeraPOO(gailuEguneratua))
                    {
                        MessageBox.Show("Egoera ondo eguneratu da.");

                        // Grid-a freskatu koloreak eta datuak eguneratzeko
                        if (cbMintegiak.SelectedValue != null && int.TryParse(cbMintegiak.SelectedValue.ToString(), out int idHautatua))
                        {
                            KargatuGailuak(idHautatua);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea egoera aldatzean: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Hautatu lerro bat lehenik.");
            }
        }

        private void dvgMintegika_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 1. Segurtasun muga: Hautatutako zutabea ez bada Combo-a, ezin da editatu
            if (dvgMintegika.Columns[e.ColumnIndex].Name != "EgoeraCombo")
            {
                e.Cancel = true;
                return;
            }

            // 2. ID-a nulua bada (lerro hutsa bada), ezin da editatu
            var idBalioa = dvgMintegika.Rows[e.RowIndex].Cells["ID"].Value;
            if (idBalioa == null || idBalioa == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

            private void dvgMintegika_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            KargatuEgoeraBalioakGrid();
        }
        private void dvgMintegika_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Bakarrik zutabe bat prozesatzen dugunean (optimizazioa)
            if (dvgMintegika.Columns[e.ColumnIndex].Name == "EgoeraCombo")
            {
                // Ziurtatu lerroak datuak dituela
                if (dvgMintegika.Rows[e.RowIndex].Cells["egoera_balioa"].Value != null &&
                    dvgMintegika.Rows[e.RowIndex].Cells["egoera_balioa"].Value != DBNull.Value)
                {
                    int egoera = Convert.ToInt32(dvgMintegika.Rows[e.RowIndex].Cells["egoera_balioa"].Value);

                    // Egoeraren arabera lerro osoaren atzeko kolorea aldatu
                    switch (egoera)
                    {
                        case 0: // ONDO
                            dvgMintegika.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case 1: // MATXURATUTA
                            dvgMintegika.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
                            break;
                        case 2: // KONPONTZEN
                            dvgMintegika.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                    }
                }
            }
        }
    }
}
