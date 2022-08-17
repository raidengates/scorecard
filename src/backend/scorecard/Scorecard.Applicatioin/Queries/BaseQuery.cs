using Kledex.Queries;
namespace Scorecard.Applicatioin.Queries
{
    public class BaseQuery<T> : BaseQuery, IQuery<T>
    {
    }
    public class BaseQuery
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public List<OrderBy> OrderBy { get; set; } = new List<OrderBy>();
    }
    public class OrderBy
    {
        public string PropertyName { get; set; }

        public bool Desc { get; set; }
    }
}
