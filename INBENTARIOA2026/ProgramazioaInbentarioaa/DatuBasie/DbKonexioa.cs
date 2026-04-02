using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbentarioa.DatuBasie
{
    internal class DbKonexioa
    {
        private static DbKonexioa instantzia = null;
        private static readonly object lockObj = new object();
        private string konexioString;

        private DbKonexioa()
        {
            // Konexio string-a hemen definitu
            konexioString = "Data Source=server;Initial Catalog=database;User ID=username;Password=password";
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

        public string KonexioString
        {
            get { return konexioString; }
        }
    }
}
