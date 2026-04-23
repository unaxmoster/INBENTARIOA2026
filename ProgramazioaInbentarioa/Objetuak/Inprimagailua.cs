using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarioa.Objetuak
{
    // Gailua klasetik herentzia hartzen du
    public class Inprimagailua : Gailua
    {
        // Inprimagailuen eremu espezifikoa (image_46d11f.png-n ikusten denez)
        private bool koloretakoa;

        public bool Koloretakoa
        {
            get { return koloretakoa; }
            set { koloretakoa = value; }
        }

        // Eraikitzailea (Constructor)
        // Oharra: Gailua klaseko oinarrizko datuak eta inprimagailuarenak batzen ditu
        public Inprimagailua(string kodea, string markaModeloa, int idMintegia, bool koloretakoa)
            : base(kodea, markaModeloa, idMintegia)
        {
            this.koloretakoa = koloretakoa;
        }

        // Eraikitzaile hutsa
        public Inprimagailua() : base() { }
    }
}
