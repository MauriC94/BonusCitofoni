using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusCitofoni
{
    public sealed class BonusRequest
    {
        //Nome del cittadino
        public string Name { get; set; }
        //Indice ricchezza
        public int IR { get; set; }
        //IBAN
        public string IBAN { get; set; }
        //cifra richiesta
        public decimal AmountRequested { get; set; }

    }
}
