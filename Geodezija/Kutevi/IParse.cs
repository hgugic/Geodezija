using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodezija.Kutevi
{
    public interface IParse<T> where T : class
    {
         T Parse(string s);
        bool TryParse(string s, out T result);
    }
}
