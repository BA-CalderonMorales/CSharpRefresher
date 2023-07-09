using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.NHibernate101
{
    public enum CustomerCreditRating
    {
        Excellent, Good, Neutral, Poor, Terrible
    }

    public class Customer
    {
        public Customer()
        {
            MemberSince = DateTime.UtcNow;
            Orders = new HashSet<Order>(); // default here so it's always initialized
        }

        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Points { get; set; }
        public virtual bool HasGoldStatus { get; set; }
        public virtual DateTime MemberSince { get; set; }
        public virtual CustomerCreditRating CreditRating { get; set; }
        public virtual double AverageRating { get; set; }
        public virtual Location Address { get; set; }

        public virtual ISet<Order> Orders { get; set; } // to use <set> in xml
        //public virtual IList<Order> Orders { get; set; } // to use <bag> in xml

        public virtual void AddOrder(Order order)
        {
            Orders.Add(order);
            order.Customer = this;
        }

        public override string ToString()
        {
            return $"Customer Information:\n\nFirst Name: {this.FirstName}\nLast Name: {this.LastName}";
        }
    }
}
