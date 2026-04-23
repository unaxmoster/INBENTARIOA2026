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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventarioa.formularioak
{
    public partial class OrdenagailuGuztiak : FormBase
    {
        public OrdenagailuGuztiak()
        {
            InitializeComponent();
            // Bi hauek ezinbestekoak dira
            dvgOrdenagailuak.CellFormatting += dvgOrdenagailuak_CellFormatting;
            dvgOrdenagailuak.DataBindingComplete += dvgOrdenagailuak_DataBindingComplete; // Lerro hau gehitu!

        }
        private void OrdenagailuGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
            KargatuDatuak();
        }

        private void konfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                btnBerriaSortuOr.Enabled = false;
                BtnEzabatuOr.Enabled = false;
                btnBerriaSortuOr.Enabled = false;
                btnEgoeraAldatuOr.Enabled = false;
            }
            else if (rola == "MintegiBurua")
            {
                btnBerriaSortuOr.Enabled = false;
            }
        }
        private void btnAtzeraOr_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void KargatuDatuak()
        {
            try
            {
                // 1. GARBITU lehendik dauden zutabeak (badaezpada)
                dvgOrdenagailuak.DataSource = null;

                // 2. SORTU ComboBox-a datuak kargatu AURRETIK
                if (!dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                {
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                    comboCol.HeaderText = "Egoera";
                    comboCol.Name = "EgoeraCombo";
                    comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                    dvgOrdenagailuak.Columns.Add(comboCol);
                }

                // 3. ORAIN kargatu datuak
                DataTable dt = DBGailuak.GetOrdenagailuGuztiak();
                dvgOrdenagailuak.DataSource = dt;

                // 4. KONFIGURATU zutabeak (Ezkutatu ID eta egoera_balioa)
                if (dvgOrdenagailuak.Columns.Contains("ID")) dvgOrdenagailuak.Columns["ID"].Visible = false;
                if (dvgOrdenagailuak.Columns.Contains("egoera_balioa")) dvgOrdenagailuak.Columns["egoera_balioa"].Visible = false;

                dvgOrdenagailuak.Columns["EgoeraCombo"].DisplayIndex = dvgOrdenagailuak.Columns.Count - 1;

                // Diseinua
                dvgOrdenagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dvgOrdenagailuak.ReadOnly = false;
                foreach (DataGridViewColumn col in dvgOrdenagailuak.Columns)
                {
                    if (col.Name != "EgoeraCombo") col.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea kargatzean: " + ex.Message);
            }
        }

        // KOLOREAK ETA EGOERA TESTUA (POO erara)
        private void dvgOrdenagailuak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dvgOrdenagailuak.Columns.Contains("egoera_balioa"))
            {
                var cellValue = dvgOrdenagailuak.Rows[e.RowIndex].Cells["egoera_balioa"].Value;
                if (cellValue != null && cellValue != DBNull.Value)
                {
                    int egoera = Convert.ToInt32(cellValue);
                    Color c = Color.White;
                    switch (egoera)
                    {
                        case 0: c = Color.LightGreen; break;
                        case 1: c = Color.Salmon; break;
                        case 2: c = Color.LightYellow; break;
                    }
                    dvgOrdenagailuak.Rows[e.RowIndex].DefaultCellStyle.BackColor = c;
                }
            }
        }

        private void btnEgoeraAldatuOr_Click(object sender, EventArgs e)
        {
            if (dvgOrdenagailuak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgOrdenagailuak.SelectedRows[0];

                    // 1. ComboBox-etik aukeratutako testua hartu
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value?.ToString();
                    if (string.IsNullOrEmpty(hautatutakoEgoera)) return;

                    // 2. Testua zenbaki (int) bihurtu
                    int egoeraZenbakia = 0; // Ondo
                    if (hautatutakoEgoera == "Matxuratuta") egoeraZenbakia = 1;
                    else if (hautatutakoEgoera == "Konpontzen") egoeraZenbakia = 2;

                    // 3. OBJEKTUA SORTU (Egoera berriarekin)
                    // Eraikitzaileak datu asko eskatzen badizkizu, garrantzitsuenak IDa eta Egoera dira kasu honetan
                    Ordenagailua ordeEguneratu = new Ordenagailua(
                        row.Cells["Kodea"].Value.ToString(),
                        row.Cells["Modeloa"].Value.ToString(),
                        0, "", "", "" // Beste datuak ez dira beharrezkoak UPDATE honetarako
                    );

                    ordeEguneratu.Id = Convert.ToInt32(row.Cells["ID"].Value);
                    ordeEguneratu.Egoera = egoeraZenbakia; // Hau da aldatuko dugun balioa

                    // 4. DBGailuak klaseari deitu objektua bidaliz
                    if (DBGailuak.EguneratuEgoeraPOO(ordeEguneratu))
                    {
                        MessageBox.Show($"Egoera ondo eguneratu da: {hautatutakoEgoera}");
                        KargatuDatuak(); // Grid-a freskatu koloreak ikusteko
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea egoera aldatzean: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Mesedez, hautatu lerro bat lehenago.");
            }
        }

        private void BtnEzabatuOr_Click(object sender, EventArgs e)
        {
            // 1. Egiaztatu lerro bat hautatuta dagoela
            if (dvgOrdenagailuak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Mesedez, hautatu ordenagailu bat ezabatzeko.");
                return;
            }

            // 2. Berrespena eskatu
            using (FormMezua mezua = new FormMezua("Ordenagailu hau historikora mugitu nahi duzu?"))
            {
                if (mezua.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        // 3. Grid-etik hautatutako lerroa hartu
                        DataGridViewRow row = dvgOrdenagailuak.SelectedRows[0];

                        // 4. OBJEKTUA SORTU (Datuak grid-etik ateraz)
                        // Oharra: Ordenagailua eraikitzaileak (kodea, modeloa, mintegia, ram, rom, cpu) behar ditu
                        // Gure Grid-ean agian datu guztiak ez daude, baina ID-a eta oinarrizkoak bai.

                        Ordenagailua ordeEzabatu = new Ordenagailua(
                            row.Cells["Kodea"].Value.ToString(),
                            row.Cells["Modeloa"].Value.ToString(),
                            0, // Mintegia (ezabatzeko ez dugu behar)
                            row.Cells["ram"].Value.ToString(),
                            row.Cells["rom"].Value.ToString(),
                            row.Cells["cpu"].Value.ToString()
                        );

                        // GARRANTZITSUA: ID-a esleitu, DBak jakiteko zein ezabatu
                        ordeEzabatu.Id = Convert.ToInt32(row.Cells["ID"].Value);

                        // 5. DEITU POO METODOARI
                        if (DBGailuak.EzabatuOrdenagailuaPOO(ordeEzabatu))
                        {
                            MessageBox.Show("Ordenagailua ondo ezabatu eta historikora mugitu da.");
                            KargatuDatuak(); // Grid-a freskatu
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea ezabatzean: " + ex.Message);
                    }
                }
            }
        }



        private void btnBerriaSortuOr_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdeBerriaSortu mintegiak = new OrdeBerriaSortu();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void dvgOrdenagailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // ZIURTATU zutabea existitzen dela errorea ez emateko
            if (dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
            {
                foreach (DataGridViewRow row in dvgOrdenagailuak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int balioa = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        if (balioa >= 0 && balioa <= 2) // 0, 1, 2 badira
                        {
                            row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxCell)row.Cells["EgoeraCombo"]).Items[balioa];
                        }
                    }
                }
            }
        }
    }

}
