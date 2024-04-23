namespace API.Helpers
{
    public class QueryObject
    {
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDescription { get; set; }
        public string SortBy { get; set; }
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

    }
}