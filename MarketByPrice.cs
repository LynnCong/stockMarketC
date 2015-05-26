using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockMarketApplication
{
    public partial class MarketByPrice : Form, StockMarketDisplay
    {
        String companyName;
        RealTimeData sub;
        List<listItem> entries = new List<listItem>(); //List of listItems that will be added to the dataGridView
        List<float> usedPrices = new List<float>(); //List of prices that have already been compressed
        //Set company name as a property of this MDI window
        public MarketByPrice(String companyName)
        {
            this.companyName = companyName;
            InitializeComponent();
        }
        //On close, remove this observer from the subject's list of observers
        protected override void OnClosing(CancelEventArgs e)
        {
            sub.unRegister(this); 
        }
        //This is called upon notifying the subject that the data has changed
        public void update(RealTimeData sub)
        {
            this.sub = sub;
            foreach (Company company in sub.companies)
            {
                if (companyName == company.companyName)
                {
                    //BUY ORDERS
                    //Clear existing entries from the datagridview for bids
                    dataGridView1.Rows.Clear();
                    foreach (BuyOrder order in company.buyOrders)
                    {
                        int numOccurs = 0;
                        if (!usedPrices.Contains(order.orderPrice))
                        {
                            //For each order, traverse the rest of the list and see if there are any others with the same price,
                            //If there are, increment numOccurs, add volume together
                            usedPrices.Add(order.orderPrice);
                            int totalVolume = 0;
                            foreach (BuyOrder CompressedOrder in company.buyOrders.Skip(0))
                            {
                                if (CompressedOrder.orderPrice == order.orderPrice) //if current iterated order has the same price as exterior order
                                {
                                    numOccurs++; //increase the occurances of an order with this price
                                    totalVolume += (int)CompressedOrder.orderSize; //add the volume of orders with this price
                                }
                            }
                            //create a new instance of helper class listItem with the number of occurances, the price and the volume
                            entries.Add(new listItem(numOccurs, order.orderPrice, totalVolume));
                            numOccurs = 0; //reset
                            totalVolume = 0; //reset
                        }
                    }
                    //Add each list item to the datagrid view, should already be properly sorted
                    foreach (listItem entry in entries.Take(10)) //only take top 10
                    {
                        this.dataGridView1.Rows.Add(entry.numOccurs, entry.orderSize, entry.orderPrice);
                    }
                    entries.Clear(); //Clear entries so that the same list can be used for sell orders now
                    usedPrices.Clear(); //clear list, same reasoning
                    //SELL ORDERS
                    //Clear existing entries from the datagridview for bids
                    dataGridView2.Rows.Clear();
                    foreach (SellOrder order in company.sellOrders)
                    {
                        int numOccurs = 0;
                        if (!usedPrices.Contains(order.orderPrice))
                        {
                            //For each order, traverse the rest of the list and see if there are any others with the same price,
                            //If there are, increment numOccurs, add volume together
                            usedPrices.Add(order.orderPrice);
                            int totalVolume = 0;
                            foreach (SellOrder CompressedOrder in company.sellOrders.Skip(0))
                            {
                                if (CompressedOrder.orderPrice == order.orderPrice) //if current iterated order has the same price as exterior order
                                {
                                    numOccurs++;
                                    totalVolume += (int)CompressedOrder.orderSize;
                                }
                            }
                            entries.Add(new listItem(numOccurs, order.orderPrice, totalVolume));
                            numOccurs = 0;
                            totalVolume = 0;
                        }
                    }
                    //add all sell orderst that have been grouped, should already be sorted
                    foreach (listItem entry in entries.Take(10)) //take top 10
                    {
                        this.dataGridView2.Rows.Add(entry.orderPrice, entry.orderSize, entry.numOccurs);
                    }
                    entries.Clear();
                    usedPrices.Clear();
                }
            }
        }
    }
    //listitem is a class that keeps data of a compressed list, mostly a simplified order but with a number of occurances
    public class listItem
    {
        public int numOccurs;
        public float orderPrice;
        public float orderSize;

        public listItem(int numOccurs, float price, float size)
        {
            this.numOccurs = numOccurs;
            this.orderPrice = price;
            this.orderSize = size;
        }
    }
}
