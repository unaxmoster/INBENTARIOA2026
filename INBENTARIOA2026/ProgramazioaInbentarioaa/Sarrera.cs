using Inbentarioa.DatuBasie;
using Inbentarioa.formularioak;
using Inventarioa.formularioak;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;

namespace Inbentarioa
{
    public partial class Sarrera : FormBase
    {
        public static class Saioa
        {
            public static int IdErabiltzailea { get; set; }
            public static string Erabiltzailea { get; set; }
            public static string Rola { get; set; }
        }
        public Sarrera()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Opción 1: Maximizar la ventana (recomendada)
            this.WindowState = FormWindowState.Maximized;
        }

        private void Sarrera_kargatu(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Klaseari deitzen diogu login-a egiteko
                var erabiltzailea = DBErabiltzaileak.Login(textizena.Text, textpass.Text);

                if (erabiltzailea != null)
                {
                    // Saioaren datu estatikoak bete
                    Saioa.IdErabiltzailea = erabiltzailea.IdErabiltzailea;
                    Saioa.Rola = erabiltzailea.Rola;
                    Saioa.Erabiltzailea = erabiltzailea.Izena;

                    using (ByteGuardians ongiEtorri = new ByteGuardians("Ongi etorri, " + Saioa.Erabiltzailea))
                    {
                        ongiEtorri.ShowDialog();
                    }

                    // Leihoa itxi bezain lasar Menua irekiko da
                    Menua menua = new Menua();
                    this.Hide(); // Sarrera ezkutatu (ez itxi!)
                    menua.Show(); // ShowDialog() ordez Show() erabili, katea ez blokeatzeko
                }
                else
                {
                    MessageBox.Show("Erabiltzailea edo pasahitza okerra da.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
