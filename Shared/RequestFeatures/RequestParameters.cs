namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int MAX_PAGE_SIZE = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize 
        { 
            get 
            {              
                return _pageSize;
            }
            set
            {
                _pageSize = (value>MAX_PAGE_SIZE) ? MAX_PAGE_SIZE:value;
            }
        }

        public string? OrderBy { get; set; }
        public string? Fields { get; set; }
    }
}
