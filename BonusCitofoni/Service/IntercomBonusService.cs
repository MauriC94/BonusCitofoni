using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusCitofoni
{
    public interface IntercomBonusService
    {
            BonusResponse ProcessRequest(BonusRequest request);
    }
}
