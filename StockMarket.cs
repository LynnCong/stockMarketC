using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApplication
{
    interface StockMarket
    {
        void register(StockMarketDisplay o); //used to add an observer to the subject
        void unRegister(StockMarketDisplay o); //used to remove an observer to the subject
        void notify(); //must be implemented to update all observers
    }
}
