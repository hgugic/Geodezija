using Geodezija.Kutevi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Tocke
{
    public interface ITockaProjekcija : ITocka
    {
        Radians SmjerniKut(ITocka vizura);
        double Duzina(ITocka vizura);
    }
}
