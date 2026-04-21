using Inbentarioa.DatuBasie;
using Inbentarioa.formularioak;
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
    public partial class MintegiGuztiak : Form
    {
        public MintegiGuztiak()
        {
            InitializeComponent();
            KargatuMintegiakCombo(); // ComboBox-a betetzen du
            KargatuGailuak(0);       // Hasieran denak erakusten ditu
        }

        private void MintegiGuztiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color colorInicio = ColorTranslator.FromHtml("#C2CBED");
            Color colorFin = ColorTranslator.FromHtml("#003FA1");

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                colorInicio,
                colorFin,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
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

                    // "Guztiak" aukera sortu eta DataTable-aren hasieran sartu
                    DataRow row = dt.NewRow();
                    row["id_mintegia"] = 0; // 0 erabiliko dugu ID bezala "Guztiak" denerako
                    row["izena"] = "Mintegi guztietako gailuak";
                    dt.Rows.InsertAt(row, 0);

                    cbMintegiak.DataSource = dt;
                    cbMintegiak.DisplayMember = "izena";      // Erabiltzaileak ikusiko duena
                    cbMintegiak.ValueMember = "id_mintegia"; // Kodeak barnean erabiliko duena
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

                    // SQL: G.egoera zuzenean hartzen dugu (0, 1, 2), ez testu bezala
                    string query = "SELECT G.id_gailua AS 'ID', " +
                                   "CASE " +
                                   "  WHEN O.id_gailua IS NOT NULL THEN 'Ordenagailua' " +
                                   "  WHEN I.id_gailua IS NOT NULL THEN 'Inprimagailua' " +
                                   "  ELSE 'Besterik' " +
                                   "END AS 'Gailu mota', " +
                                   "G.marka_modeloa AS 'Modeloa', " +
                                   "M.izena AS 'Mintegia', G.eroste_data AS 'Data', " +
                                   "G.egoera AS 'egoera_balioa' " + // Balio numerikoa gorde
                                   "FROM Gailuak G " +
                                   "JOIN Mintegiak M ON G.id_mintegia = M.id_mintegia " +
                                   "LEFT JOIN Ordenagailuak O ON G.id_gailua = O.id_gailua " +
                                   "LEFT JOIN Inprimagailuak I ON G.id_gailua = I.id_gailua";

                    if (idMintegia > 0) query += " WHERE G.id_mintegia = @idMintegia";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    if (idMintegia > 0) cmd.Parameters.AddWithValue("@idMintegia", idMintegia);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // 1. ComboBox zutabea sortu (ez bada existitzen)
                    if (!dvgMintegika.Columns.Contains("EgoeraCombo"))
                    {
                        DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                        comboCol.HeaderText = "Egoera";
                        comboCol.Name = "EgoeraCombo";
                        comboCol.Items.AddRange("Ondo", "Matxuratuta", "Konpontzen");
                        dvgMintegika.Columns.Add(comboCol);
                    }

                    dvgMintegika.DataSource = dt;

                    // 2. DISEINUA: Zabalera osoa
                    dvgMintegika.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dvgMintegika.Columns["EgoeraCombo"].DisplayIndex = dvgMintegika.Columns.Count - 1;
                    dvgMintegika.Columns["egoera_balioa"].Visible = false;

                    // 3. Edizio baimenak eta babesa
                    dvgMintegika.ReadOnly = false;
                    foreach (DataGridViewColumn col in dvgMintegika.Columns)
                    {
                        if (col.Name != "EgoeraCombo") col.ReadOnly = true;
                    }

                    // ID-a txikiago mantendu
                    if (dvgMintegika.Columns.Contains("ID"))
                    {
                        dvgMintegika.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dvgMintegika.Columns["ID"].Width = 45;
                    }

                    dvgMintegika.AllowUserToAddRows = false;

                    // ComboBox-eko balioak hasieratu kargatutako datuekin
                    KargatuEgoeraBalioakGrid();
                }
            }
            catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
        }
        private void KargatuEgoeraBalioakGrid()
        {
            foreach (DataGridViewRow row in dvgMintegika.Rows)
            {
                if (row.Cells["egoera_balioa"].Value != null && row.Cells["egoera_balioa"].Value != DBNull.Value)
                {
                    int index = Convert.ToInt32(row.Cells["egoera_balioa"].Value);
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["EgoeraCombo"];
                    if (index >= 0 && index < cell.Items.Count)
                    {
                        row.Cells["EgoeraCombo"].Value = cell.Items[index];
                    }
                }
            }
        }

        private void cbMintegiak_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ziurtatu zerbait hautatuta dagoela eta balioa zenbakia dela
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
                var erantzuna = MessageBox.Show("Ziur zaude gailu hau ezabatu nahi duzula?", "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        // 1. Hartu gailuaren IDa (Grid-eko "ID" zutabetik)
                        int idGailua = Convert.ToInt32(dvgMintegika.SelectedRows[0].Cells["ID"].Value);
                        string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                        using (MySqlConnection conn = new MySqlConnection(konexioa))
                        {
                            conn.Open();
                            // Gailua ezabatzean, herentziagatik (CASCADE baduzu) umea ere ezabatuko da
                            // Ez baduzu CASCADE, lehenago umea ezabatu beharko zenuke (Ordenagailuak/Inprimagailuak)
                            string sql = "DELETE FROM Gailuak WHERE id_gailua = @id";

                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", idGailua);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Gailua ondo ezabatu da.");
                            }
                        }

                        // 2. Freskatu grid-a orain hautatuta dagoen mintegi berdinarekin
                        int idHautatua = Convert.ToInt32(cbMintegiak.SelectedValue);
                        KargatuGailuak(idHautatua);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea ezabatzean: ");
                    }
                }
            }
            else
            {
                MessageBox.Show("Mesedez, hautatu gailu bat zerrendatik.");
            }
        }

        private void btnEgoeraAldatu_Click(object sender, EventArgs e)
        {
            if (dvgMintegika.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dvgMintegika.SelectedRows[0];
                    if (row.Cells["ID"].Value == null) return;

                    int idGailua = Convert.ToInt32(row.Cells["ID"].Value);
                    string hautatutakoEgoera = row.Cells["EgoeraCombo"].Value?.ToString();

                    if (string.IsNullOrEmpty(hautatutakoEgoera)) return;

                    int egoeraBerria = 0;
                    if (hautatutakoEgoera == "Matxuratuta") egoeraBerria = 1;
                    else if (hautatutakoEgoera == "Konpontzen") egoeraBerria = 2;

                    string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                    using (MySqlConnection conn = new MySqlConnection(konexioa))
                    {
                        conn.Open();
                        string query = "UPDATE Gailuak SET egoera = @egoera WHERE id_gailua = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@egoera", egoeraBerria);
                        cmd.Parameters.AddWithValue("@id", idGailua);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Egoera ondo eguneratu da.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
            }
            else
            {
                MessageBox.Show("Hautatu lerro bat lehenik.");
            }
        }

        private void dvgMintegika_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // ID-rik ez badago, ezin da editatu (zutabe hutsak babestu)
            if (dvgMintegika.Rows[e.RowIndex].Cells["ID"].Value == null ||
                dvgMintegika.Rows[e.RowIndex].Cells["ID"].Value == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        private void dvgMintegika_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            KargatuEgoeraBalioakGrid();
        }
    }
}
