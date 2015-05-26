namespace StockMarketApplication
{
    partial class StockStateSummary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changeNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picture = new System.Windows.Forms.DataGridViewImageColumn();
            this.changePercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shareVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.company,
            this.symbol,
            this.openPrice,
            this.lastSale,
            this.changeNet,
            this.picture,
            this.changePercentage,
            this.shareVolume});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1094, 367);
            this.dataGridView1.TabIndex = 0;
            // 
            // company
            // 
            this.company.HeaderText = "Company";
            this.company.Name = "company";
            this.company.ReadOnly = true;
            // 
            // symbol
            // 
            this.symbol.HeaderText = "Symbol";
            this.symbol.Name = "symbol";
            this.symbol.ReadOnly = true;
            // 
            // openPrice
            // 
            this.openPrice.HeaderText = "Open Price";
            this.openPrice.Name = "openPrice";
            this.openPrice.ReadOnly = true;
            // 
            // lastSale
            // 
            this.lastSale.HeaderText = "Last Sale";
            this.lastSale.Name = "lastSale";
            this.lastSale.ReadOnly = true;
            // 
            // changeNet
            // 
            this.changeNet.HeaderText = "Change Net";
            this.changeNet.Name = "changeNet";
            this.changeNet.ReadOnly = true;
            // 
            // picture
            // 
            this.picture.HeaderText = "";
            this.picture.Name = "picture";
            this.picture.ReadOnly = true;
            this.picture.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.picture.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // changePercentage
            // 
            this.changePercentage.HeaderText = "Change %";
            this.changePercentage.Name = "changePercentage";
            this.changePercentage.ReadOnly = true;
            // 
            // shareVolume
            // 
            this.shareVolume.HeaderText = "Share Volume";
            this.shareVolume.Name = "shareVolume";
            this.shareVolume.ReadOnly = true;
            // 
            // StockStateSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 367);
            this.Controls.Add(this.dataGridView1);
            this.Name = "StockStateSummary";
            this.Text = "Stock State Summary";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn company;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn openPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn changeNet;
        private System.Windows.Forms.DataGridViewImageColumn picture;
        private System.Windows.Forms.DataGridViewTextBoxColumn changePercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn shareVolume;
    }
}