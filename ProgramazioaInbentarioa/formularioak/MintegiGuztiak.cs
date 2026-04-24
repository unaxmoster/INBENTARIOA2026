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
    public partial class MintegiGuztiak : FormBase
    {
        public MintegiGuztiak()
        {
            InitializeComponent();
            dvgMintegika.CellFormatting += dvgMintegika_CellFormatting;
            dvgMintegika.CellBeginEdit += dvgMintegika_CellBeginEdit;
            dvgMintegika.DataBindingComplete += dvgMintegika_DataBindingComplete;
            KargatuMintegiakCombo();
            KargatuGailuak(0);
        }

        private void MintegiGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            KonfiguratuBaimenak();
            KargatuGailuak(0);
        }

        private void KonfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                button1.Enabled = false;
                BtnEzabatu.Enabled = false;
                btnEgoeraAldatu.Enabled = false;
            }
            else if (rola == "MintegiBurua")
            {
                button1.Enabled = false;
                BtnEzabatu.Enabled = false;
            }
        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            IKUSI mintegiak = new IKUSI();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void KargatuMintegiakCombo()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string query = "SELECT id_mintegia, izena FROM Mintegiak";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataRow row = dt.NewRow();
                    row["id_mintegia"] = 0;
                    row["izena"] = "Mintegi guztietako gailuak";
                    dt.Rows.InsertAt(row, 0);

                    cbMintegiak.DataSource = dt;
                    cbMintegiak.DisplayMember = "izena";
                    cbMintegiak.ValueMember = "id_mintegia";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message);
            }
        }

        private void KargatuGailuak(int idMintegia)
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    string query = @"SELECT G.id_gailua AS 'ID', 
                                           G.identifikazio_kodea AS 'Kodea', 
                                           CASE 
                                               WHEN O.id_gailua IS NOT NULL THEN 'Ordenagailua' 
                                               WHEN I.id_gailua IS NOT NULL THEN 'Inprimagailua' 
                                               ELSE 'Besterik' 
                                           END AS 'Gailu mota', 
                                           G.marka_modeloa AS 'Modeloa', 
                                           M.izena AS 'Mintegia', 
                                           M.id_mintegia AS 'id_mintegia',
                                           G.eroste_data AS 'Data', 
                                           G.egoera AS 'egoera_balioa' 
                                    FROM Gailuak G 
                                    JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia 
                                    LEFT JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua 
                                    LEFT JOIN Inprimagailuak I ON G.id_gailua = I.id_gailua";

                    if (idMintegia > 0) query += " WHERE G.id_mintegia = @idMintegia";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    if (idMintegia > 0) cmd.Parameters.AddWithValue("@idMintegia", idMintegia);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // ComboBox zutabea sortu
                    if (!dvgMintegika.Columns.Contains("EgoeraCombo"))
                    {
                        DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                        comboCol.HeaderText = "Egoera";
                        comboCol.Name = "EgoeraCombo";
                        comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                        dvgMintegika.Columns.Add(comboCol);
                    }

                    dvgMintegika.DataSource = dt;

                    // Diseinua
                    dvgMintegika.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dvgMintegika.Columns["EgoeraCombo"].DisplayIndex = dvgMintegika.Columns.Count - 1;

                    // Zutabeak ezkutatu
                    if (dvgMintegika.Columns.Contains("egoera_balioa"))
                        dvgMintegika.Columns["egoera_balioa"].Visible = false;
                    if (dvgMintegika.Columns.Contains("ID"))
                        dvgMintegika.Columns["ID"].Visible = false;
                    if (dvgMintegika.Columns.Contains("id_mintegia"))
                        dvgMintegika.Columns["id_mintegia"].Visible = false;

                    // Konfigurazioak
                    dvgMintegika.ReadOnly = false;
                    dvgMintegika.AllowUserToAddRows = false;
                    dvgMintegika.AllowUserToDeleteRows = false;

                    // ComboBox zutabea BAKARRIK editagarria
                    foreach (DataGridViewColumn col in dvgMintegika.Columns)
                    {
                        if (col.Name != "EgoeraCombo")
                        {
                            col.ReadOnly = true;
                        }
                    }

                    // ComboBox balioak hasieratu
                    foreach (DataGridViewRow row in dvgMintegika.Rows)
                    {
                        if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                        {
                            int egoeraIndex = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                            if (egoeraIndex >= 0 && egoeraIndex <= 2)
                            {
                                row.Cells["EgoeraCombo"].Value = ((DataGridViewComboBoxColumn)dvgMintegika.Columns["EgoeraCombo"]).Items[egoeraIndex];
                            }
                        }
                    }

                    // Baimenak konfiguratu
                    KonfiguratuLerroenBaimenak();

                    // Behartu birmarrazketa
                    dvgMintegika.Invalidate();
                    dvgMintegika.Refresh();
                }
            }
            catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
        }

        private void KonfiguratuLerroenBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            int erabiltzaileMintegiId = Sarrera.Saioa.IdMintegia;

            bool idMintegiaExistitzenDa = dvgMintegika.Columns.Contains("id_mintegia");
            bool egoeraComboExistitzenDa = dvgMintegika.Columns.Contains("EgoeraCombo");

            if (!egoeraComboExistitzenDa) return;

            foreach (DataGridViewRow row in dvgMintegika.Rows)
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

        private void cbMintegiak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMintegiak.SelectedValue != null && int.TryParse(cbMintegiak.SelectedValue.ToString(), out int idHautatua))
            {
                KargatuGailuak(idHautatua);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdInp mintegiak = new OrdInp();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void BtnEzabatu_Click(object sender, EventArgs e)
        {
            if (dvgMintegika.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dvgMintegika.SelectedRows[0];

                using (FormMezua mezua = new FormMezua("Gailu hau historikora mugitu nahi duzu?"))
                {
                    if (mezua.ShowDialog() == DialogResult.Yes)
                    {
                        try
                        {
                            string gailuMota = row.Cells["Gailu mota"].Value.ToString();
                            bool emaitza = false;

                            if (gailuMota == "Inprimagailua")
                            {
                                Inprimagailua inp = new Inprimagailua();
                                inp.Id = Convert.ToInt32(row.Cells["ID"].Value);
                                inp.IdentifikazioKodea = row.Cells["Kodea"].Value.ToString();
                                inp.MarkaModeloa = row.Cells["Modeloa"].Value.ToString();
                                emaitza = DBGailuak.EzabatuInprimagailuaPOO(inp);
                            }
                            else if (gailuMota == "Ordenagailua")
                            {
                                Ordenagailua ord = new Ordenagailua(
                                    row.Cells["Kodea"].Value.ToString(),
                                    row.Cells["Modeloa"].Value.ToString(),
                                    0, "", "", ""
                                );
                                ord.Id = Convert.ToInt32(row.Cells["ID"].Value);
                                emaitza = DBGailuak.EzabatuOrdenagailuaPOO(ord);
                            }

                            if (emaitza)
                            {
                                MessageBox.Show("Gailua ondo mugitu da historikora.");
                                KargatuGailuak(Convert.ToInt32(cbMintegiak.SelectedValue));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Errorea ezabatzean: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgMintegika.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgMintegika.SelectedRows[0];

                    if (row.Cells["EgoeraCombo"].Value == null)
                    {
                        MessageBox.Show("Aukeratu egoera bat lehenik.");
                        return;
                    }

                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value.ToString();
                    int egoeraZenbakia;

                    switch (hautatutakoEgoera)
                    {
                        case "Ondo": egoeraZenbakia = 0; break;
                        case "Matxuratuta": egoeraZenbakia = 1; break;
                        case "Konpontzen": egoeraZenbakia = 2; break;
                        default:
                            MessageBox.Show("Egoera balio ezezaguna: " + hautatutakoEgoera);
                            return;
                    }

                    Gailua gailuEguneratua = new Gailua();
                    gailuEguneratua.Id = Convert.ToInt32(row.Cells["ID"].Value);
                    gailuEguneratua.Egoera = egoeraZenbakia;

                    if (DBGailuak.EguneratuEgoeraPOO(gailuEguneratua))
                    {
                        MessageBox.Show("Egoera ondo eguneratu da.");
                        KargatuGailuak(Convert.ToInt32(cbMintegiak.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea egoera aldatzean: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Hautatu lerro bat lehenik.");
            }
        }

        private void dvgMintegika_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dvgMintegika.Columns[e.ColumnIndex].Name != "EgoeraCombo")
            {
                e.Cancel = true;
                return;
            }

            var idBalioa = dvgMintegika.Rows[e.RowIndex].Cells["ID"].Value;
            if (idBalioa == null || idBalioa == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        private void dvgMintegika_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dvgMintegika.Invalidate();
        }

        private void dvgMintegika_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dvgMintegika.Rows[e.RowIndex];
            string rola = Sarrera.Saioa.Rola;

            // 1. KOLOREA EZARRI ROLAREN ARABERA
            if (rola == "Ikt")
            {
                // Ikt: egoeraren kolorea
                if (dvgMintegika.Columns.Contains("egoera_balioa"))
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
                bool idMintegiaExistitzenDa = dvgMintegika.Columns.Contains("id_mintegia");
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
                    if (dvgMintegika.Columns.Contains("egoera_balioa"))
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
        }
    }
}