using System;
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
            // MySQL-rako sintaxi zuzena (zure zerbitzariko datuekin ordezkatu):
            //konexioString = "Server=localhost;Database=inbentarioa2026;Uid=root;Pwd=root;";

            konexioString = "Server=anarcocapitalista90;Database=inbentarioa2026;Uid=root2026;Pwd=root2026;";

            //   anarcocapitalista90

            // OHARRA: 'root' erabili ohi da MySQL-n defektuz, 
            // eta pasahitza instalazioan jarri zenuena da.
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
    }
}


/*
 
Nola erabili beste klase batean?
Datuak irakurri nahi dituzunean, honela deituko zenioke:

C#
using MySql.Data.MySqlClient;

// ... formularioaren barruan ...
using (MySqlConnection conn = new MySqlConnection(DbKonexioa.Instantzia.GetKonexioString()))
{
    conn.Open();
    // Hemen zure SQL aginduak...
}

 */