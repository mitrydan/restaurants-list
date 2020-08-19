namespace RestaurantsList.Requests
{
    public abstract class PagedReqest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PagedReqest()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public void Validate()
        {
            PageNumber = PageNumber < 1 ? 1 : PageNumber;
            PageSize = PageSize > 10 ? 10 : PageSize;
        }
    }
}
