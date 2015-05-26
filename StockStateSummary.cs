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
    public partial class StockStateSummary : Form, StockMarketDisplay
    {
        RealTimeData sub;
        public StockStateSummary()
        {
            InitializeComponent();
        }
        //Used to remove this observer from the list of notified observers
        protected override void OnClosing(CancelEventArgs e)
        {
            sub.unRegister(this);
        }

        //Will fill all applicable columns of stock state summary for all companies in the subject
        public void update(RealTimeData sub)
        {
            this.sub = sub;
            dataGridView1.Rows.Clear();
            // Create an unbound DataGridView by declaring a column count, prevents mising header after clear
            dataGridView1.ColumnCount = 8;
            dataGridView1.ColumnHeadersVisible = true;
            foreach (Company company in sub.companies)
                this.dataGridView1.Rows.Add(company.companyName, company.symbol, company.openPrice, company.getLastPrice(), company.priceChange,company.picture, company.percentChange, company.getVolume()); 
        }
    }
}
