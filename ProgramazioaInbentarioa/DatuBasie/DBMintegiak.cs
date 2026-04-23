using System.Data;
using MySql.Data.MySqlClient;

namespace Inbentarioa.DatuBasie
{
    internal class DBMintegiak
    {
        public static DataTable GetMintegiakCombo()
        {
            string sql = "SELECT id_mintegia, izena FROM mintegiak";
            return ExekutatuQuery(sql);
        }

        public static bool GehituMintegia(string izena, int idArduraduna)
        {
            string sql = "INSERT INTO mintegiak (izena, id_arduraduna) VALUES (@izena, @idArduraduna)";
            MySqlParameter[] ps = {
                new MySqlParameter("@izena", izena),
                new MySqlParameter("@idArduraduna", idArduraduna)
            };
            // ExekutatuNonQuery metodoa erabiliz (lehengo klaseko antzekoa)
            return true;
        }

        private static DataTable ExekutatuQuery(string sql)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            return dt;
        }
        public static bool MintegiaEtaBuruaSortu(string mintegiIzena, string erabIzena, string pasahitza)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();

            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                // Transakzioa hasi dena ondo doala ziurtatzeko
                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Mintegia sortu
                    string sqlMintegia = "INSERT INTO mintegiak (izena) VALUES (@mIzena)";
                    MySqlCommand cmdM = new MySqlCommand(sqlMintegia, conn, trans);
                    cmdM.Parameters.AddWithValue("@mIzena", mintegiIzena);
                    cmdM.ExecuteNonQuery();

                    // 2. Sortu berri den Mintegiaren IDa lortu
                    long mintegiId = cmdM.LastInsertedId;

                    // 3. Erabiltzailea sortu (MintegiBurua rola eta id_mintegia lotuz)
                    string sqlErabiltzailea = @"INSERT INTO erabiltzaileak (erabiltzailea, rola, pasahitza, id_mintegia) 
                                      VALUES (@uIzena, 'MintegiBurua', @uPass, @uMintegiId)";

                    MySqlCommand cmdU = new MySqlCommand(sqlErabiltzailea, conn, trans);
                    cmdU.Parameters.AddWithValue("@uIzena", erabIzena);
                    cmdU.Parameters.AddWithValue("@uPass", pasahitza); // Gogoratu segurtasunagatik hash-a gomendagarria dela
                    cmdU.Parameters.AddWithValue("@uMintegiId", mintegiId);
                    cmdU.ExecuteNonQuery();

                    // 4. Sortu berri den Erabiltzailearen IDa lortu mintegian arduradun gisa jartzeko
                    long erabiltzaileId = cmdU.LastInsertedId;

                    // 5. Mintegia eguneratu arduradunaren IDarekin (id_arduraduna)
                    string sqlUpdateMintegia = "UPDATE mintegiak SET id_arduraduna = @eId WHERE id_mintegia = @mId";
                    MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdateMintegia, conn, trans);
                    cmdUpdate.Parameters.AddWithValue("@eId", erabiltzaileId);
                    cmdUpdate.Parameters.AddWithValue("@mId", mintegiId);
                    cmdUpdate.ExecuteNonQuery();

                    // Dena ondo badoa, aldaketak berretsi
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // Errore bat badago, dena atzera bota (Rollback)
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}