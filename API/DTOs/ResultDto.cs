namespace API.DTOs
{
    public class ResultDto<TResponse>
    {
        public const string Type = "ResultDto";
        public bool IsSucces { get; set; }
        public TResponse Response { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public ResultDto(bool isSucces, TResponse response, IEnumerable<string> errors)
        {
            IsSucces = isSucces;
            Response = response;
            Errors = errors;
        }
    }
}