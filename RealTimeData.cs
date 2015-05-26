using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApplication
{
    public class RealTimeData : StockMarket
    {
        //List of all observers
        private List<StockMarketDisplay> observers = new List<StockMarketDisplay>();
        //List of all companies
        public List<Company> companies = new List<Company>();

        public RealTimeData()
        {
        }
        //Used to add an observer to a RealTimeData instance
        public void register(StockMarketDisplay o)
        {
            observers.Add(o);
        }
        //Used to remove an observer from a RealTimeData instance
        public void unRegister(StockMarketDisplay o)
        {
            observers.Remove(o);
        }
        //Tell all registered observers to update
        public void notify()
        {
            //Orders could not be removed while iterating through a list, instead they were set to 0, they will be removed now
            foreach (Company company in companies)
            {
                //Remove all empty orders
                company.sellOrders.RemoveAll(item => item.orderSize == 0);
                //Remove all empty orders
                company.buyOrders.RemoveAll(item => item.orderSize == 0);
            }
            foreach (StockMarketDisplay o in observers)
                o.update(this); 
        }
    }
}
