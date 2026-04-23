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
    public partial class ErabiltzaileaGehitu : FormBase
    {
        public ErabiltzaileaGehitu()
        {
            InitializeComponent();
            // HEMEN deituko ditugu datuak kargatzeko funtzioak=>>
            KargatuMintegiakCombo();
            KargatuRolakEskuz();
            // Tabulazio ordena ezarri:
            txtErIzena.TabIndex = 0;   // 1.goa
            txtPass.TabIndex = 1;      // 2.goa
            cbMintegia.TabIndex = 2;   // 3.goa
            cbArduraduna.TabIndex = 3; // 4.goa
            btnGorde.TabIndex = 4;     // 5.goa
        }


        private void ErabiltzaileaGehitu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        // Rolak eskuz kargatzeko (Ikt, MintegiBurua, Irakaslea)
        private void KargatuRolakEskuz()
        {
            cbArduraduna.Items.Clear();
            cbArduraduna.Items.Add("Irakaslea"); // Orain hau da 0 indizea
            cbArduraduna.Items.Add("MintegiBurua");
            cbArduraduna.Items.Add("Ikt");
            cbArduraduna.SelectedIndex = 0;
        }
        // MINTEGIAK kargatu (Datu-basetik)
        private void KargatuMintegiakCombo()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string query = "SELECT id_mintegia, izena FROM mintegiak";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbMintegia.DataSource = dt;
                    cbMintegia.DisplayMember = "izena";
                    cbMintegia.ValueMember = "id_mintegia";
                }
            }
            catch (Exception ex) { MessageBox.Show("Errorea mintegiak kargatzean: "); }
        }
        //Kargatu arduradunak comboBox-a=>

        private void lblizena_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ATZERA_Click(object sender, EventArgs e)
        {
            // Ez ireki "new Erabiltzaileak()" hemen. 
            // Bakarrik itxi leiho hau, Cancel emaitzarekin.
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGorde_Click_1(object sender, EventArgs e)
        {
            // Balidazioa: izena eta pasahitza idatzi direla ziurtatu
            if (string.IsNullOrWhiteSpace(txtErIzena.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Mesedez, bete izena eta pasahitza.");
                return;
            }

            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // --- 1. EGIAZTATU ERABILTZAILE IZEN HORI JADA BADAGOEN ---
                    string sqlCheckUser = "SELECT COUNT(*) FROM erabiltzaileak WHERE erabiltzailea = @izena";
                    using (MySqlCommand cmdCheck = new MySqlCommand(sqlCheckUser, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@izena", txtErIzena.Text);
                        int existitzenDa = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (existitzenDa > 0)
                        {
                            MessageBox.Show("Errorea: '" + txtErIzena.Text + "' erabiltzaile izena jada hartuta dago.");
                            return;
                        }
                    }

                    // --- 2. EGIAZTATU MINTEGIA LIBRE DAGOEN (MintegiBurua bada) ---
                    if (cbArduraduna.SelectedItem.ToString() == "MintegiBurua")
                    {
                        string checkQuery = "SELECT id_arduraduna FROM mintegiak WHERE id_mintegia = @idMintegia";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@idMintegia", cbMintegia.SelectedValue);
                            object result = checkCmd.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                MessageBox.Show("Errorea: Mintegi honek badu jada MintegiBurua esleituta.");
                                return;
                            }
                        }
                    }

                    // --- 3. ERABILTZAILEA SORTU (GAKOA: id_mintegia gehitu dugu) ---
                    // SQL query-an id_mintegia gehitu dugu erabiltzaile bakoitza bere mintegiarekin lotzeko
                    string query = "INSERT INTO erabiltzaileak (erabiltzailea, rola, pasahitza, id_mintegia) VALUES (@izena, @rola, @pasahitza, @idMintegia)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@izena", txtErIzena.Text);
                        cmd.Parameters.AddWithValue("@rola", cbArduraduna.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@pasahitza", txtPass.Text);

                        // HEMEN HARTZEN DUGU COMBOBOX-EAN HAUTATUTAKO ID-A
                        cmd.Parameters.AddWithValue("@idMintegia", cbMintegia.SelectedValue);

                        cmd.ExecuteNonQuery();
                    }

                    // --- 4. MINTEGIBURUA BADA, MINTEGIAK TAULA EGUNERATU ---
                    if (cbArduraduna.SelectedItem.ToString() == "MintegiBurua")
                    {
                        string getID = "SELECT LAST_INSERT_ID()";
                        MySqlCommand cmdID = new MySqlCommand(getID, conn);
                        int berriaID = Convert.ToInt32(cmdID.ExecuteScalar());

                        string updateMintegia = "UPDATE mintegiak SET id_arduraduna = @idArduraduna WHERE id_mintegia = @idMintegia";
                        using (MySqlCommand cmdUpdate = new MySqlCommand(updateMintegia, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@idArduraduna", berriaID);
                            cmdUpdate.Parameters.AddWithValue("@idMintegia", cbMintegia.SelectedValue);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Erabiltzailea ondo gorde da!");

                    // Elkarrizketa-koadroari OK dela esan
                    this.DialogResult = DialogResult.OK;

                    // Formulario hau itxi (kontrola atzera bueltatuko da)
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Erroreaegon da saiatu berriro"); }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
