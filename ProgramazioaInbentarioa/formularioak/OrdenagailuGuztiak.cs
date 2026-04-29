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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inventarioa.formularioak
{
    public partial class OrdenagailuGuztiak : FormBase
    {
        // Aldagai globala ComboBox-en iturbururako
        private DataTable egoeraIturburua;

        public OrdenagailuGuztiak()
        {
            InitializeComponent();
            dvgOrdenagailuak.CellFormatting += dvgOrdenagailuak_CellFormatting;
            dvgOrdenagailuak.DataBindingComplete += dvgOrdenagailuak_DataBindingComplete;
            dvgOrdenagailuak.CellBeginEdit += dvgOrdenagailuak_CellBeginEdit; // Gehitu hau
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
                btnEgoeraAldatuOr.Enabled = false;
            }
            else if (rola == "MintegiBurua")
            {
                btnBerriaSortuOr.Enabled = false;
                BtnEzabatuOr.Enabled = false;
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
                // GARRANTZITSUA: Lehenik datuak garbitu
                dvgOrdenagailuak.DataSource = null;
                dvgOrdenagailuak.Columns.Clear();  // HAU GEHITU - zutabeak garbitzeko

                // Datu-baseko datuak kargatu
                DataTable dt = DBGailuak.GetOrdenagailuGuztiak();
                dvgOrdenagailuak.DataSource = dt;

                // Zutabeak ezkutatu
                if (dvgOrdenagailuak.Columns.Contains("ID"))
                    dvgOrdenagailuak.Columns["ID"].Visible = false;
                if (dvgOrdenagailuak.Columns.Contains("egoera_balioa"))
                    dvgOrdenagailuak.Columns["egoera_balioa"].Visible = false;
                if (dvgOrdenagailuak.Columns.Contains("id_mintegia"))
                    dvgOrdenagailuak.Columns["id_mintegia"].Visible = false;

                // ComboBox zutabea sortu (beti sortu berria, lehenik ez zegoelako)
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.HeaderText = "Egoera Berria";
                comboCol.Name = "EgoeraCombo";
                comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                dvgOrdenagailuak.Columns.Add(comboCol);

                // ComboBox-a eskuinean kokatzeko
                dvgOrdenagailuak.Columns["EgoeraCombo"].DisplayIndex = dvgOrdenagailuak.Columns.Count - 1;

                // ComboBox-aren balioa hasieratu
                foreach (DataGridViewRow row in dvgOrdenagailuak.Rows)
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
                dvgOrdenagailuak.ReadOnly = false;
                dvgOrdenagailuak.AllowUserToAddRows = false;
                dvgOrdenagailuak.AllowUserToDeleteRows = false;

                // ComboBox zutabea BAKARRIK editagarria
                foreach (DataGridViewColumn col in dvgOrdenagailuak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                dvgOrdenagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Baimenak konfiguratu
                KonfiguratuLerroenBaimenak();

                // Behartu birmarrazketa
                dvgOrdenagailuak.Invalidate();
                dvgOrdenagailuak.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea kargatzean: " + ex.Message);
            }
        }

        private void EzabatuZutabeHutsak()
        {
            // Atzetik aurrera ibili zutabeak ezabatzeko (indizeak ez aldatzeko)
            for (int i = dvgOrdenagailuak.Columns.Count - 1; i >= 0; i--)
            {
                DataGridViewColumn col = dvgOrdenagailuak.Columns[i];
        
                // EgoeraCombo zutabea ez ezabatu
                if (col.Name == "EgoeraCombo") continue;
        
                // Zutabe guztiak egiaztatu baliorik ote duten
                bool zutabeHutsa = true;
        
                foreach (DataGridViewRow row in dvgOrdenagailuak.Rows)
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
                    dvgOrdenagailuak.Columns.RemoveAt(i);
                }
            }
        }
        private void KonfiguratuLerroenBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;

            bool idMintegiaExistitzenDa = dvgOrdenagailuak.Columns.Contains("id_mintegia");
            bool egoeraComboExistitzenDa = dvgOrdenagailuak.Columns.Contains("EgoeraCombo");

            if (!egoeraComboExistitzenDa) return;

            foreach (DataGridViewRow row in dvgOrdenagailuak.Rows)
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

        // ZELULA EDITATZEN HASI AURETIK - ComboBox ez denetan editatzea saihestu
        private void dvgOrdenagailuak_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // ComboBox ez den zutabe bat editatu nahi bada, editazioa utzi
            if (dvgOrdenagailuak.Columns[e.ColumnIndex].Name != "EgoeraCombo")
            {
                e.Cancel = true;
            }
        }

        private void dvgOrdenagailuak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dvgOrdenagailuak.Rows[e.RowIndex];
            string rola = Sarrera.Saioa.Rola;

            // 1. KOLOREA EZARRI ROLAREN ARABERA
            if (rola == "Ikt")
            {
                // Ikt: egoeraren kolorea aplikatzen dugu
                if (dvgOrdenagailuak.Columns.Contains("egoera_balioa"))
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
                bool idMintegiaExistitzenDa = dvgOrdenagailuak.Columns.Contains("id_mintegia");
                bool bereMintegikoa = false;

                if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                {
                    int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                    int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;
                    bereMintegikoa = (gailuMintegiId == erabiltzaileMintegiId);
                }

                if (bereMintegikoa)
                {
                    // Bere mintegiko gailuak: egoeraren kolorea
                    if (dvgOrdenagailuak.Columns.Contains("egoera_balioa"))
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
                    // Beste mintegiko gailuak: gris argia
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            else // Irakaslea
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            // 2. Zutabe hutsak zuriz
            if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.LightGray;
                e.Value = "(hutsik)";
                e.FormattingApplied = true;
            }
        }

        private void dvgOrdenagailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void btnEgoeraAldatuOr_Click(object sender, EventArgs e)
        {
            if (dvgOrdenagailuak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgOrdenagailuak.SelectedRows[0];

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

                    int gailuId = Convert.ToInt32(row.Cells["ID"].Value);
                    int egoeraZaharra = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                    string egoeraTestua = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraBerria;

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

                    // ***** GARRANTZITSUA: Egoera "Matxuratuta" (1) bada, gorde hondatutakoak taulan *****
                    if (egoeraBerria == 1)
                    {
                        // Egiaztatu lehen ez zegoen Matxuratuta
                        if (egoeraZaharra != 1)
                        {
                            bool gordeOndo = DBGailuak.GordeHondatutakoGailua(gailuId, "Ordenagailua");
                            if (!gordeOndo)
                            {
                                MessageBox.Show("Arazoa gertatu da hondatutako gailua gordetzean.");
                                return;
                            }
                        }
                    }
                    if (egoeraBerria == 0 || egoeraBerria == 2)
                    {
                        DBGailuak.KenduHondatutakoGailua(gailuId);
                    }

                    Ordenagailua ordeEguneratu = new Ordenagailua(
                        row.Cells["Kodea"].Value.ToString(),
                        row.Cells["Modeloa"].Value.ToString(),
                        0, "", "", ""
                    );
                    ordeEguneratu.Id = gailuId;
                    ordeEguneratu.Egoera = egoeraBerria;

                    if (DBGailuak.EguneratuEgoeraPOO(ordeEguneratu))
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

        private void BtnEzabatuOr_Click(object sender, EventArgs e)
        {
            if (dvgOrdenagailuak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Mesedez, hautatu ordenagailu bat ezabatzeko.");
                return;
            }

            using (FormMezua mezua = new FormMezua("Ordenagailu hau historikora mugitu nahi duzu?"))
            {
                if (mezua.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow row = dvgOrdenagailuak.SelectedRows[0];

                        Ordenagailua ordeEzabatu = new Ordenagailua(
                            row.Cells["Kodea"].Value.ToString(),
                            row.Cells["Modeloa"].Value.ToString(),
                            0,
                            row.Cells["ram"].Value?.ToString() ?? "",
                            row.Cells["rom"].Value?.ToString() ?? "",
                            row.Cells["cpu"].Value?.ToString() ?? ""
                        );

                        ordeEzabatu.Id = Convert.ToInt32(row.Cells["ID"].Value);

                        if (DBGailuak.EzabatuOrdenagailuaPOO(ordeEzabatu))
                        {
                            MessageBox.Show("Ordenagailua ondo ezabatu eta historikora mugitu da.");
                            KargatuDatuak();
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
            OrdeBerriaSortu ordeBerria = new OrdeBerriaSortu();
            ordeBerria.ShowDialog(); // Formularioa modal gisa ireki

            // Formularioa itxi ondoren, datuak birkargatu
            KargatuDatuak();
        }
    }
}