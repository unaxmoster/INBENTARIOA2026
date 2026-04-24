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
                dvgOrdenagailuak.DataSource = null;

                // Datu-baseko datuak kargatu
                DataTable dt = DBGailuak.GetOrdenagailuGuztiak();
                dvgOrdenagailuak.DataSource = dt;

                // Zutabeak ezkutatu (Mota zutabea EZ ezkutatu, erakutsi nahi baduzu)
                if (dvgOrdenagailuak.Columns.Contains("ID"))
                    dvgOrdenagailuak.Columns["ID"].Visible = false;
                if (dvgOrdenagailuak.Columns.Contains("egoera_balioa"))
                    dvgOrdenagailuak.Columns["egoera_balioa"].Visible = false;
                if (dvgOrdenagailuak.Columns.Contains("id_mintegia"))
                    dvgOrdenagailuak.Columns["id_mintegia"].Visible = false;

                // Mota zutabea ikusgai utzi (edo nahi baduzu izenburua aldatu)
                if (dvgOrdenagailuak.Columns.Contains("Mota"))
                {
                    dvgOrdenagailuak.Columns["Mota"].HeaderText = "Mota";
                    dvgOrdenagailuak.Columns["Mota"].DisplayIndex = 1; // Kodearen ondoren kokatzeko
                }

                // EZABATU ZUTABE HUTSAK (elementurik ez dutenak)
                for (int i = dvgOrdenagailuak.Columns.Count - 1; i >= 0; i--)
                {
                    DataGridViewColumn col = dvgOrdenagailuak.Columns[i];

                    // EgoeraCombo oraindik ez dago, beraz zutabe originalak bakarrik egiaztatu
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

                // ComboBox zutabea sortu (dagoeneko ez badago)
                if (!dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                {
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                    comboCol.HeaderText = "Egoera";
                    comboCol.Name = "EgoeraCombo";

                    // Aukerak gehitu (zure erreferentziako kodearen moduan)
                    comboCol.Items.Add("Ondo");      // Index 0
                    comboCol.Items.Add("Matxuratuta"); // Index 1
                    comboCol.Items.Add("Konpontzen"); // Index 2

                    dvgOrdenagailuak.Columns.Add(comboCol);
                }

                // ComboBox-a eskuinean kokatzeko
                dvgOrdenagailuak.Columns["EgoeraCombo"].DisplayIndex = dvgOrdenagailuak.Columns.Count - 1;

                // ComboBox-aren balioa hasieratu (zure erreferentziako kodearen moduan)
                foreach (DataGridViewRow row in dvgOrdenagailuak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        if (egoeraIndex >= 0 && egoeraIndex <= 2)
                        {
                            row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dvgOrdenagailuak.Columns["EgoeraCombo"]).Items[egoeraIndex];
                        }
                    }
                }

                // KONFIGURAZIOAK (zure erreferentziako kodearen moduan)
                dvgOrdenagailuak.ReadOnly = false;
                dvgOrdenagailuak.AllowUserToAddRows = false;    // Beheko lerro zuria kentzeko
                dvgOrdenagailuak.AllowUserToDeleteRows = false;

                // ComboBox zutabea BAKARRIK editagarria
                foreach (DataGridViewColumn col in dvgOrdenagailuak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                // Zutabe hutsak berriro egiaztatu (ComboBox gehitu ostean)
                for (int i = dvgOrdenagailuak.Columns.Count - 1; i >= 0; i--)
                {
                    DataGridViewColumn col = dvgOrdenagailuak.Columns[i];

                    if (col.Name == "EgoeraCombo") continue; // EgoeraCombo ez ezabatu

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

                dvgOrdenagailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Baimenak konfiguratu
                KonfiguratuLerroenBaimenak();

                // Behartu birmarrazketa
                dvgOrdenagailuak.Invalidate();
                dvgOrdenagailuak.Refresh();

                // Egoera zutabea ikusgai dagoela ziurtatu
                if (dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                {
                    dvgOrdenagailuak.Columns["EgoeraCombo"].ReadOnly = false;
                }
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

            // Egiaztatu zutabeak existitzen diren
            bool idMintegiaExistitzenDa = dvgOrdenagailuak.Columns.Contains("id_mintegia");
            bool egoeraComboExistitzenDa = dvgOrdenagailuak.Columns.Contains("EgoeraCombo");

            if (!egoeraComboExistitzenDa) return;

            foreach (DataGridViewRow row in dvgOrdenagailuak.Rows)
            {
                if (row.IsNewRow) continue;

                // Lehenik, ComboBox irakurtzeko soilik jartzen dugu
                row.Cells["EgoeraCombo"].ReadOnly = true;

                // Baimendutako erabiltzaileen arabera ezarri ReadOnly
                if (rola == "Ikt")
                {
                    // Ikt-ek edozein gailuren egoera aldatu dezake
                    row.Cells["EgoeraCombo"].ReadOnly = false;
                }
                else if (rola == "MintegiBurua")
                {
                    // MintegiBurua: bere mintegiko gailuak bakarrik
                    if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                    {
                        int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                        if (gailuMintegiId == erabiltzaileMintegiId)
                        {
                            row.Cells["EgoeraCombo"].ReadOnly = false;
                            System.Diagnostics.Debug.WriteLine($"Lerroa {row.Index}: ReadOnly=false (bere mintegikoa)");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Lerroa {row.Index}: ReadOnly=true (beste mintegikoa)");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Lerroa {row.Index}: id_mintegia ez da existitzen");
                    }
                }
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
                // Ikt: egoeraren kolorea aplikatzen dugu zuzenean
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
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }

                // Ikt-rentzat ComboBox editagarri
                if (dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                {
                    row.Cells["EgoeraCombo"].ReadOnly = false;
                }
            }
            else if (rola == "MintegiBurua")
            {
                // MintegiBurua: egiaztatu bere mintegikoa den
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
                    // Bere mintegiko gailuak: egoeraren kolorea aplikatu eta ComboBox editagarri
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
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                        }
                    }

                    // Bere mintegiko gailuak: ComboBox editagarri
                    if (dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = false;
                    }
                }
                else
                {
                    // Beste mintegiko gailuak: gris argia eta ComboBox irakurtzeko soilik
                    row.DefaultCellStyle.BackColor = Color.LightGray;

                    // Beste mintegiko gailuak: ComboBox ezin aldatu
                    if (dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = true;
                    }
                }
            }
            else // Irakaslea
            {
                row.DefaultCellStyle.BackColor = Color.White;

                // Irakasleak ezin du ezer aldatu
                if (dvgOrdenagailuak.Columns.Contains("EgoeraCombo"))
                {
                    row.Cells["EgoeraCombo"].ReadOnly = true;
                }
            }

            // 2. Zutabe hutsak zuriz eta "hutsik" testuarekin
            if (e.Value == null || e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.LightGray;
                e.CellStyle.Font = new Font(dvgOrdenagailuak.Font, FontStyle.Italic);
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

                    if (row.Cells["EgoeraCombo"].Value == null)
                    {
                        MessageBox.Show("Aukeratu egoera bat lehenik.");
                        return;
                    }

                    string egoeraTestua = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraZenbakia;
                    int egoeraZaharra = Convert.ToInt32(row.Cells["egoera_balioa"].Value);

                    switch (egoeraTestua)
                    {
                        case "Ondo": egoeraZenbakia = 0; break;
                        case "Matxuratuta": egoeraZenbakia = 1; break;
                        case "Konpontzen": egoeraZenbakia = 2; break;
                        default:
                            MessageBox.Show("Egoera balio ezezaguna: " + egoeraTestua);
                            return;
                    }

                    int gailuId = Convert.ToInt32(row.Cells["ID"].Value);

                    Ordenagailua ordeEguneratu = new Ordenagailua(
                        row.Cells["Kodea"].Value.ToString(),
                        row.Cells["Modeloa"].Value.ToString(),
                        0, "", "", ""
                    );
                    ordeEguneratu.Id = gailuId;
                    ordeEguneratu.Egoera = egoeraZenbakia;

                    if (DBGailuak.EguneratuEgoeraPOO(ordeEguneratu))
                    {
                        // Matxuratuta (1) bada ETA lehen ez bazen Matxuratuta, hondatutakoak taulara sartu
                        if (egoeraZenbakia == 1 && egoeraZaharra != 1)
                        {
                            DBGailuak.GordeHondatutakoGailua(gailuId, "Ordenagailua");  // 2 parametro!
                        }

                        MessageBox.Show($"Egoera ondo eguneratu da: {egoeraTestua}");
                        KargatuDatuak();
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