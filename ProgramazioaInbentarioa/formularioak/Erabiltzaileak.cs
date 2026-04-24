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
    public partial class Erabiltzaileak : FormBase
    {
        public Erabiltzaileak()
        {
            InitializeComponent();
            KargatuErabiltzaileak(); // Formularioa irekitzean datuak kargatu
        }

        private void Erabiltzaileak_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            konfiguratuBaimenak();
        }

        private void konfiguratuBaimenak()
        {
            string rola = Sarrera.Saioa.Rola;
            if (rola == "Irakaslea")
            {
                ErabBerriaSortu.Enabled = false;
                button2.Enabled = false;
                
            }
            else if (rola == "MintegiBurua")
            {
                ErabBerriaSortu.Enabled = false;
                button2.Enabled = false;
            }
        }

        // --- DATUAK KARGATZEKO METODOA ---
        private void KargatuErabiltzaileak()
        {
            try
            {
                // DBErabiltzaileak klaseari eskatzen dizkiogu datuak eta Grid-ari esleitu
                dgvErabiltzaileak.DataSource = DBErabiltzaileak.GetErabiltzaileGuztiakPOO();

                dgvErabiltzaileak.ReadOnly = true;
                dgvErabiltzaileak.AllowUserToAddRows = false;
                dgvErabiltzaileak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvErabiltzaileak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvErabiltzaileak.Columns.Contains("ID"))
                {
                    dgvErabiltzaileak.Columns["ID"].Visible = false;
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
            this.Hide(); // Zerrenda ezkutatu

            using (ErabiltzaileaGehitu gehituForm = new ErabiltzaileaGehitu())
            {
                // 'ShowDialog' lerroan programa gelditu egingo da 'Gehitu' itxi arte
                if (gehituForm.ShowDialog() == DialogResult.OK)
                {
                    KargatuErabiltzaileak(); // Datuak freskatu grid-ean
                }

                // GAKOA: Gehitu leihoa itxi denean, zerrenda BERRIRO ERAKUTSI
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvErabiltzaileak.SelectedRows.Count > 0)
            {
                try
                {
                    int idHautatua = Convert.ToInt32(dgvErabiltzaileak.SelectedRows[0].Cells["ID"].Value);

                    // 1. KONTROLA: Saioa hasita duen erabiltzailea?
                    if (idHautatua == Sarrera.Saioa.IdErabiltzailea)
                    {
                        MessageBox.Show("Ezin duzu zure burua ezabatu saioa hasita duzun bitartean.");
                        return;
                    }

                    // 2. KONTROLA (BERRIA): Mintegi baten arduraduna da?
                    // Deitu oraintxe sortu dugun metodoari
                    if (DBErabiltzaileak.MintegiarenArduradunaDa(idHautatua))
                    {
                        MessageBox.Show("Ezin da erabiltzailea ezabatu mintegi baten arduraduna delako. " +
                                        "Aldatu mintegi horren arduraduna lehenago.",
                                        "Ekintza galarazita", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    // 3. EZABATZEA (Dena ondo badago)
                    var erantzuna = MessageBox.Show("Ziur zaude erabiltzaile hau ezabatu nahi duzula?", "Berretsi", MessageBoxButtons.YesNo);
                    if (erantzuna == DialogResult.Yes)
                    {
                        if (DBErabiltzaileak.EzabatuErabiltzailea(idHautatua))
                        {
                            MessageBox.Show("Erabiltzailea ondo ezabatu da.");
                            KargatuErabiltzaileak(); // Grid-a freskatu
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea: " + ex.Message);
                }
            }
        }
    }
}
