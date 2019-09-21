using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geodezija.Kutevi;

namespace Geodezija.Tocke
{
    public class TockaGeo
    {
        public IRadian Sirina { get; set; }
        public IRadian Duzina { get; set; }
        public double?  Visina { get; set; }

        public Tocka3D Konverzija()
        {
            return new Tocka3D();
        }
    }
}
