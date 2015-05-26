using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApplication
{
    public interface StockMarketDisplay
    {
         void update(RealTimeData s); //forces each observer to implement the update function, to change their data regardless of the model
    }
}
