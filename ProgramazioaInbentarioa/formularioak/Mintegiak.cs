using Inbentarioa.DatuBasie;
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

namespace Inbentarioa.formularioak
{
    public partial class Mintegiak : FormBase
    {
        public Mintegiak()
        {
            InitializeComponent();
        }

        private void Mintegiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Mintegiak kargatzeko metodoari deia=>
            konfiguratuBaimenak();
            KargatuMintegiak();
        }

        private void konfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                EZABATUTAKOAK.Enabled = false;
                btnEzabatu.Enabled = false;
            }
            else if (rola == "MintegiBurua")
            {
                EZABATUTAKOAK.Enabled = false;
                btnEzabatu.Enabled = false;
            }
        }

        private void KargatuMintegiak()
        {
            try
            {
                // Konexioa lortu
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // SQL kontsulta: id_mintegia eta izena bakarrik
                    // SQL kontsulta zuzendua: Gehitu M.id_mintegia AS 'ID'
                    string query = @"SELECT M.id_mintegia AS 'ID', 
                        M.izena AS 'Mintegiaren Izena', 
                        E.erabiltzailea AS 'Arduraduna' 
                    FROM mintegiak M
                    LEFT JOIN erabiltzaileak E ON M.id_arduraduna = E.id_erabiltzailea";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Datuak Grid-ean kargatu
                    dgvMintegiLista.DataSource = dt;

                    // ID zutabea ezkutatu (horrela Cells["ID"] erabili dezakezu baina ez da ikusten)
                    if (dgvMintegiLista.Columns.Contains("ID"))
                    {
                        dgvMintegiLista.Columns["ID"].Visible = false;
                    }

                    // --- Formatu txukuna emateko ---
                    dgvMintegiLista.ReadOnly = true;
                    dgvMintegiLista.AllowUserToAddRows = false;
                    dgvMintegiLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dgvMintegiLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvMintegiLista.MultiSelect = false; // Bakarrik banaka ezabatzeko (seguruagoa)

                    // ID zutabeari tamaina zehatza eman (txikiagoa)
                    if (dgvMintegiLista.Columns.Contains("ID"))
                    {
                        dgvMintegiLista.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvMintegiLista.Columns["ID"].Width = 60;
                        dgvMintegiLista.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak kargatzean: ");
            }
        }
        private void IRTEN_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        private void EZABATUTAKOAK_Click(object sender, EventArgs e)
        {
              this.Hide();
              MintegiaGehitu mintegiak = new MintegiaGehitu();
              mintegiak.ShowDialog();
              this.Close();
            //______________________________________
            /*
            // Formularioa ireki
            MintegiaGehitu gehituForm = new MintegiaGehitu();

            // ShowDialog-ek leihoa irekita mantentzen du bat-batean itxi gabe
            if (gehituForm.ShowDialog() == DialogResult.OK)
            {
                // Mintegi berria ondo gorde bada, zerrenda freskatu
                KargatuMintegiak();
            }
            // KONTUZ: Hemen ez jarri 'this.Close()' edo 'this.Hide()', 
            // bestela formulario nagusia itxi egingo da.
            */
        }



        private void dgvMintegiLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEzabatu_Click(object sender, EventArgs e)
        {

            if (dgvMintegiLista.SelectedRows.Count > 0)
            {
                // Kontuz: ziurtatu Grid-eko zutabearen izena "ID" dela
                int idMintegia = Convert.ToInt32(dgvMintegiLista.SelectedRows[0].Cells["ID"].Value);
                string izena = dgvMintegiLista.SelectedRows[0].Cells["Mintegiaren Izena"].Value.ToString();

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
                    {
                        conn.Open();

                        // 1. EGIAZTAPENA: Gailuak (id_mintegia zutabea badago gailuetan)
                        string sqlGailuak = "SELECT COUNT(*) FROM gailuak WHERE id_mintegia = @id";
                        MySqlCommand cmdGailuak = new MySqlCommand(sqlGailuak, conn);
                        cmdGailuak.Parameters.AddWithValue("@id", idMintegia);
                        int gailuKopuru = Convert.ToInt32(cmdGailuak.ExecuteScalar());

                        // 2. EGIAZTAPENA: Arduraduna (Erabiltzaileak taulan ez dagoenez id_mintegia, 
                        // begiratu dugu ea mintegi honek arduradunik esleituta duen)
                        string sqlArduraduna = "SELECT id_arduraduna FROM mintegiak WHERE id_mintegia = @id";
                        MySqlCommand cmdArduraduna = new MySqlCommand(sqlArduraduna, conn);
                        cmdArduraduna.Parameters.AddWithValue("@id", idMintegia);
                        object arduradunaObj = cmdArduraduna.ExecuteScalar();

                        // DBNull ez bada, esan nahi du erabiltzaile bat lotuta dagoela arduradun gisa
                        bool arduradunaDu = (arduradunaObj != null && arduradunaObj != DBNull.Value);

                        // BALDINTZA: Gailuak baditu EDO arduradun bat badu, ezin da ezabatu
                        if (gailuKopuru > 0 || arduradunaDu)
                        {
                            string arrazoia = gailuKopuru > 0 ? $"{gailuKopuru} gailu lotuta ditu." : "arduradun bat esleituta du (Erabiltzaile lotua).";
                            MessageBox.Show($"Ezin da '{izena}' mintegia ezabatu: {arrazoia}\n\nLehenik hustu behar duzu.",
                                            "Ekintza galarazita", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        // 3. EZABATZEA: Dena hutsik badago
                        var erantzuna = MessageBox.Show($"Ziur zaude '{izena}' mintegi HUTSA ezabatu nahi duzula?",
                                                        "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (erantzuna == DialogResult.Yes)
                        {
                            string sqlDel = "DELETE FROM mintegiak WHERE id_mintegia = @id";
                            MySqlCommand cmdDel = new MySqlCommand(sqlDel, conn);
                            cmdDel.Parameters.AddWithValue("@id", idMintegia);
                            cmdDel.ExecuteNonQuery();

                            MessageBox.Show("Mintegia ondo ezabatu da.");
                            KargatuMintegiak(); // Freskatu Grid-a
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea egiaztapenak egitean: " + ex.Message);
                }
            }
        }
    }
}
