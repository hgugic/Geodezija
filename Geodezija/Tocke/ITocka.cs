using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Tocke
{
    public interface ITocka
    {
        /// <summary>
        /// Vrijednost koordinate na y-osi 
        /// </summary>
        double Y { get; set; }

        /// <summary>
        /// Vrijednost koordinate na x-osi 
        /// </summary>
        double X { get; set; }
    }
}
