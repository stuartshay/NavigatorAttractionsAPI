namespace NavigatorAttractions.Core.Models
{
    public class PagedResultModel<T>
    {
        public long Total { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public int From
        {
            get
            {
                if (this.Total == 0)
                    return 0;

                return ((Page - 1) * Limit) + 1;
            }
        }

        public int To
        {
            get
            {
                if (this.Total < this.Page * this.Limit)
                    return (int)this.Total;

                return Page * Limit;
            }
        }

        public List<dynamic> Results { get; set; }
    }
}