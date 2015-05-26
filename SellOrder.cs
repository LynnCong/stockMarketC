using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApplication
{
    class SellOrder : Order, IEquatable<Order>, IComparable<Order>
    {
        public SellOrder(DateTime time, float size, float price)
        {
            this.orderDate = time;
            this.orderSize = size;
            this.orderPrice = price;
        }
        bool IEquatable<Order>.Equals(Order other)
        {
            if (other == null) return false;
            return (this.orderPrice.Equals(other.orderPrice));
        }
        int IComparable<Order>.CompareTo(Order compareOrder)
        {
            // A null value means that this object is greater. 
            if (compareOrder == null)
            {
                return 1;
            }
            else if (this.Equals(compareOrder))
            {
                return this.orderDate.CompareTo(compareOrder.orderDate);
            }
            else
                return this.orderPrice.CompareTo(compareOrder.orderPrice); //sort in ascending order based on lowest price
        }
    }
}
