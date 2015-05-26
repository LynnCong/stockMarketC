using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketApplication
{
    //Must implement comparable and equatable to sort by price 
    class BuyOrder : Order, IEquatable<Order>, IComparable<Order>
    {
        //create a buy order with a time, order size and price
        public BuyOrder(DateTime time, float size, float price)
        {
            this.orderDate = time;
            this.orderSize = size;
            this.orderPrice = price;
        }
        //define two orders as equal if they have the same order price
        bool IEquatable<Order>.Equals(Order other)
        {
            if (other == null) return false; //comparing to null
            return (this.orderPrice.Equals(other.orderPrice));
        }
        int IComparable<Order>.CompareTo(Order compareOrder)
        {
            // A null value means that this object is greater. 
            if (compareOrder == null)
            {
                return 1;
            }
            else if (this.Equals(compareOrder)) //if the two objects are equal, compare by order date
            {
                return this.orderDate.CompareTo(compareOrder.orderDate);
            }
            else

                return -(this.orderPrice.CompareTo(compareOrder.orderPrice)); //compare two order prices
        }
    }

}
