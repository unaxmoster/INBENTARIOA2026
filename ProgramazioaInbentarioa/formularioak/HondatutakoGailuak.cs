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
            KargatuGailuHondatuak();
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
                dgvHondatutakoak.Columns.Clear();
                dgvHondatutakoak.DataSource = null;

                DataTable dt = DBGailuak.GetHondatutakoGailuak();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Ez dago hondatutako gailurik datu-basean.");
                    return;
                }

                // ComboBox zutabea sortu
                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.HeaderText = "Egoera Berria";
                comboCol.Name = "EgoeraCombo";
                comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                dgvHondatutakoak.Columns.Add(comboCol);

                // Lotu datuak
                dgvHondatutakoak.DataSource = dt;

                // Konfigurazioak
                dgvHondatutakoak.ReadOnly = false;
                dgvHondatutakoak.AllowUserToAddRows = false;
                dgvHondatutakoak.AllowUserToDeleteRows = false;

                // Zutabeak ezkutatu (EGIAZTATU existitzen direla lehenik)
                if (dgvHondatutakoak.Columns.Contains("ID"))
                    dgvHondatutakoak.Columns["ID"].Visible = false;
                if (dgvHondatutakoak.Columns.Contains("egoera_balioa"))
                    dgvHondatutakoak.Columns["egoera_balioa"].Visible = false;
                if (dgvHondatutakoak.Columns.Contains("id_mintegia"))
                    dgvHondatutakoak.Columns["id_mintegia"].Visible = false;

                // ComboBox eskuinean kokatu
                if (dgvHondatutakoak.Columns.Contains("EgoeraCombo"))
                {
                    dgvHondatutakoak.Columns["EgoeraCombo"].DisplayIndex = dgvHondatutakoak.Columns.Count - 1;
                }

                // Beste zutabe guztiak irakurtzeko soilik
                foreach (DataGridViewColumn col in dgvHondatutakoak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                // ComboBox balioak hasieratu
                foreach (DataGridViewRow row in dgvHondatutakoak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        if (egoeraIndex >= 0 && egoeraIndex <= 2)
                        {
                            row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dgvHondatutakoak.Columns["EgoeraCombo"]).Items[egoeraIndex];
                        }
                    }
                }

                dgvHondatutakoak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Baimenak konfiguratu
                KonfiguratuLerroenBaimenak();

                // Behartu birmarrazketa
                dgvHondatutakoak.Invalidate();
                dgvHondatutakoak.Refresh();
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

                row.Cells["EgoeraCombo"].ReadOnly = true;

                if (rola == "Ikt")
                {
                    row.Cells["EgoeraCombo"].ReadOnly = false;
                }
                else if (rola == "MintegiBurua")
                {
                    if (idMintegiaExistitzenDa && row.Cells["id_mintegia"].Value != null && row.Cells["id_mintegia"].Value != DBNull.Value)
                    {
                        int gailuMintegiId = Convert.ToInt32(row.Cells["id_mintegia"].Value);
                        if (gailuMintegiId == erabiltzaileMintegiId)
                        {
                            row.Cells["EgoeraCombo"].ReadOnly = false;
                        }
                    }
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

                    // Egiaztatu ComboBox editagarria den
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

                    int idGailua = Convert.ToInt32(row.Cells["ID"].Value);
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraBerria;

                    switch (hautatutakoEgoera)
                    {
                        case "Ondo": egoeraBerria = 0; break;
                        case "Matxuratuta": egoeraBerria = 1; break;
                        case "Konpontzen": egoeraBerria = 2; break;
                        default:
                            MessageBox.Show("Egoera balio ezezaguna: " + hautatutakoEgoera);
                            return;
                    }

                    Gailua gailua = new Gailua();
                    gailua.Id = idGailua;
                    gailua.Egoera = egoeraBerria;

                    if (DBGailuak.EguneratuEgoeraPOO(gailua))
                    {
                        MessageBox.Show("Egoera ondo eguneratu da.");
                        KargatuGailuHondatuak();
                    }
                    else
                    {
                        MessageBox.Show("Ezin izan da egoera eguneratu.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
            }
            else { MessageBox.Show("Hautatu gailu bat."); }
        }
    }
}