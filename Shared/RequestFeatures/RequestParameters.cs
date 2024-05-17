namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int MaxPageSize = 50;
        public int pageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize ? MaxPageSize : value); }
        }
    }
}
