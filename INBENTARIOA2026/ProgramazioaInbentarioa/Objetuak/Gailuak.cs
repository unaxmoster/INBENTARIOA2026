using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarioa.Objetuak
{
   
        public class Gailua
        {
            public int Id { get; set; }
            public string IdentifikazioKodea { get; set; }
            public string MarkaModeloa { get; set; }
            public int IdMintegia { get; set; }
            public DateTime ErosteData { get; set; }
            public int Egoera { get; set; } // 0: Ondo, 1: Hondatuta, 2: Konpontzen

            // Eraikitzaile kargatua
            public Gailua(string kodea, string modeloa, int mintegia, int egoera = 0)
            {
                this.IdentifikazioKodea = kodea;
                this.MarkaModeloa = modeloa;
                this.IdMintegia = mintegia;
                this.ErosteData = DateTime.Now;
                this.Egoera = egoera;
            }

            public Gailua() { } // Eraikitzaile hutsa (beharrezkoa batzuetan)
        }
    
}
