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
    public partial class MintegiaGehitu : FormBase
    {
        public MintegiaGehitu()
        {
            InitializeComponent();
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
        {// 1. Balidazioak: Testu kutxak hutsik ez egotea
            if (string.IsNullOrWhiteSpace(txtMintegiIzena.Text) ||
                string.IsNullOrWhiteSpace(txtErab.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Mesedez, bete eremu guztiak: Mintegia, Erabiltzailea eta Pasahitza.");
                return;
            }

            try
            {
                // 2. Klaseko metodoari deitu (Logika guztia han dago)
                bool ondo = DBMintegiak.MintegiaEtaBuruaSortu(
                    txtMintegiIzena.Text.Trim(),
                    txtErab.Text.Trim(),
                    txtPass.Text.Trim()
                );

                if (ondo)
                {
                    MessageBox.Show("Mintegia eta bere arduraduna ondo sortu dira!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062) // 1062 = Duplicate entry (erabiltzaile izena hartuta badago)
            {
                MessageBox.Show("Errorea: Erabiltzaile izen hori jada existitzen da.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore ezezagun bat gertatu da: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
