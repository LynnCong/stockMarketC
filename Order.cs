using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApplication
{
    public abstract class Order : IEquatable<Order>, IComparable<Order>
    {
        public DateTime orderDate;
        public float orderSize;
        public float orderPrice;
        
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
                return this.orderPrice.CompareTo(compareOrder.orderPrice); //sort in ascending order unless overridden
        }
    }
}
