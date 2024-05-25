namespace API.Helpers
{
    public class QueryObject
    {
        public double TransactionAmount { get; set; }
        public bool? TransactionType { get; set; }
        public int? TransactionDescription { get; set; }
        public string SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

    }
}