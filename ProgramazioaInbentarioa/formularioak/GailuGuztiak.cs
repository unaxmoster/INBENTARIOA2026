using Inbentarioa.DatuBasie;
using Inventarioa.formularioak;
using Inventarioa.Objetuak;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Inbentarioa.formularioak
{
    public partial class GailuGuztiak : FormBase
    {
        public GailuGuztiak()
        {
            InitializeComponent();
            // GARRANTZITSUA: Resize egiten denean hondoa berriz margotzeko
            this.ResizeRedraw = true;
        }

        private void GailuGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
            KargatuDatuak();
        }

        private void konfiguratuBaimenak()
        {   //Enabled=>Desabilitatu bakarrik
            //Visible=> Bistatik ezkutatu
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                btnEzabatu.Enabled= false;
                btnEgoeraAldatu.Enabled = false;
                btnBerriaSortu.Enabled = false; 
                //_______Visible erabili beharrean {.Enabled}} jarriz gero (DESABILITATU bakarrik egoten du)
            }
            else if (rola == "MintegiBurua")
            {
                btnBerriaSortu.Visible = false;
            }
        }

        private void KargatuDatuak()
        {
            try
            {
                DataTable dt = DBGailuak.GetGailuGuztiak();
                dvgGailuak.DataSource = dt;
                //__  Zutabeak ez modifikatu ahal izateko __
                // Zutabe zehatzak irakurtzeko soilik jarri (ezin dira editatu)
                // 1. Grid osoa blokeatu
                dvgGailuak.ReadOnly = false; // Gurasoa false egon behar da umeak aldatu ahal izateko
                dvgGailuak.AllowUserToAddRows = false;    // Beheko lerro zuria kentzeko
                dvgGailuak.AllowUserToDeleteRows = false; // 'Supr' sakatzean ez ezabatzeko
                //_______________________________
                //_______________________________

                // 1. Egoera testu-zutabea ezkutatu
                if (dvgGailuak.Columns.Contains("Egoera")) dvgGailuak.Columns["Egoera"].Visible = false;
                if (dvgGailuak.Columns.Contains("egoera_balioa")) dvgGailuak.Columns["egoera_balioa"].Visible = false;
                if (dvgGailuak.Columns.Contains("ID")) dvgGailuak.Columns["ID"].Visible = false;

                // 2. ComboBox zutabea sortu (dagoeneko ez badago)
                if (!dvgGailuak.Columns.Contains("EgoeraCombo"))
                {
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                    comboCol.HeaderText = "Egoera Aldatu";
                    comboCol.Name = "EgoeraCombo";

                    // Aukerak gehitu (Datu-baseko ordena berean: 0, 1, 2)
                    comboCol.Items.Add("Ondo");      // Index 0
                    comboCol.Items.Add("Hondatuta"); // Index 1
                    comboCol.Items.Add("Konpontzen"); // Index 2

                    dvgGailuak.Columns.Add(comboCol);
                }

                // 3. ComboBox-aren balioa hasieratu datu-baseko balioarekin
                foreach (DataGridViewRow row in dvgGailuak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dvgGailuak.Columns["EgoeraCombo"]).Items[egoeraIndex];
                    }
                }

                dvgGailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Errorea kargatzean: " + ex.Message); }
        }

        private void btnEzabatu_Click(object sender, EventArgs e)
        {
            if (dvgGailuak.SelectedRows.Count > 0)
            {
                using (FormMezua mezua = new FormMezua("Gailu hau historikora mugitu nahi duzu?"))
                {
                    if (mezua.ShowDialog() == DialogResult.Yes)
                    {
                        try
                        {
                            DataGridViewRow row = dvgGailuak.SelectedRows[0];
                            int id = Convert.ToInt32(row.Cells["ID"].Value);
                            string mota = row.Cells["mota"].Value.ToString();
                            bool emaitza = false;

                            // Mota bakoitzaren arabera, bere objektua sortu eta metodoari deitu
                            if (mota == "Inprimagailua")
                            {
                                Inprimagailua inp = new Inprimagailua();
                                inp.Id = id;
                                inp.IdentifikazioKodea = row.Cells["identifikazio_kodea"].Value.ToString();
                                // Kontuz: Grid-ean 'izena' baduzu 'MarkaModeloa'ren ordez, egokitu:
                                inp.MarkaModeloa = row.Cells["marka_modeloa"].Value.ToString();

                                emaitza = DBGailuak.EzabatuInprimagailuaPOO(inp);
                            }
                            else if (mota == "Ordenagailua")
                            {
                                // Zure eraikitzailea erabiliz: (kodea, modeloa, mintegia, ram, rom, cpu)
                                // Grid-eko zutabe izenak zure SQL-ko "AS" izenekin bat etorri behar dira
                                string kodea = row.Cells["identifikazio_kodea"].Value.ToString();
                                string modeloa = row.Cells["marka_modeloa"].Value.ToString(); // SQL-an 'izena' jarri duzu
                                int mintegia = 0; // Grid-ean id_mintegia ez baduzu, 0 jarri daiteke ezabatzeko soilik bada

                                Ordenagailua ord = new Ordenagailua(kodea, modeloa, mintegia, "", "", "");
                                ord.Id = id; // IDa Gailua klasetik dator

                                emaitza = DBGailuak.EzabatuOrdenagailuaPOO(ord);
                            }

                            if (emaitza)
                            {
                                MessageBox.Show("Gailua ondo ezabatu eta historikora mugitu da.");
                                KargatuDatuak();
                            }
                            else
                            {
                                MessageBox.Show("Ezin izan da gailua ezabatu.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Errorea prozesuan: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgGailuak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgGailuak.SelectedRows[0];

                    // 1. Datuak jaso
                    int idGailua = Convert.ToInt32(row.Cells["ID"].Value);
                    string aukeratutakoEgoera = row.Cells["EgoeraCombo"].Value.ToString();

                    // 2. Zenbakira bihurtu
                    int egoeraBerria = 0;
                    if (aukeratutakoEgoera == "Hondatuta") egoeraBerria = 1;
                    else if (aukeratutakoEgoera == "Konpontzen") egoeraBerria = 2;

                    // 3. POO: Gailu objektu bat prestatu
                    Gailua gailua = new Gailua();
                    gailua.Id = idGailua;
                    gailua.Egoera = egoeraBerria;

                    // 4. Klaseari deitu
                    if (DBGailuak.EguneratuEgoeraPOO(gailua))
                    {
                        MessageBox.Show("Egoera ondo eguneratu da.");
                        KargatuDatuak(); // Taula freskatu kolore berriak ikusteko
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea eguneratzean: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Mesedez, hautatu lerro bat lehenik.");
            }
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI ikusi = new IKUSI();
            ikusi.ShowDialog();
            this.Close();
        }
        public static DataTable GetGailuGuztiak()
        {
            DataTable dt = new DataTable();
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            // SQL honek 'mota' zutabea sortzen du unean bertan
            string sql = @"
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.izena, 'Ordenagailua' AS mota 
        FROM gailuak g 
        INNER JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
        UNION
        SELECT g.id_gailua AS ID, g.identifikazio_kodea, g.izena, 'Inprimagailua' AS mota 
        FROM gailuak g 
        INNER JOIN inprimagailuak i ON g.id_gailua = i.id_gailua";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // Errorea kudeatu
            }
            return dt;
        }

        private void dvgGailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dvgGailuak.Rows)
            {
                if (row.Cells["egoera_balioa"].Value != null)
                {
                    int balioa = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                    if (balioa == 0) row.DefaultCellStyle.BackColor = Color.LightGreen; // Ondo
                    if (balioa == 1) row.DefaultCellStyle.BackColor = Color.LightCoral; // Hondatuta
                    if (balioa == 2) row.DefaultCellStyle.BackColor = Color.LightYellow; // Konpontzen
                }
            }
        }

        private void btnBerriaSortu_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdInp ikusi = new OrdInp();
            ikusi.ShowDialog();
            this.Close();
        }
    }
}