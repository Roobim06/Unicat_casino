using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicat_Casino
{
    class karty
    {
        public string symbol;
        public string numer;
        public string zdj;
        public bool odkryte;
        public karty(string symbol, string numer, string zdj, bool odkryte)
        {
            this.symbol = symbol;
            this.numer = numer;
            this.zdj = zdj;
            this.odkryte = odkryte;
        }
    }
}
