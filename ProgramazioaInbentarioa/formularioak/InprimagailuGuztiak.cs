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
            dvgInprimagailuak.DataBindingComplete += dvgInprimagailuak_DataBindingComplete;
        }

        private void InprimagailuGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
            KargatuGailuak();
        }

        private void konfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                button1.Enabled = false;
                BtnEzabatu2.Enabled = false;
                btnEgoeraAldatu.Enabled = false;
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
                dvgInprimagailuak.DataSource = null;

                // 1. Datuak lortu
                DataTable dt = DBGailuak.GetInprimagailuGuztiak();
                dvgInprimagailuak.DataSource = dt;

                // 2. EZABATU ZUTABE HUTSAK (daturik ez duten zutabeak)
                for (int i = dvgInprimagailuak.Columns.Count - 1; i >= 0; i--)
                {
                    DataGridViewColumn col = dvgInprimagailuak.Columns[i];

                    // Garrantzitsua: ComboBox oraindik ez dago, beraz zutabe originalak bakarrik
                    bool zutabeHutsa = true;

                    foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
                    {
                        if (row.IsNewRow) continue;

                        if (row.Cells[col.Index].Value != null &&
                            row.Cells[col.Index].Value != DBNull.Value &&
                            !string.IsNullOrWhiteSpace(row.Cells[col.Index].Value.ToString()))
                        {
                            zutabeHutsa = false;
                            break;
                        }
                    }

                    if (zutabeHutsa)
                    {
                        dvgInprimagailuak.Columns.RemoveAt(i);
                    }
                }

                // 3. ComboBox-a sortu (ez badago)
                if (!dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
                {
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                    comboCol.HeaderText = "Egoera";
                    comboCol.Name = "EgoeraCombo";
                    comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                    dvgInprimagailuak.Columns.Add(comboCol);
                }

                // 4. ComboBox-a eskuinean kokatzeko (azkena)
                if (dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
                {
                    dvgInprimagailuak.Columns["EgoeraCombo"].DisplayIndex = dvgInprimagailuak.ColumnCount - 1;
                }

                // 5. Zutabeak ezkutatu (beharrezkoak ez direnak)
                if (dvgInprimagailuak.Columns.Contains("id_gailua"))
                    dvgInprimagailuak.Columns["id_gailua"].Visible = false;
                if (dvgInprimagailuak.Columns.Contains("ID"))
                    dvgInprimagailuak.Columns["ID"].Visible = false;
                if (dvgInprimagailuak.Columns.Contains("egoera_balioa"))
                    dvgInprimagailuak.Columns["egoera_balioa"].Visible = false;

                // 6. ComboBox-en balioa hasieratu (DataBindingComplete baino lehen)
                foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (dvgInprimagailuak.Columns.Contains("egoera_balioa") &&
                        row.Cells["egoera_balioa"].Value != null &&
                        row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        if (egoeraIndex >= 0 && egoeraIndex <= 2)
                        {
                            row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dvgInprimagailuak.Columns["EgoeraCombo"]).Items[egoeraIndex];
                        }
                    }
                }

                // 7. KONFIGURAZIOAK
                dvgInprimagailuak.ReadOnly = false;
                dvgInprimagailuak.AllowUserToAddRows = false;    // Beheko lerro zuria kentzeko
                dvgInprimagailuak.AllowUserToDeleteRows = false;

                // 8. ComboBox zutabea BAKARRIK editagarria
                foreach (DataGridViewColumn col in dvgInprimagailuak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                // 9. Konfiguratu baimenak lerroz lerro
                KonfiguratuLerroenBaimenak();

                dvgInprimagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea kargatzean: " + ex.Message);
            }
        }

        private void KonfiguratuLerroenBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;

            foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
            {
                if (row.IsNewRow) continue;

                // ComboBox-a irakurtzeko soilik hasieran
                if (dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
                {
                    row.Cells["EgoeraCombo"].ReadOnly = true;

                    // Baimendutako erabiltzaileak bakarrik
                    if (rola == "Ikt")
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = false;
                    }
                    else if (rola == "MintegiBurua")
                    {
                        // Egiaztatu gailu hau bere mintegikoa den
                        if (dvgInprimagailuak.Columns.Contains("id_mintegia") &&
                            row.Cells["id_mintegia"].Value != null &&
                            row.Cells["id_mintegia"].Value != DBNull.Value)
                        {
                            int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                            if (gailuMintegiId == erabiltzaileMintegiId)
                            {
                                row.Cells["EgoeraCombo"].ReadOnly = false;
                            }
                        }
                    }
                }

                // Beste zelula guztiak irakurtzeko soilik
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (dvgInprimagailuak.Columns[cell.ColumnIndex].Name != "EgoeraCombo")
                    {
                        cell.ReadOnly = true;
                    }
                }
            }
        }

        private void dvgInprimagailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // ComboBox-a behar bezala konfiguratu
            if (dvgInprimagailuak.Columns.Contains("EgoeraCombo"))
            {
                foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (dvgInprimagailuak.Columns.Contains("egoera_balioa") &&
                        row.Cells["egoera_balioa"].Value != null &&
                        row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        try
                        {
                            int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                            if (egoeraIndex >= 0 && egoeraIndex <= 2)
                            {
                                row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dvgInprimagailuak.Columns["EgoeraCombo"]).Items[egoeraIndex];
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        private void dvgInprimagailuak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Koloreak ezarri egoeraren arabera
            if (e.RowIndex >= 0 && dvgInprimagailuak.Columns.Contains("egoera_balioa"))
            {
                var cellValue = dvgInprimagailuak.Rows[e.RowIndex].Cells["egoera_balioa"].Value;
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
                    dvgInprimagailuak.Rows[e.RowIndex].DefaultCellStyle.BackColor = c;
                }
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

                        // ID-a lortu (zein zutabetan dagoen egiaztatu)
                        int gailuId = 0;
                        if (dvgInprimagailuak.Columns.Contains("ID"))
                        {
                            gailuId = Convert.ToInt32(row.Cells["ID"].Value);
                        }
                        else if (dvgInprimagailuak.Columns.Contains("id_gailua"))
                        {
                            gailuId = Convert.ToInt32(row.Cells["id_gailua"].Value);
                        }

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

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgInprimagailuak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgInprimagailuak.SelectedRows[0];

                    // 1. ComboBox-etik balioa lortu (Item-a edo Value-a)
                    object comboValue = row.Cells["EgoeraCombo"].Value;
                    if (comboValue == null)
                    {
                        MessageBox.Show("Mesedez, hautatu egoera bat lehenik.");
                        return;
                    }

                    // 2. Egoera zenbakia lortu
                    int egoeraZenbakia = 0;
                    string comboText = comboValue.ToString();

                    if (comboText == "Matxuratuta") egoeraZenbakia = 1;
                    else if (comboText == "Konpontzen") egoeraZenbakia = 2;
                    else egoeraZenbakia = 0; // "Ondo"

                    // 3. POO: Objektua sortu
                    Inprimagailua inpEguneratu = new Inprimagailua();

                    // ID-a lortu
                    if (dvgInprimagailuak.Columns.Contains("ID"))
                    {
                        inpEguneratu.Id = Convert.ToInt32(row.Cells["ID"].Value);
                    }
                    else if (dvgInprimagailuak.Columns.Contains("id_gailua"))
                    {
                        inpEguneratu.Id = Convert.ToInt32(row.Cells["id_gailua"].Value);
                    }

                    inpEguneratu.Egoera = egoeraZenbakia;

                    // 4. Eguneratu
                    if (DBGailuak.EguneratuEgoeraPOO(inpEguneratu))
                    {
                        MessageBox.Show("Inprimagailuaren egoera ondo eguneratu da.");
                        KargatuGailuak();
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