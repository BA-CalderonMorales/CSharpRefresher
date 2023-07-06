using NHibernate.Type;

namespace Fundamentals.NHibernate101
{
    /// <summary>
    /// Helps specify that we want to use the string type of the enum instead of integer.
    /// </summary>
    public class CustomerCreditRatingType : EnumStringType<CustomerCreditRating>
    {
    }
}
