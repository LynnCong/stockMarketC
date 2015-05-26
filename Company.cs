using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StockMarketApplication
{
    public class Company
    {
        //Images are loaded form application bundled resources
        public Bitmap up = new Bitmap(StockMarketApplication.Properties.Resources.up); //image for increased value
        public Bitmap down = new Bitmap(StockMarketApplication.Properties.Resources.down); //image for decreased value
        public Bitmap noChange = new Bitmap(StockMarketApplication.Properties.Resources.noChange); //image for consistent value
        public Bitmap picture = new Bitmap(StockMarketApplication.Properties.Resources.noChange);

        //Company name and symbol identifiers
        public String companyName = ""; 
        public String symbol = "";
        
        //Company evaluation fields
        public float openPrice = 0;
        private float lastSale = 0;
        public float priceChange = 0;
        public float percentChange = 0;
        private int volume = 0;

        //Lists of orders, separated buy bids and asks
        public List<Order> buyOrders = new List<Order>();
        public List<Order> sellOrders = new List<Order>();

        public Company(String name, String symbol, float openPrice, float lastSale)
        {
            this.companyName = name;
            this.symbol = symbol;
            this.openPrice = openPrice;
            this.lastSale = lastSale;
            this.picture = noChange;
        }
        //used to change the open price
        public void setopenPrice(float price)
        {
            this.openPrice = price;
        }
        //When last price is changes, the picture will be set accordingly, if there is any increase or decrease
        //this method will set all calculated values that are set according to last price changes, this includes:
        //picure, last sale, price change, and percentage change
        public void setLastPrice(float salePrice)
        {
            this.lastSale = salePrice;
            this.priceChange = salePrice - openPrice;

            if (priceChange < 0)
                picture = down;
            else if (priceChange == 0)
                picture = noChange;
            else
                picture = up;

            this.percentChange = priceChange / openPrice * 100; 
        }
        //Return the last price
        public float getLastPrice()
        {
            return this.lastSale;
        }
        public int getVolume()
        {
            return this.volume;
        }
        //Set volume will increasae the volume of shares in completed transactions by adding additional shares to the total
        //this total is only reset by exiting the application, it is assumed that with future implementation, the
        //volume will be reset after the trading day has finished
        public void setVolume(int volume)
        {
            this.volume += volume;
        }
    }
}
