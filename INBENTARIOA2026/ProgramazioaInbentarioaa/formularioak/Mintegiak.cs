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
    public partial class Mintegiak : Form
    {
        public Mintegiak()
        {
            InitializeComponent();
        }

        private void Mintegiak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Mintegiak kargatzeko metodoari deia=>
            KargatuMintegiak();
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
                MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message);
            }
        }
        private void IRTEN_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menua mintegiak = new Menua();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void GAILUAK_Click(object sender, EventArgs e)
        {
            this.Hide();
            MintegiLista mintegiak = new MintegiLista();
            mintegiak.ShowDialog();
            this.Close();
        }

        private void EZABATUTAKOAK_Click(object sender, EventArgs e)
        {
            //  this.Hide();
            //  MintegiaGehitu mintegiak = new MintegiaGehitu();
            //  mintegiak.ShowDialog();
            // this.Close();
            //______________________________________
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
        }



        private void dgvMintegiLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEzabatu_Click(object sender, EventArgs e)
        {
            if (dgvMintegiLista.SelectedRows.Count > 0)
            {
                int idMintegia = Convert.ToInt32(dgvMintegiLista.SelectedRows[0].Cells["ID"].Value);
                string izena = dgvMintegiLista.SelectedRows[0].Cells["Mintegiaren Izena"].Value.ToString();

                var erantzuna = MessageBox.Show($"Ziur zaude '{izena}' eta bere gailuak historikora mugitu nahi dituzula?",
                    "Berretsi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (erantzuna == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
                        {
                            conn.Open();
                            using (MySqlTransaction trans = conn.BeginTransaction())
                            {
                                try
                                {
                                    string insertHistory = @"
                                        INSERT INTO Ezabatutakoak (id_gailua, marka_modeloa, mota, eroste_data)
                                        SELECT 
                                            g.id_gailua, 
                                            IFNULL(g.marka_modeloa, 'Ezezaguna')as marka_modeloa,
                                            (CASE 
                                                 WHEN o.id_gailua IS NOT NULL THEN 'Ordenagailua' 
                                                 WHEN i.id_gailua IS NOT NULL THEN 'Inprimagailua' 
                                                 ELSE 'Besterik' 
                                             END) as mota,
                                             g.eroste_data
                                        FROM gailuak g
                                        LEFT JOIN ordenagailuak o ON g.id_gailua = o.id_gailua
                                        LEFT JOIN inprimagailuak i ON g.id_gailua = i.id_gailua
                                        WHERE g.id_mintegia = @idMin";


                                    MySqlCommand cmdHistory = new MySqlCommand(insertHistory, conn, trans);
                                    cmdHistory.Parameters.AddWithValue("@idMin", idMintegia);


                                    // DEBUG: Ikusi nahi dugu ea zerbait kopiatzen duen
                                    int kopurua = cmdHistory.ExecuteNonQuery();

                                    if (kopurua == 0)
                                    {
                                        // Kontuz! Hemen badakigu gailuak ez direla kopiatu
                                        // Mezua aterako dugu jakiteko
                                        MessageBox.Show("Abisua: Mintegi honek ez du gailurik kopiatzeko.");
                                    }
                                    else
                                    {
                                        // 2. URRATSA: EZABATU UMEAK (Letra xehez)
                                        string delUmeak = @"
                                        DELETE FROM ordenagailuak WHERE id_gailua IN (SELECT id_gailua FROM gailuak WHERE id_mintegia = @id);
                                        DELETE FROM inprimagailuak WHERE id_gailua IN (SELECT id_gailua FROM gailuak WHERE id_mintegia = @id);
                                        DELETE FROM hondatutakoak WHERE id_gailua IN (SELECT id_gailua FROM gailuak WHERE id_mintegia = @id);";
                                        
                                        MySqlCommand cmdDelUmeak = new MySqlCommand(delUmeak, conn, trans);
                                        cmdDelUmeak.Parameters.AddWithValue("@id", idMintegia);
                                        cmdDelUmeak.ExecuteNonQuery();
                                   
                                        // 3. URRATSA: EZABATU GURASOAK ETA MINTEGIA
                                        MySqlCommand cmdDelGailuak = new MySqlCommand("DELETE FROM gailuak WHERE id_mintegia = @id", conn, trans);
                                        cmdDelGailuak.Parameters.AddWithValue("@id", idMintegia);
                                        cmdDelGailuak.ExecuteNonQuery();
                                    }
                                    MySqlCommand cmdDelMintegia = new MySqlCommand("DELETE FROM mintegiak WHERE id_mintegia = @id", conn, trans);
                                        cmdDelMintegia.Parameters.AddWithValue("@id", idMintegia);
                                        cmdDelMintegia.ExecuteNonQuery();
                                    

                                    trans.Commit();
                                    MessageBox.Show($"{kopurua} gailu historikora mugitu dira eta mintegia ezabatu da.");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("ERROR INSERT: " + ex.ToString());
                                    trans.Rollback();
                                    // trans.Rollback();
                                    throw ex;
                                }
                            }
                        }
                        KargatuMintegiak();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore teknikoa: " + ex.Message);
                    }
                }
            }
        }
    }
}
