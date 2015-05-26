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
    public partial class MarketByOrder : Form, StockMarketDisplay
    {
        String companyName = "";
        RealTimeData sub;
        public MarketByOrder(String companyName)
        {
            this.companyName = companyName;
            InitializeComponent();
        }
        //Remove this observer from the subject
        protected override void OnClosing(CancelEventArgs e)
        {
            sub.unRegister(this);
        }
        public void update(RealTimeData sub)
        {
            this.sub = sub;
            foreach (Company company in sub.companies)
            {
                if (companyName == company.companyName)
                {
                    //Clear previous entries from the dataGridView and reset heads?? Not sure how/why, but it prevents crashes...
                    dataGridView1.Rows.Clear();
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.ColumnHeadersVisible = true;
                    //Sort the buy orders according to its implementation of CompareTo
                    company.buyOrders.Sort();
                    foreach (BuyOrder buyOrder in company.buyOrders.Take(10)) //only take top 10
                    {
                        this.dataGridView1.Rows.Add(buyOrder.orderSize,buyOrder.orderPrice);
                    }
                    dataGridView2.Rows.Clear();
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.ColumnHeadersVisible = true;
                    //Sort the sell orders according to its implementation of CompareTo
                    company.sellOrders.Sort();
                    foreach (SellOrder sellOrder in company.sellOrders.Take(10)) //only take top 10
                    {
                        this.dataGridView2.Rows.Add(sellOrder.orderPrice, sellOrder.orderSize);
                    }
                } 
            }
        }
    }
}
