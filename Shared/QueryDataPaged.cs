namespace Shared
{
    public class QueryDataPaged : QueryData
    {
        public int rowsCount { get; set; } = 10;
        public int pageNo { get; set; } = 1;
        

    }
}
