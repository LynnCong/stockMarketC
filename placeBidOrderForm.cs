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
    public partial class placeBidOrderForm : Form
    {
        RealTimeData data;
        public placeBidOrderForm(RealTimeData data)
        {
            this.data = data;
            InitializeComponent();
            //Add companies to the drop down list
            foreach (Company company in data.companies)
            {
                comboBox1.Items.Add(company.companyName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Submit button is clicked
                float numShares = (float)Convert.ToDouble(this.numSharesInput.Text);
                float purchasePrice = (float)Convert.ToDouble(this.purchasePriceInput.Text);
                if (purchasePrice < 0)
                {
                    throw new System.ArgumentException("Share PRice must be positive", "original");
                }
                String name = comboBox1.Text;
                int numSold = 0;
                Boolean sale = false;
                foreach (Company company in data.companies)
                {
                    if (company.companyName.Equals(name))
                    {
                        //Check all sell orders to see if a buy can occur
                        foreach (SellOrder sellOrder in company.sellOrders)
                        {
                            //Check to see if the sale price is less than or equal to the buy price
                            if (sellOrder.orderPrice <= purchasePrice)
                            {
                                //If the sale has the same number of shares as the buy request
                                if (sellOrder.orderSize == numShares)
                                {
                                    sale = true;
                                    numSold += (int)numShares;
                                    numShares -= sellOrder.orderSize;
                                    sellOrder.orderSize = 0;
                                }
                                //If there are more shares for sale then the purchase request
                                else if (sellOrder.orderSize > numShares)
                                {
                                    sellOrder.orderSize -= numShares;
                                    sale = true;
                                    numSold += (int)numShares;
                                }
                                //There are more shares being bought then there are in the current sell offer
                                else if (sellOrder.orderSize < numShares)
                                {
                                    sale = true;
                                    numSold += (int)sellOrder.orderSize;
                                    //subtract the number of bid shares from the total being sold
                                    numShares -= sellOrder.orderSize;
                                    //Remove the buy order from the list because all have been sold
                                    sellOrder.orderSize = 0;
                                    //Allow continuation of the loop and look for another list with a usable price
                                }
                            }
                        }
                        //If there are still shares left to buy after checking all the current sales
                        if (numShares > 0)
                        {
                            company.buyOrders.Add(new BuyOrder(DateTime.Now, numShares, purchasePrice));
                            company.buyOrders.Sort();
                        }
                        //if a transaction has occured
                        if (sale)
                        {
                            company.setLastPrice(purchasePrice);
                            company.setVolume((int)(numSold));
                        }
                        //Do this even though no transaction has occured, items have been added to lists
                        numSharesInput.Clear();
                        purchasePriceInput.Clear();
                        data.notify();
                        return;
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Some fields contain improper values", "Stock Market Application");
            }
            numSharesInput.Clear();
            purchasePriceInput.Clear();
            data.notify();
        }
        //on cancel button click
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
