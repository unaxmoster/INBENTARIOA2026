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
            this.ResizeRedraw = true;
            dvgGailuak.CellFormatting += dvgGailuak_CellFormatting;
            dvgGailuak.CellBeginEdit += dvgGailuak_CellBeginEdit;
            dvgGailuak.DataBindingComplete += dvgGailuak_DataBindingComplete;
        }

        private void GailuGuztiak_Load(object sender, EventArgs e)
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
                btnEzabatu.Enabled = false;
                btnEgoeraAldatu.Enabled = false;
                btnBerriaSortu.Enabled = false;
            }
            else if (rola == "MintegiBurua")
            {
                btnBerriaSortu.Enabled = false;
                btnEzabatu.Enabled = false;
            }
        }

        private void KargatuDatuak()
        {
            try
            {
                DataTable dt = DBGailuak.GetGailuGuztiak();
                dvgGailuak.DataSource = dt;

                // Zutabeak ezkutatu
                if (dvgGailuak.Columns.Contains("ID")) dvgGailuak.Columns["ID"].Visible = false;
                if (dvgGailuak.Columns.Contains("egoera_balioa")) dvgGailuak.Columns["egoera_balioa"].Visible = false;
                if (dvgGailuak.Columns.Contains("id_mintegia")) dvgGailuak.Columns["id_mintegia"].Visible = false;

                // ComboBox zutabea sortu
                if (!dvgGailuak.Columns.Contains("EgoeraCombo"))
                {
                    DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                    comboCol.HeaderText = "Egoera";
                    comboCol.Name = "EgoeraCombo";
                    comboCol.Items.Add("Ondo");
                    comboCol.Items.Add("Hondatuta");
                    comboCol.Items.Add("Konpontzen");
                    dvgGailuak.Columns.Add(comboCol);
                }

                // ComboBox eskuinean kokatu
                dvgGailuak.Columns["EgoeraCombo"].DisplayIndex = dvgGailuak.Columns.Count - 1;

                // ComboBox balioa hasieratu
                foreach (DataGridViewRow row in dvgGailuak.Rows)
                {
                    if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                    {
                        int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                        if (egoeraIndex >= 0 && egoeraIndex <= 2)
                        {
                            row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dvgGailuak.Columns["EgoeraCombo"]).Items[egoeraIndex];
                        }
                    }
                }

                // Konfigurazioak
                dvgGailuak.ReadOnly = false;
                dvgGailuak.AllowUserToAddRows = false;
                dvgGailuak.AllowUserToDeleteRows = false;

                // ComboBox zutabea BAKARRIK editagarria
                foreach (DataGridViewColumn col in dvgGailuak.Columns)
                {
                    if (col.Name != "EgoeraCombo")
                    {
                        col.ReadOnly = true;
                    }
                }

                dvgGailuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Baimenak konfiguratu
                KonfiguratuLerroenBaimenak();

                // Behartu birmarrazketa
                dvgGailuak.Invalidate();
                dvgGailuak.Refresh();
            }
            catch (Exception ex) { MessageBox.Show("Errorea kargatzean: " + ex.Message); }
        }

        private void KonfiguratuLerroenBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;

            bool idMintegiaExistitzenDa = dvgGailuak.Columns.Contains("id_mintegia");
            bool egoeraComboExistitzenDa = dvgGailuak.Columns.Contains("EgoeraCombo");

            if (!egoeraComboExistitzenDa) return;

            foreach (DataGridViewRow row in dvgGailuak.Rows)
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

        private void dvgGailuak_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dvgGailuak.Columns[e.ColumnIndex].Name != "EgoeraCombo")
            {
                e.Cancel = true;
            }
        }

        private void dvgGailuak_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dvgGailuak.Rows[e.RowIndex];
            string rola = Sarrera.Saioa.Rola;

            // 1. KOLOREA EZARRI ROLAREN ARABERA
            if (rola == "Ikt")
            {
                // Ikt: egoeraren kolorea
                if (dvgGailuak.Columns.Contains("egoera_balioa"))
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

                if (dvgGailuak.Columns.Contains("EgoeraCombo"))
                {
                    row.Cells["EgoeraCombo"].ReadOnly = false;
                }
            }
            else if (rola == "MintegiBurua")
            {
                bool idMintegiaExistitzenDa = dvgGailuak.Columns.Contains("id_mintegia");
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
                    if (dvgGailuak.Columns.Contains("egoera_balioa"))
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

                    if (dvgGailuak.Columns.Contains("EgoeraCombo"))
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = false;
                    }
                }
                else
                {
                    // Beste mintegiko gailuak: gris argia
                    row.DefaultCellStyle.BackColor = Color.LightGray;

                    if (dvgGailuak.Columns.Contains("EgoeraCombo"))
                    {
                        row.Cells["EgoeraCombo"].ReadOnly = true;
                    }
                }
            }
            else // Irakaslea
            {
                row.DefaultCellStyle.BackColor = Color.White;

                if (dvgGailuak.Columns.Contains("EgoeraCombo"))
                {
                    row.Cells["EgoeraCombo"].ReadOnly = true;
                }
            }
        }

        private void dvgGailuak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dvgGailuak.Invalidate();
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

                            if (mota == "Inprimagailua")
                            {
                                Inprimagailua inp = new Inprimagailua();
                                inp.Id = id;
                                inp.IdentifikazioKodea = row.Cells["identifikazio_kodea"].Value.ToString();
                                inp.MarkaModeloa = row.Cells["marka_modeloa"].Value.ToString();
                                emaitza = DBGailuak.EzabatuInprimagailuaPOO(inp);
                            }
                            else if (mota == "Ordenagailua")
                            {
                                string kodea = row.Cells["identifikazio_kodea"].Value.ToString();
                                string modeloa = row.Cells["marka_modeloa"].Value.ToString();
                                Ordenagailua ord = new Ordenagailua(kodea, modeloa, 0, "", "", "");
                                ord.Id = id;
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

                    if (row.Cells["EgoeraCombo"].Value == null)
                    {
                        MessageBox.Show("Aukeratu egoera bat lehenik.");
                        return;
                    }

                    string egoeraTestua = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraBerria;
                    string mota = row.Cells["Mota"].Value.ToString(); // "Ordenagailua" edo "Inprimagailua"

                    switch (egoeraTestua)
                    {
                        case "Ondo": egoeraBerria = 0; break;
                        case "Hondatuta": egoeraBerria = 1; break;
                        case "Konpontzen": egoeraBerria = 2; break;
                        default:
                            MessageBox.Show("Egoera balio ezezaguna: " + egoeraTestua);
                            return;
                    }

                    int gailuId = Convert.ToInt32(row.Cells["ID"].Value);
                    Gailua gailua = new Gailua();
                    gailua.Id = gailuId;
                    gailua.Egoera = egoeraBerria;

                    if (DBGailuak.EguneratuEgoeraPOO(gailua))
                    {
                       
                        // Egoera "Hondatuta" bada (1), gorde hondatutakoak taulan
                        if (egoeraBerria == 1)
                        {
                            bool gordeOndo = DBGailuak.GordeHondatutakoGailua(gailuId, mota);
                            if (!gordeOndo)
                            {
                                MessageBox.Show("Arazoa gertatu da hondatutako gailua gordetzean.");
                            }
                        }
                        // ********************************

                        MessageBox.Show("Egoera ondo eguneratu da.");
                        KargatuDatuak(); 
                    }
                    if (egoeraBerria == 0)
                    {
                        DBGailuak.KenduHondatutakoGailua(gailuId);
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

        private void btnBerriaSortu_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdInp ikusi = new OrdInp();
            ikusi.ShowDialog();
            this.Close();
        }
    }
}