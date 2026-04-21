using Inbentarioa.DatuBasie;
using Inventarioa.formularioak;
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
    public partial class Erabiltzaileak : Form
    {
        public Erabiltzaileak()
        {
            InitializeComponent();
            KargatuErabiltzaileak(); // Formularioa irekitzean datuak kargatu
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

        private void Erabiltzaileak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        // --- DATUAK KARGATZEKO METODOA ---
        private void KargatuErabiltzaileak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();

                    // SQL: Erabiltzailea eta Rola bakarrik hautatzen ditugu
                    // Ziurtatu zure taulako zutabeak 'erabiltzailea' eta 'rola' deitzen direla
                    string query = "SELECT id_erabiltzailea AS 'ID', erabiltzailea AS 'Erabiltzailea', rola AS 'Rola' FROM Erabiltzaileak";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // dgvErabiltzaileak da zure Grid-aren izena
                    dgvErabiltzaileak.DataSource = dt;

                    // --- DISEINUA ETA KONFIGURAZIOA ---
                    dgvErabiltzaileak.ReadOnly = true; // Erabiltzaileak ezin du grid-ean zuzenean idatzi
                    dgvErabiltzaileak.AllowUserToAddRows = false; // Lerro huts automatikoa kendu
                    dgvErabiltzaileak.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Lerro osoa hautatu klik egitean

                    // Zabalera grid osora egokitu
                    dgvErabiltzaileak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // ID zutabea ezkutatu (barnean kudeatzeko erabilgarria da, baina ez dugu erakutsi behar)
                    if (dgvErabiltzaileak.Columns.Contains("ID"))
                    {
                        dgvErabiltzaileak.Columns["ID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea erabiltzaileak kargatzean: " + ex.Message);
            }
        }
        private void ATZERA_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Uneko formularioa (zerrenda) ezkutatu besterik ez
            this.Hide();

            using (ErabiltzaileaGehitu gehituForm = new ErabiltzaileaGehitu())
            {
                // 2. Gehitu formularioa ireki eta itxaron
                if (gehituForm.ShowDialog() == DialogResult.OK)
                {
                    // --- GAKOA HEMEN DAGO ---
                    // Ez dugu 'new Erabiltzaileak()' sortu behar.
                    // Uneko formularioa (this) erabiliko dugu berriro.

                    this.Show(); // Zerrenda berriro erakutsi
                    KargatuErabiltzaileak(); // Grid-a freskatu datu berriekin
                }
                else
                {
                    // Erabiltzaileak ezer gorde gabe itxi badu (Cancel), erakutsi berriro freskatu gabe
                    this.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvErabiltzaileak.SelectedRows.Count > 0)
            {
                var erantzuna = MessageBox.Show("Ziur zaude erabiltzaile hau ezabatu nahi duzula?", "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        int idErabiltzailea = Convert.ToInt32(dgvErabiltzaileak.SelectedRows[0].Cells["ID"].Value);
                        string konexioa = DbKonexioa.Instantzia.GetKonexioString();

                        using (MySqlConnection conn = new MySqlConnection(konexioa))
                        {
                            conn.Open();
                            string sql = "DELETE FROM Erabiltzaileak WHERE id_erabiltzailea = @id";
                            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", idErabiltzailea);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Erabiltzailea ondo ezabatu da.");
                            }
                        }
                        KargatuErabiltzaileak(); // Zerrenda freskatu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errorea ezabatzean: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Mesedez, hautatu erabiltzaile bat zerrendatik.");
            }
        }
    }
}
