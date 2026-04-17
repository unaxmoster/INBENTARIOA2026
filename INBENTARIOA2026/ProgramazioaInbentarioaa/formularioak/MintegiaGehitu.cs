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
    public partial class MintegiaGehitu : Form
    {
        public MintegiaGehitu()
        {
            InitializeComponent();
            // ComboBox-a betetzeko metodoari deeia
            KargatuArduradunLibreak();
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

        private void KargatuArduradunakCombo()
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                // Egiaztatu zure 'erabiltzaileak' taulako zutabe izen zuzena (izena, erabiltzailea...)
                string sql = "SELECT id_erabiltzailea, izena FROM erabiltzaileak";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbArduraduna.DataSource = dt;
                cmbArduraduna.DisplayMember = "izena";           // Ikusten den testua
                cmbArduraduna.ValueMember = "id_erabiltzailea";  // Atzean gordetzen den IDa
            }
        }
        private void KargatuArduradunLibreak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    // Kontsulta honek: 
                    // 1. MintegiBurua direnak soilik hartzen ditu.
                    // 2. Jada mintegi bat esleituta dutenak kanpoan uzten ditu.
                    string sql = @"SELECT id_erabiltzailea, erabiltzailea 
                           FROM erabiltzaileak 
                           WHERE rola = 'MintegiBurua' 
                           AND id_erabiltzailea NOT IN (SELECT id_arduraduna FROM mintegiak WHERE id_arduraduna IS NOT NULL)";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbArduraduna.DataSource = dt;
                    cmbArduraduna.DisplayMember = "erabiltzailea";   // ComboBox-ean ikusiko dena
                    cmbArduraduna.ValueMember = "id_erabiltzailea"; // DBan gordeko den IDa

                    // Hautapenik gabe hasteko (opcionala)
                    cmbArduraduna.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea arduradunak kargatzean: " + ex.Message);
            }
        }
        private void MintegiaGehitu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void IRTEN_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mintegiak mintegiak = new Mintegiak();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void EZABATUTAKOAK_Click(object sender, EventArgs e)
        {// Balidazio txiki bat: izena eta arduraduna hautatuta daudela ziurtatu
            if (string.IsNullOrEmpty(txtMintegiIzena.Text) || cmbArduraduna.SelectedValue == null)
            {
                MessageBox.Show("Mesedez, bete datu guztiak.");
                return;
            }

            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                try
                {
                    conn.Open();
                    // ID-A KENDUTA, HORRELA GERATU BEHAR DU:
                    string sql = "INSERT INTO mintegiak (izena, id_arduraduna) VALUES (@izena, @idArduraduna)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@izena", txtMintegiIzena.Text);
                    cmd.Parameters.AddWithValue("@idArduraduna", cmbArduraduna.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Mintegia ondo gorde da!");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea gordetzean: " + ex.Message);
                }
            }
        }
    }
}
