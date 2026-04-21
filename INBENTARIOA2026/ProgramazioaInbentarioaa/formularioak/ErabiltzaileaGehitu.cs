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
    public partial class ErabiltzaileaGehitu : Form
    {
        public ErabiltzaileaGehitu()
        {
            InitializeComponent();
            // HEMEN deituko ditugu datuak kargatzeko funtzioak=>>
            KargatuMintegiakCombo();
            KargatuRolakEskuz();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Definir los colores del degradado usando códigos hexadecimales
            Color colorInicio = ColorTranslator.FromHtml("#C2CBED"); // Azul claro
            Color colorFin = ColorTranslator.FromHtml("#003FA1");    // Azul oscuro

            // Crear un pincel con degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, // Área donde se aplicará el degradado
                colorInicio,         // Color inicial
                colorFin,            // Color final
                LinearGradientMode.Horizontal)) // Dirección del degradado (horizontal)
            {
                // Rellenar el fondo del formulario con el degradado
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void ErabiltzaileaGehitu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        // Rolak eskuz kargatzeko (Ikt, MintegiBurua, Irakaslea)
        private void KargatuRolakEskuz()
        {
            cbArduraduna.Items.Clear();
            cbArduraduna.Items.Add("Ikt");
            cbArduraduna.Items.Add("MintegiBurua");
            cbArduraduna.Items.Add("Irakaslea");
            cbArduraduna.SelectedIndex = 0; // Lehenengoa hautatu lehenetsi gisa
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
            catch (Exception ex) { MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message); }
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
                    // 0 --- EGIAZTATU MINTEGIA LIBRE DAGOEN ---
                    if (cbArduraduna.SelectedItem.ToString() == "MintegiBurua")
                    {
                        string checkQuery = "SELECT id_arduraduna FROM mintegiak WHERE id_mintegia = @idMintegia";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@idMintegia", cbMintegia.SelectedValue);
                            object result = checkCmd.ExecuteScalar();

                            // DBNull.Value ez bada, esan nahi du jada ID bat daukala (arduradun bat du)
                            if (result != null && result != DBNull.Value)
                            {
                                MessageBox.Show("Errorea: Mintegi honek badu jada MintegiBurua esleituta.");
                                return; // Metodotik irten, ez dugu gordeko
                            }
                        }
                    }

                    // 1. Erabiltzailea sortu (txtPass erabiliz)
                    string query = "INSERT INTO erabiltzaileak (erabiltzailea, rola, pasahitza) VALUES (@izena, @rola, @pasahitza)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@izena", txtErIzena.Text);
                        cmd.Parameters.AddWithValue("@rola", cbArduraduna.SelectedItem.ToString());
                        // HEMEN: Lehen txtErIzena erabiltzen genuen, orain txtPass.Text
                        cmd.Parameters.AddWithValue("@pasahitza", txtPass.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. MintegiBurua bada, Mintegiak taula eguneratu
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
                    // 1. Zerrenda formularioa ireki
                    Erabiltzaileak zerrenda = new Erabiltzaileak();
                    zerrenda.Show();


                    this.DialogResult = DialogResult.OK; // Honek esaten dio 'using' horri datuak freskatu behar direla
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Errorea: " + ex.Message); }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
