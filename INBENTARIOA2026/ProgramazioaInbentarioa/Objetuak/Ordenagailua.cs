using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarioa.Objetuak
{
    // KLASE BAKARRA ETA GARBIA
    public class Ordenagailua : Gailua
    {
        public string Ram { get; set; }
        public string Rom { get; set; }
        public string Cpu { get; set; }

        // Eraikitzailea: Gurasoaren datuak (base) eta bereak hartzen ditu
        public Ordenagailua(string kodea, string modeloa, int mintegia, string ram, string rom, string cpu)
            : base(kodea, modeloa, mintegia)
        {
            this.Ram = ram;
            this.Rom = rom;
            this.Cpu = cpu;
        }
    }
}
