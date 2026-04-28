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

      /*  private DbKonexioa()
        {
            // Zure konexio-katea
            // 172.22.160.1
            //Izenarekin Server=localhost;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;
            // konexioString = "Server=anarcocapitalista90;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;";
            konexioString = "Server=172.22.160.1;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;";

        }
      */
      private DbKonexioa()
        {
            // Lehenik, saiatu izenarekin
            string server = "anarcocapitalista90";

            // Ezin bada, erabili IPa
            try
            {
                var ips = System.Net.Dns.GetHostAddresses("anarcocapitalista90");
                if (ips.Length > 0)
                    server = ips[0].ToString();
            }
            catch
            {
                server = "172.22.160.1"; // IP finkoa
            }

            konexioString = $"Server={server};Database=inbentarioa2026;Uid=root2026;Pwd=root2026;";
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