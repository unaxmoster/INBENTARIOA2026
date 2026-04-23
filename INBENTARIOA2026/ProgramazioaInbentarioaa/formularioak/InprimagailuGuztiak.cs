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
    public partial class InprimagailuGuztiak : FormBase
    {
        public InprimagailuGuztiak()
        {
            InitializeComponent();
            //Zutabeak koloreztatzeko=>
            dvgInprimagailuak.CellFormatting += dvgInprimagailuak_CellFormatting;
            KargatuGailuak();
        }
        private void InprimagailuGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
        }

        private void konfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                button1.Enabled = false;
                BtnEzabatu2.Enabled = false;
                btnEgoeraAldatu.Enabled = false;
                //_______Visible erabili beharrean {.}} jarriz gero (DESABILITATU bakarrik egoten du)
            }
            else if (rola == "MintegiBurua")
            {
                button1.Visible = false;
            }
        }

        //Elementuak kodez zentratzeko modua
        private void InprimagailuGuztiak_Resize(object sender, EventArgs e)
        {
            // Titulua zentratu
            EZABATUTAKOAK.Left = (this.ClientSize.Width - EZABATUTAKOAK.Width) / 2;

            // Grid-a zentratu eta tamaina egokitu (marjinak utziz)
            dvgInprimagailuak.Width = (int)(this.ClientSize.Width * 0.8); // Pantailaren %80
            dvgInprimagailuak.Left = (this.ClientSize.Width - dvgInprimagailuak.Width) / 2;

            // Botoia zentratu
            ATZERA.Left = (this.ClientSize.Width - ATZERA.Width) / 2;
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }
        private void KargatuGailuak()
        {
            try
            {
                // 1. Datuak lortu
                DataTable dt = DBGailuak.GetInprimagailuGuztiak();
                dvgInprimagailuak.DataSource = dt;

                // 2. ComboBox-a sortu (ez badago)
                if (!dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
                {
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                    comboCol.HeaderText = "Egoera";
                    comboCol.Name = "EgoeraCombo"; // Hau da kodean erabili beharreko ID-a
                    comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                    dvgInprimagailuak.Columns.Add(comboCol);
                }

                // 3. Ordena aldatu: EgoeraCombo azkena jarri
                // Garrantzitsua: DataSource jarri ondoren egin behar da
                if (dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
                {
                    dvgInprimagailuak.Columns["EgoeraCombo"].DisplayIndex = dvgInprimagailuak.ColumnCount - 1;
                }

                // 4. Zutabeak konfiguratu (ezkutatu behar direnak)
                // Begiratu zure SQL-ak 'id_gailua' ala 'ID' itzultzen duen
                if (dvgInprimagailuak.Columns.Contains("id_gailua")) dvgInprimagailuak.Columns["id_gailua"].Visible = false;
                if (dvgInprimagailuak.Columns.Contains("ID")) dvgInprimagailuak.Columns["ID"].Visible = false;
                if (dvgInprimagailuak.Columns.Contains("egoera_balioa")) dvgInprimagailuak.Columns["egoera_balioa"].Visible = false;

                dvgInprimagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea kargatzean: " + ex.Message);
            }
        }

        private void BtnEzabatu_Click(object sender, EventArgs e)
        {
            if (dvgInprimagailuak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Mesedez, hautatu lerro bat ezabatzeko.");
                return;
            }

            using (FormMezua mezua = new FormMezua("Gailu hau ezabatu eta historikora mugitu nahi duzu?"))
            {
                if (mezua.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow row = dvgInprimagailuak.SelectedRows[0];

                        // GAKOA: Hemen 'id_gailua' jarri behar du (Datu-basean izen hori duelako)
                        // Zutabea Grid-ean ikusten ez bada ere, Index-a edo Name-a ondo egon behar da.
                        
                        int gailuId = Convert.ToInt32(row.Cells[1].Value);

                        if (DBGailuak.EzabatuInprimagailuaOsoa(gailuId))
                        {
                            MessageBox.Show("Inprimagailua ondo ezabatu da.");
                            KargatuGailuak(); // Zerrenda freskatu
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            InpBerriaSortu mintegiak = new InpBerriaSortu();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void dvgInprimagailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
            {
                foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["EgoeraCombo"];
                        if (egoeraIndex >= 0 && egoeraIndex < cell.Items.Count)
                        {
                            row.Cells["EgoeraCombo"].Value = cell.Items[egoeraIndex];
                        }
                    }
                }
            }
        }

        private void dvgInprimagailuak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Ziurtatu lerroak datuak dituela
            if (dvgInprimagailuak.Columns[e.ColumnIndex].Name == "EgoeraCombo")
            {
                // "egoera_balioa" zutabeko balioa lortu (0, 1 edo 2)
                if (dvgInprimagailuak.Rows[e.RowIndex].Cells["egoera_balioa"].Value != null)
                {
                    int egoera = Convert.ToInt32(dvgInprimagailuak.Rows[e.RowIndex].Cells["egoera_balioa"].Value);

                    // Koloreak esleitu egoeraren arabera
                    switch (egoera)
                    {
                        case 0: // ONDO
                            dvgInprimagailuak.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case 1: // MATXURATUTA
                            dvgInprimagailuak.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Salmon;
                            break;
                        case 2: // KONPONTZEN
                            dvgInprimagailuak.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                    }
                }
            }
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgInprimagailuak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgInprimagailuak.SelectedRows[0];

                    // 1. ComboBox-etik hautatutako testua jaso
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value?.ToString();

                    if (string.IsNullOrEmpty(hautatutakoEgoera))
                    {
                        MessageBox.Show("Mesedez, hautatu egoera bat lehenik.");
                        return;
                    }

                    // 2. Testua zenbakira pasatu (Ondo=0, Matxuratuta=1, Konpontzen=2)
                    int egoeraZenbakia = 0;
                    if (hautatutakoEgoera == "Matxuratuta") egoeraZenbakia = 1;
                    else if (hautatutakoEgoera == "Konpontzen") egoeraZenbakia = 2;

                    // 3. POO: Objektua sortu eta datuak esleitu
                    // Inprimagailua gailu bat denez, datu basera bidaltzeko nahikoa dugu IDa eta Egoera berria kudeatzea
                    Inprimagailua inpEguneratu = new Inprimagailua();
                    inpEguneratu.Id = Convert.ToInt32(row.Cells["ID"].Value);
                    inpEguneratu.Egoera = egoeraZenbakia;

                    // 4. Klaseari deitu objektu osoa bidaliz
                    if (DBGailuak.EguneratuEgoeraPOO(inpEguneratu))
                    {
                        MessageBox.Show("Inprimagailuaren egoera ondo eguneratu da.");
                        KargatuGailuak(); // Grid-a eta koloreak freskatu
                    }
                    else
                    {
                        MessageBox.Show("Ezin izan da egoera eguneratu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea egoera aldatzean: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Hautatu lerro bat zerrendan lehenago.");
            }
        }
    }
}
