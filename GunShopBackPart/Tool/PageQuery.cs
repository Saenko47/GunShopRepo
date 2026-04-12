namespace GunShopBackPart.Tool
{
    public class PageQuery
    {
        private const int MaxPageSize = 20;

        public int Page { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
