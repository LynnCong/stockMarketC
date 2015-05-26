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
    public partial class StockSecuritiesExchange : Form
    {
        //create instance of subject
        static RealTimeData data = new RealTimeData();

        public StockSecuritiesExchange()
        {
            this.Icon = StockMarketApplication.Properties.Resources.Eng;
            InitializeComponent();
             //Hardcode companies into system temporarily
            data.companies.Add(new Company("Microsoft Corporation", "MSFT", (float)46.13, 0));
            data.companies.Add(new Company("Apple Inc.", "AAPL", (float)105.22, 0));
            data.companies.Add(new Company("Facebook, Inc.", "FB", (float)80.67, 0));

            //company names as buttons in watch->ORder
            ToolStripMenuItem[] orderItems = new ToolStripMenuItem[data.companies.Count];
            //company names as buttons in watch->Price
            ToolStripMenuItem[] priceItems = new ToolStripMenuItem[data.companies.Count];

            //Populate List elements
            for (int i = 0; i < data.companies.Count; i++ )
            {
                //add a blank element to each list
                orderItems[i] = new ToolStripMenuItem();
                priceItems[i] = new ToolStripMenuItem();

                //add the company name to each list
                orderItems[i].Name = data.companies.ElementAt(i).companyName;
                priceItems[i].Name = data.companies.ElementAt(i).companyName;
                orderItems[i].Tag = i;
                priceItems[i].Tag = i;

                //change the text that is displayed to a company name for each list
                orderItems[i].Text = data.companies.ElementAt(i).companyName;
                priceItems[i].Text = data.companies.ElementAt(i).companyName;

                //Add the event handlers to each list element
                orderItems[i].Click += new EventHandler(MarketByOrderHandler);
                priceItems[i].Click += new EventHandler(MarketByPriceHandler);
            }
            //add the list of items to the ToolStripMenu
            this.marketByPriceToolStripMenuItem.DropDownItems.AddRange(priceItems);
            this.marketByOrderToolStripMenuItem.DropDownItems.AddRange(orderItems);
        }

        static int Main(string[] args)
        {
            //Main application logic
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StockSecuritiesExchange());
            return 0;
        }

        //Used to add functionality for when a company name is selected in market by order
        private void MarketByOrderHandler(object sender, EventArgs e)
        {
            //item that will hold configurations
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            //Create MDI window
            MarketByOrder newMDIChild = new MarketByOrder(clickedItem.Name);
            //Register the observer
            data.register(newMDIChild);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            //Change name of the window
            newMDIChild.Text = "Market Depth By Order ("+clickedItem.Name+")";
            // Display the new form.
            newMDIChild.Show();
            data.notify();
        }

        //Used to add functionality for when a company name is selected in market by price
        private void MarketByPriceHandler(object sender, EventArgs e)
        {
            //Item that will hold configuration
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            //Create MDI window
            MarketByPrice newMDIChild = new MarketByPrice(clickedItem.Name);
            //Register the observer
            data.register(newMDIChild);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            //Change name of the window
            newMDIChild.Text = "Market Depth By Price (" + clickedItem.Name + ")";
            // Display the new form.
            newMDIChild.Show();
            data.notify();
        }
        //Method hides the watch and order menu items when application is first opened
        //it also fades stop trading, because that option does not apply currently
        private void StockSecuritiesExchange_Load(object sender, EventArgs e)
        {
            watchToolStripMenuItem.Visible = false;
            ordersToolStripMenuItem.Visible = false;
            stopTradingToolStripMenuItem.Enabled = false;
        }

        //Method spawns a stock state summary window when that button is clicked
        private void stockStateSumaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockStateSummary newMDIChild = new StockStateSummary();
            //Register the observer
            data.register(newMDIChild);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
            data.notify();
        }

        //Method spawns a bid window
        private void bidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            placeBidOrderForm newMDIChild = new placeBidOrderForm(data);
            //Register the observer
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        //Handler for application exit menu item
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //When begin trading is selected, it fades that button until stop has been selected
        //this also makes additional menu buttons available to the user.
        private void beginTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            watchToolStripMenuItem.Visible = true;
            ordersToolStripMenuItem.Visible = true;
            stopTradingToolStripMenuItem.Enabled = true;
            beginTradingToolStripMenuItem.Enabled = false;
        }

        //Begin trading is reset, method does not reset trading day (total volume of transactions is not reset)
        //It could be reset easily, but not required
        private void stopTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            watchToolStripMenuItem.Visible = false;
            ordersToolStripMenuItem.Visible = false;
            stopTradingToolStripMenuItem.Enabled = false;
            beginTradingToolStripMenuItem.Enabled = true;
        }

        //Spawn sell window
        private void askToolStripMenuItem_Click(object sender, EventArgs e)
        {
            placeAskOrderForm newMDIChild = new placeAskOrderForm(data);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade); //cascade windows layout
        }

        private void horizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal); //horizontal windows layout
        }

        private void verticalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical); //vertical windows layout
        }

        
    }
}
