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
            dvgInprimagailuak.CellFormatting += dvgInprimagailuak_CellFormatting;
            dvgInprimagailuak.DataBindingComplete += dvgInprimagailuak_DataBindingComplete;
            dvgInprimagailuak.CellBeginEdit += dvgInprimagailuak_CellBeginEdit;
        }

        private void InprimagailuGuztiak_Load(object sender, EventArgs e)
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
                button1.Enabled = false;
                BtnEzabatu2.Enabled = false;
                btnEgoeraAldatu.Enabled = false;
            }
            else if (rola == "MintegiBurua")
            {
                button1.Enabled = false;
                BtnEzabatu2.Enabled = false;
            }
        }

        private void ATZERA_Click(object sender, EventArgs e)
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
                // GARRANTZITSUA: Lehenik datuak garbitu
                dvgInprimagailuak.DataSource = null;
                dvgInprimagailuak.Columns.Clear();  // HAU GEHITU - zutabeak garbitzeko

                // Datu-baseko inprimagailu datuak kargatu
                DataTable dt = DBGailuak.GetInprimagailuGuztiak();
                dvgInprimagailuak.DataSource = dt;

                // Zutabeak ezkutatu
                if (dvgInprimagailuak.Columns.Contains("ID"))
                    dvgInprimagailuak.Columns["ID"].Visible = false;
                if (dvgInprimagailuak.Columns.Contains("egoera_balioa"))
                    dvgInprimagailuak.Columns["egoera_balioa"].Visible = false;
                if (dvgInprimagailuak.Columns.Contains("id_mintegia"))
                    dvgInprimagailuak.Columns["id_mintegia"].Visible = false;

                // ComboBox zutabea sortu
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.HeaderText = "Egoera Berria";
                comboCol.Name = "EgoeraCombo";
                comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                dvgInprimagailuak.Columns.Add(comboCol);

                // ComboBox eskuinean kokatu
                dvgInprimagailuak.Columns["EgoeraCombo"].DisplayIndex = dvgInprimagailuak.Columns.Count - 1;

                // ComboBox balioa hasieratu
                foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        string egoeraTestua = "";
                        switch (egoeraIndex)
                        {
                            case 0: egoeraTestua = "Ondo"; break;
                            case 1: egoeraTestua = "Matxuratuta"; break;
                            case 2: egoeraTestua = "Konpontzen"; break;
                        }
                        row.Cells["EgoeraCombo"].Value = egoeraTestua;
                    }
                }

                // Konfigurazioak
                dvgInprimagailuak.ReadOnly = false;
                dvgInprimagailuak.AllowUserToAddRows = false;
                dvgInprimagailuak.AllowUserToDeleteRows = false;

                // ComboBox bakarra editagarri
                foreach (DataGridViewColumn col in dvgInprimagailuak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                dvgInprimagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Baimenak konfiguratu
                KonfiguratuLerroenBaimenak();

                // Behartu birmarrazketa
                dvgInprimagailuak.Invalidate();
                dvgInprimagailuak.Refresh();
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

            bool idMintegiaExistitzenDa = dvgInprimagailuak.Columns.Contains("id_mintegia");
            bool egoeraComboExistitzenDa = dvgInprimagailuak.Columns.Contains("EgoeraCombo");

            if (!egoeraComboExistitzenDa) return;

            foreach (DataGridViewRow row in dvgInprimagailuak.Rows)
            {
                if (row.IsNewRow) continue;

                // Lehenespenez, ezin editatu
                if (row.Cells["EgoeraCombo"] != null)
                {
                    row.Cells["EgoeraCombo"].ReadOnly = true;
                }

                if (rola == "Ikt")
                {
                    // IKT-ak denak editatu ditzake
                    if (row.Cells["EgoeraCombo"] != null)
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = false;
                    }
                }
                else if (rola == "MintegiBurua")
                {
                    // MintegiBurua: bere mintegikoak bakarrik
                    if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                    {
                        int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                        if (gailuMintegiId == erabiltzaileMintegiId)
                        {
                            if (row.Cells["EgoeraCombo"] != null)
                            {
                                row.Cells["EgoeraCombo"].ReadOnly = false;
                            }
                        }
                    }
                }
                // Irakaslea: ezin editatu (beraz, ReadOnly = true mantentzen da)
            }
        }

        private void dvgInprimagailuak_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dvgInprimagailuak.Columns[e.ColumnIndex].Name != "EgoeraCombo")
            {
                e.Cancel = true;
            }
        }

        private void dvgInprimagailuak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dvgInprimagailuak.Rows[e.RowIndex];
            string rola = Sarrera.Saioa.Rola;

            // 1. KOLOREA EZARRI ROLAREN ARABERA
            if (rola == "Ikt")
            {
                // Ikt: egoeraren kolorea
                if (dvgInprimagailuak.Columns.Contains("egoera_balioa"))
                {
                    var egoeraValue = row.Cells["egoera_balioa"].Value;
                    if (egoeraValue != null && egoeraValue != DBNull.Value)
                    {
                        int egoera = Convert.ToInt32(egoeraValue);
                        switch (egoera)
                        {
                            case 0: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                            case 1: row.DefaultCellStyle.BackColor = Color.Salmon; break;
                            case 2: row.DefaultCellStyle.BackColor = Color.LightYellow; break;
                            default: row.DefaultCellStyle.BackColor = Color.White; break;
                        }
                    }
                }
            }
            else if (rola == "MintegiBurua")
            {
                bool idMintegiaExistitzenDa = dvgInprimagailuak.Columns.Contains("id_mintegia");
                bool bereMintegikoa = false;

                if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                {
                    int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                    int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;
                    bereMintegikoa = (gailuMintegiId == erabiltzaileMintegiId);
                }

                if (bereMintegikoa)
                {
                    // Bere mintegiko inprimagailuak: egoeraren kolorea
                    if (dvgInprimagailuak.Columns.Contains("egoera_balioa"))
                    {
                        var egoeraValue = row.Cells["egoera_balioa"].Value;
                        if (egoeraValue != null && egoeraValue != DBNull.Value)
                        {
                            int egoera = Convert.ToInt32(egoeraValue);
                            switch (egoera)
                            {
                                case 0: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                                case 1: row.DefaultCellStyle.BackColor = Color.Salmon; break;
                                case 2: row.DefaultCellStyle.BackColor = Color.LightYellow; break;
                                default: row.DefaultCellStyle.BackColor = Color.White; break;
                            }
                        }
                    }
                }
                else
                {
                    // Beste mintegiko inprimagailuak: gris argia
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            else // Irakaslea
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            // Zutabe hutsak
            if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.LightGray;
                e.CellStyle.Font = new Font(dvgInprimagailuak.Font, FontStyle.Italic);
                e.Value = "(hutsik)";
                e.FormattingApplied = true;
            }
        }

        private void dvgInprimagailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Datuen lotura amaitzean, freskatu
            dvgInprimagailuak.Invalidate();
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

                            int gailuId = Convert.ToInt32(row.Cells["ID"].Value);

                            if (DBGailuak.EzabatuInprimagailuaOsoa(gailuId))
                            {
                                MessageBox.Show("Inprimagailua ondo ezabatu da.");
                                KargatuDatuak();
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
            InpBerriaSortu inprimagailuBerria = new InpBerriaSortu();
            inprimagailuBerria.ShowDialog();
            this.Close();
        }


        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgInprimagailuak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgInprimagailuak.SelectedRows[0];

                    // Egiaztatu ComboBox editagarria den (baimenak)
                    if (row.Cells["EgoeraCombo"].ReadOnly)
                    {
                        MessageBox.Show("Ez duzu baimenik gailu honen egoera aldatzeko (ez da zure mintegikoa).");
                        return;
                    }

                    if (row.Cells["EgoeraCombo"].Value == null)
                    {
                        MessageBox.Show("Aukeratu egoera bat lehenik.");
                        return;
                    }

                    string egoeraTestua = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraBerria;
                    int egoeraZaharra = Convert.ToInt32(row.Cells["egoera_balioa"].Value);

                    switch (egoeraTestua)
                    {
                        case "Ondo": egoeraBerria = 0; break;
                        case "Matxuratuta": egoeraBerria = 1; break;
                        case "Konpontzen": egoeraBerria = 2; break;
                        default:
                            MessageBox.Show("Egoera balio ezezaguna: " + egoeraTestua);
                            return;
                    }

                    // Egoera berdina bada, ez egin ezer
                    if (egoeraZaharra == egoeraBerria)
                    {
                        MessageBox.Show("Egoera berdina da. Ez da eguneratu behar.");
                        return;
                    }

                    int gailuId = Convert.ToInt32(row.Cells["ID"].Value);

                    // ***** GARRANTZITSUA: Egoera "Matxuratuta" (1) bada, gorde hondatutakoak taulan *****
                    if (egoeraBerria == 1)
                    {
                        // Egiaztatu lehen ez zegoen Matxuratuta
                        if (egoeraZaharra != 1)
                        {
                            bool gordeOndo = DBGailuak.GordeHondatutakoGailua(gailuId, "Inprimagailua");
                            if (!gordeOndo)
                            {
                                MessageBox.Show("Arazoa gertatu da hondatutako gailua gordetzean.");
                                return;
                            }
                        }
                    }

                    // Gailu objektua sortu (Gailua klasea erabiliz)
                    Gailua gailua = new Gailua();
                    gailua.Id = gailuId;
                    gailua.Egoera = egoeraBerria;

                    // Eguneratu egoera
                    if (DBGailuak.EguneratuEgoeraPOO(gailua))
                    {
                        MessageBox.Show($"Egoera ondo eguneratu da: {egoeraTestua}");
                        KargatuDatuak();
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
                MessageBox.Show("Mesedez, hautatu lerro bat lehenago.");
            }
        }
    }
}