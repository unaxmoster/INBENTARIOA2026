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
    public partial class HondatutakoGailuak : FormBase
    {
        public HondatutakoGailuak()
        {
            InitializeComponent();
            dgvHondatutakoak.CellFormatting += dgvHondatutakoak_CellFormatting;
            dgvHondatutakoak.CellBeginEdit += dgvHondatutakoak_CellBeginEdit;
            dgvHondatutakoak.DataBindingComplete += dgvHondatutakoak_DataBindingComplete;
        }

        private void HondatutakoGailuak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
            KargatuGailuHondatuak();
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
                // MintegiBurua: berariazko baimenak lerro bakoitzean konfiguratuko dira
                btnEgoeraAldatu.Enabled = true; // Botoia gaituta, baina barruan egiaztatuko da
            }
        }

        private void KargatuGailuHondatuak()
        {
            try
            {
                // GARRANTZITSUA: Zutabe guztiak garbitu hasieran
                dgvHondatutakoak.Columns.Clear();
                dgvHondatutakoak.DataSource = null;

                DataTable dt = DBGailuak.GetHondatutakoGailuak();

                // Gailurik ez badago
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Ez dago hondatutako gailurik datu-basean.", "Informazioa",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Iragazi taula: Egoera "Ondo" (0) duten gailuak KENDU
                // Horrela, behin konponduta, ez dira agertuko
                DataRow[] rowsToDelete = dt.Select("egoera_balioa = 0");
                foreach (DataRow row in rowsToDelete)
                {
                    dt.Rows.Remove(row);
                }

                // Berriro egiaztatu iragazkiaren ondoren gailurik geratzen den
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Ez dago hondatutako gailurik datu-basean.", "Informazioa",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ComboBox zutabea sortu
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.HeaderText = "Egoera Berria";
                comboCol.Name = "EgoeraCombo";
                comboCol.Items.AddRange("Ondo", "Hondatuta", "Konpontzen");

                // Lotu datuak
                dgvHondatutakoak.DataSource = dt;

                // ComboBox zutabea gehitu
                dgvHondatutakoak.Columns.Add(comboCol);

                // Konfigurazioak
                dgvHondatutakoak.ReadOnly = false;
                dgvHondatutakoak.AllowUserToAddRows = false;
                dgvHondatutakoak.AllowUserToDeleteRows = false;

                // Zutabeak ezkutatu (existitzen badira)
                if (dgvHondatutakoak.Columns.Contains("ID"))
                    dgvHondatutakoak.Columns["ID"].Visible = false;
                if (dgvHondatutakoak.Columns.Contains("id_mintegia"))
                    dgvHondatutakoak.Columns["id_mintegia"].Visible = false;
                if (dgvHondatutakoak.Columns.Contains("egoera_balioa"))
                    dgvHondatutakoak.Columns["egoera_balioa"].Visible = false;

                // ComboBox eskuinean kokatu
                dgvHondatutakoak.Columns["EgoeraCombo"].DisplayIndex = dgvHondatutakoak.Columns.Count - 1;

                // ComboBox-en zabalera txikiagoa ezarri (aukerakoa)
                dgvHondatutakoak.Columns["EgoeraCombo"].Width = 100;

                // Beste zutabe guztiak irakurtzeko soilik
                foreach (DataGridViewColumn col in dgvHondatutakoak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                // ComboBox balioak hasieratu egoera_balioa erabiliz
                foreach (DataGridViewRow row in dgvHondatutakoak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        string egoeraTestua = "";
                        switch (egoeraIndex)
                        {
                            case 0: egoeraTestua = "Ondo"; break;
                            case 1: egoeraTestua = "Hondatuta"; break;
                            case 2: egoeraTestua = "Konpontzen"; break;
                        }
                        row.Cells["EgoeraCombo"].Value = egoeraTestua;
                    }
                }

                dgvHondatutakoak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Baimenak konfiguratu
                KonfiguratuLerroenBaimenak();
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

            bool idMintegiaExistitzenDa = dgvHondatutakoak.Columns.Contains("id_mintegia");
            bool egoeraComboExistitzenDa = dgvHondatutakoak.Columns.Contains("EgoeraCombo");

            if (!egoeraComboExistitzenDa) return;

            foreach (DataGridViewRow row in dgvHondatutakoak.Rows)
            {
                if (row.IsNewRow) continue;

                // Lehenespenez, ComboBox irakurtzeko soilik eta gris
                if (row.Cells["EgoeraCombo"] != null)
                {
                    row.Cells["EgoeraCombo"].ReadOnly = true;
                    row.Cells["EgoeraCombo"].Style.BackColor = Color.LightGray;
                }

                // Lerroaren kolorea gris jartzen dugu lehenespenez
                row.DefaultCellStyle.BackColor = Color.LightGray;

                if (rola == "Ikt")
                {
                    // IKT-ak denak editatu ditzake eta kolore normala
                    if (row.Cells["EgoeraCombo"] != null)
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = false;
                        row.Cells["EgoeraCombo"].Style.BackColor = Color.White;
                    }
                    row.DefaultCellStyle.BackColor = Color.Salmon; // Gailu hondatuak
                }
                else if (rola == "MintegiBurua")
                {
                    // MintegiBurua: bere mintegikoak bakarrik
                    if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                    {
                        int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                        if (gailuMintegiId == erabiltzaileMintegiId)
                        {
                            // Bere mintegiko gailua: editatu daiteke eta kolore normala
                            if (row.Cells["EgoeraCombo"] != null)
                            {
                                row.Cells["EgoeraCombo"].ReadOnly = false;
                                row.Cells["EgoeraCombo"].Style.BackColor = Color.White;
                            }
                            row.DefaultCellStyle.BackColor = Color.Salmon;
                        }
                        else
                        {
                            // Beste mintegiko gailua: ezin editatu eta gris
                            if (row.Cells["EgoeraCombo"] != null)
                            {
                                row.Cells["EgoeraCombo"].ReadOnly = true;
                                row.Cells["EgoeraCombo"].Style.BackColor = Color.LightGray;
                            }
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                    }
                }
                else // Irakaslea
                {
                    // Irakasleak: ezin editatu, baina kolore salmon (soilik ikusi)
                    if (row.Cells["EgoeraCombo"] != null)
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = true;
                        row.Cells["EgoeraCombo"].Style.BackColor = Color.LightGray;
                    }
                    row.DefaultCellStyle.BackColor = Color.Salmon;
                }
            }
        }

        private void dgvHondatutakoak_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // ComboBox ez den zutabe bat editatu nahi bada, editazioa utzi
            if (dgvHondatutakoak.Columns[e.ColumnIndex].Name != "EgoeraCombo")
            {
                e.Cancel = true;
            }
        }

        private void dgvHondatutakoak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvHondatutakoak.Rows[e.RowIndex];
            string rola = Sarrera.Saioa.Rola;

            // 1. KOLOREA EZARRI ROLAREN ARABERA
            if (rola == "Ikt")
            {
                // Ikt: kolore normala (hondatutakoak direnez, salmon kolorea)
                row.DefaultCellStyle.BackColor = Color.Salmon;
            }
            else if (rola == "MintegiBurua")
            {
                bool idMintegiaExistitzenDa = dgvHondatutakoak.Columns.Contains("id_mintegia");
                bool bereMintegikoa = false;

                if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                {
                    int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                    int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;
                    bereMintegikoa = (gailuMintegiId == erabiltzaileMintegiId);
                }

                if (bereMintegikoa)
                {
                    // Bere mintegiko gailu hondatuak: salmon kolorea
                    row.DefaultCellStyle.BackColor = Color.Salmon;
                }
                else
                {
                    // Beste mintegiko gailu hondatuak: gris argia
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            else // Irakaslea
            {
                row.DefaultCellStyle.BackColor = Color.Salmon;
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
            dgvHondatutakoak.Invalidate();
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dgvHondatutakoak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dgvHondatutakoak.SelectedRows[0];

                    // Egiaztatu ComboBox editagarria den (baimenak)
                    if (row.Cells["EgoeraCombo"].ReadOnly)
                    {
                        MessageBox.Show("Ez duzu baimenik gailu honen egoera aldatzeko (ez da zure mintegikoa edo ez duzu baimenik).");
                        return;
                    }

                    if (row.Cells["EgoeraCombo"].Value == null)
                    {
                        MessageBox.Show("Aukeratu egoera bat lehenik.");
                        return;
                    }

                    int idGailua = Convert.ToInt32(row.Cells["ID"].Value);
                    int egoeraZaharra = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraBerria;

                    switch (hautatutakoEgoera)
                    {
                        case "Ondo": egoeraBerria = 0; break;
                        case "Hondatuta": egoeraBerria = 1; break;
                        case "Konpontzen": egoeraBerria = 2; break;
                        default:
                            MessageBox.Show("Egoera balio ezezaguna: " + hautatutakoEgoera);
                            return;
                    }

                    // Egoera aldatu bada bakarrik egin
                    if (egoeraZaharra == egoeraBerria)
                    {
                        MessageBox.Show("Egoera berdina da. Ez da eguneratu behar.");
                        return;
                    }

                    // Egoera "Hondatuta" bada (1), gorde hondatutakoak taulan (matxura kopurua handitu)
                    if (egoeraBerria == 1)
                    {
                        string mota = row.Cells["Mota"].Value.ToString();
                        DBGailuak.GordeHondatutakoGailua(idGailua, mota);
                    }

                    Gailua gailua = new Gailua();
                    gailua.Id = idGailua;
                    gailua.Egoera = egoeraBerria;

                    if (DBGailuak.EguneratuEgoeraPOO(gailua))
                    {
                        MessageBox.Show("Egoera ondo eguneratu da.");

                        // Egoera "Ondo" (0) bada, gailua hondatutakoen zerrendatik desagertu behar da
                        // Horregatik, datu-basea berriro kargatu
                        KargatuGailuHondatuak();
                    }
                    else
                    {
                        MessageBox.Show("Ezin izan da egoera eguneratu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Hautatu gailu bat.");
            }
        }
    }
}