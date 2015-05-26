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
    public partial class placeAskOrderForm : Form
    {
        RealTimeData data; //used to modify the subject
        public placeAskOrderForm(RealTimeData data)
        {
            this.data = data;
            InitializeComponent();
            //populate the dropdown company list from the companies in the subject
            foreach (Company company in data.companies)
            {
                comboBox1.Items.Add(company.companyName);
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            //Validation is handled for any inputs that cannot be parsed into what they should be, excpetion is thrown
            try
            {
                //Submit button is clicked
                //get number of shares input and parse
                float numShares = (float)Convert.ToDouble(this.numSharesInput.Text);
                //get purchase price from form and parse
                float purchasePrice = (float)Convert.ToDouble(this.purchasePriceInput.Text);
                //make sure purchase price is a positive value
                if (purchasePrice < 0)
                {
                    throw new System.ArgumentException("Share Price must be positive", "Place Bid Order Form");
                }
                //get company name from the dropdown
                String name = comboBox1.Text;
                int numSold = 0; //reset tranasction number
                Boolean sale = false; //has a transaction occured
                foreach (Company company in data.companies)
                {
                    if (company.companyName.Equals(name))
                    {
                        //Insert logic here to compress orders together
                        //if there are orders in the sell list that are the same price or greater as an order in the buy list, 
                        //make the sell, record dumber of shares sold and the sell price.
                        //Update the buy list and sell list
                        foreach (BuyOrder buyOrder in company.buyOrders)
                        {
                            //Check and see if the sale price is less than or equal to the current bid price
                            if (buyOrder.orderPrice >= purchasePrice)
                            {
                                //if the buy order has the same number of shares as the sell order
                                if (buyOrder.orderSize == numShares)
                                {
                                    sale = true;
                                    numSold += (int)numShares;
                                    numShares -= buyOrder.orderSize;
                                    buyOrder.orderSize = 0;
                                }
                                //If there are more shares in the buy order then in the sell
                                else if (buyOrder.orderSize > numShares)
                                {
                                    sale = true;
                                    buyOrder.orderSize -= numShares;
                                    numSold += (int)numShares;
                                    
                                }
                                //There are more shares being sold than there are in the current offer to buy
                                else if (buyOrder.orderSize < numShares)
                                {
                                    sale = true;
                                    numSold += (int)buyOrder.orderSize;
                                    //subtract the number of bid shares from the total being sold
                                    numShares -= buyOrder.orderSize;
                                    //Remove the buy order from the list because all have been sold
                                    buyOrder.orderSize = 0;
                                    //Allow continuation of the loop and look for another list with a usable price
                                }
                            }
                        }
                        //If there are still shares left to sell after checking all lists for offers, add to sell list
                        if (numShares > 0)
                        {
                            //add a new sell order with current time/date, number of shares from input, price form input
                            company.sellOrders.Add(new SellOrder(DateTime.Now, numShares, purchasePrice));
                            company.sellOrders.Sort(); //sort list with new items present
                        }
                        //if a transaction has been completed
                        if (sale)
                        {
                            company.setLastPrice(purchasePrice); //set last transaction price
                            company.setVolume((int)(numSold)); //add volume of shares sold to total by calling setVolume()
                        }
                        numSharesInput.Clear(); //clear shares input text box
                        purchasePriceInput.Clear(); //clear purchase price input box
                        data.notify(); //notify the subject to update adn reflect changes
                        return;
                    }
                }
            }
            catch
            {
                //show user that input validation was unsuccessful
                MessageBox.Show("Some fields contain improper values", "Stock Market Application");
            }
            //At this point the the boxes should be cleared even if inputs were invalid
            numSharesInput.Clear(); 
            purchasePriceInput.Clear();
            data.notify();
        }

        //Cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
