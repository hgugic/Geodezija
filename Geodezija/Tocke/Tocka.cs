using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Tocke
{
    public class Tocka : TockaProjekcija
    {
        public double h { get; set; }

        public Tocka(double y, double x, double h) : base(y, x)
        {
            this.h = h;
        }
    }
}
