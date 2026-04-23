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
    public partial class EzabatutakoGailuak : FormBase
    {
        public EzabatutakoGailuak()
        {
            InitializeComponent();
        }

        private void EzabatutakoGailuak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            KargatuEzabatutakoak(); // Datuak kargatzeko funtzioa deitu
        }

        private void SARRERA_Click(object sender, EventArgs e)
        {

        }

        private void ATZERA_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }
        private void KargatuEzabatutakoak()
        {
            try
            {
                string konexioa = DbKonexioa.Instantzia.GetKonexioString();
                using (MySqlConnection conn = new MySqlConnection(konexioa))
                {
                    conn.Open();
                    string query = "SELECT id_ezabatua AS 'ID', identifikazio_kodea AS 'Kodea', mota AS 'Gailu-Mota', " +
                                   "marka_modeloa AS 'Modeloa', eroste_data AS 'Eroste_Data', " +
                                   "ezabatutako_eguna AS 'ezabatze_data' FROM Ezabatutakoak";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // 1. Datuak lotu
                    dgvEzabatuak.DataSource = dt;

                    // 2. MODUA EZARRI (Garrantzitsua: lehenago egin zutabeak manipulatu baino lehen)
                    dgvEzabatuak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // 3. ID-A EZKUTATU (Hau da funtzionatu behar duena)
                    if (dgvEzabatuak.Columns.Contains("ID"))
                    {
                        dgvEzabatuak.Columns["ID"].Visible = false;
                    }

                    // 4. Diseinu apur bat
                    dgvEzabatuak.ReadOnly = true;
                    dgvEzabatuak.AllowUserToAddRows = false;

                    // Kodea zutabea apur bat identifikagarriagoa izan dadin (hautazkoa)
                    if (dgvEzabatuak.Columns.Contains("Kodea"))
                    {
                        dgvEzabatuak.Columns["Kodea"].MinimumWidth = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }
    }
}
