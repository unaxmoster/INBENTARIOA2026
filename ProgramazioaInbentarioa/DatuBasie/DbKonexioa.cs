using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Inbentarioa.DatuBasie
{
    internal class DbKonexioa
    {
        private static DbKonexioa instantzia = null;
        private static readonly object lockObj = new object();
        private string konexioString;

        private DbKonexioa()
        {
            // Konexio-katea
            
            //Izenarekin Server=localhost;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;
             konexioString = "Server=anarcocapitalista90;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;";
            
            //konexioString = "Server=10.33.28.85;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;";

        }
    

        public static DbKonexioa Instantzia
        {
            get
            {
                if (instantzia == null)
                {
                    lock (lockObj)
                    {
                        if (instantzia == null)
                        {
                            instantzia = new DbKonexioa();
                        }
                    }
                }
                return instantzia;
            }
        }

        public string GetKonexioString()
        {
            return konexioString;
        }

        // MySqlConnection lortu
        public MySqlConnection LortuKonexioa()
        {
            MySqlConnection konexioa = new MySqlConnection(konexioString);
            konexioa.Open();
            return konexioa;
        }

        // ExecuteQuery - SELECT kontsultetarako (DataTable itzultzen du)
        public DataTable ExecuteQuery(string query, MySqlParameter[] parametroak = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection konexioa = LortuKonexioa())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, konexioa))
                {
                    if (parametroak != null)
                    {
                        cmd.Parameters.AddRange(parametroak);
                    }
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // ExecuteNonQuery - INSERT, UPDATE, DELETE kontsultetarako
        public int ExecuteNonQuery(string query, MySqlParameter[] parametroak = null)
        {
            using (MySqlConnection konexioa = LortuKonexioa())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, konexioa))
                {
                    if (parametroak != null)
                    {
                        cmd.Parameters.AddRange(parametroak);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // ExecuteScalar - balio bakar bat itzultzen duten kontsultetarako (adibidez COUNT)
        public object ExecuteScalar(string query, MySqlParameter[] parametroak = null)
        {
            using (MySqlConnection konexioa = LortuKonexioa())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, konexioa))
                {
                    if (parametroak != null)
                    {
                        cmd.Parameters.AddRange(parametroak);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}