using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Inbentarioa.DatuBasie
{
    internal class DBErabiltzaileak
    {
        public static Erabiltzailea Login(string izena, string pasahitza)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                try
                {
                    conn.Open();
                    // 1. ZUZENKETA: Gehitu 'id_mintegia' SELECT kontsultan
                    string sql = "SELECT id_erabiltzailea, rola, erabiltzailea, id_mintegia FROM erabiltzaileak WHERE erabiltzailea = @user AND pasahitza = @pass";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", izena);
                    cmd.Parameters.AddWithValue("@pass", pasahitza);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Erabiltzailea
                            {
                                IdErabiltzailea = reader.GetInt32("id_erabiltzailea"),
                                Rola = reader.GetString("rola"),
                                Izena = reader.GetString("erabiltzailea"),

                                // 2. ZUZENKETA: Balioa irakurri. Kontuz NULL balioekin!
                                IdMintegia = reader.IsDBNull(reader.GetOrdinal("id_mintegia"))
                                             ? (int?)null
                                             : reader.GetInt32("id_mintegia")
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Errorea datu-basean: " + ex.Message);
                }
            }
            return null;
        }

        // --- GEHITU METODO HAUEK ---

        public static DataTable GetErabiltzaileGuztiakPOO()
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                string query = @"SELECT e.id_erabiltzailea AS 'ID', 
                        e.erabiltzailea AS 'Erabiltzailea', 
                        e.rola AS 'Rola', 
                        m.izena AS 'Mintegia'
                 FROM erabiltzaileak e
                 LEFT JOIN mintegiak m ON e.id_mintegia = m.id_mintegia";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static bool EzabatuErabiltzailea(int id)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                string sql = "DELETE FROM Erabiltzaileak WHERE id_erabiltzailea = @id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool MintegiakBaduBurua(int idMintegia)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM erabiltzaileak WHERE id_mintegia = @idMin AND rola = 'MintegiBurua'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idMin", idMintegia);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        public static bool MintegiarenArduradunaDa(int idErabiltzailea)
        {
            string konexioa = DbKonexioa.Instantzia.GetKonexioString();
            using (MySqlConnection conn = new MySqlConnection(konexioa))
            {
                conn.Open();
                // Begiratu ea id hau mintegiak taulako id_arduraduna zutabean dagoen
                string sql = "SELECT COUNT(*) FROM mintegiak WHERE id_arduraduna = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", idErabiltzailea);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
    }

    public class Erabiltzailea
    {
        public int IdErabiltzailea { get; set; }
        public string Izena { get; set; }
        public string Rola { get; set; }
        public int? IdMintegia { get; set; } // Nullable izan daiteke IKT denean adibidez
    }

}