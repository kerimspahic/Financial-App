namespace API.Response
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(IEnumerable<T> data, int pageNumber, int pageSize)
        {
            Data = data.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            Total = data.Count();
        }

        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}