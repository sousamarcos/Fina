using System.Text.Json.Serialization;

namespace Fina.Core.Responses
{
    public class Response<TData>
    {
        [JsonConstructor]
        public Response() => _code = Configuration.DefaultStatusCodeResponse;

        public Response(TData? data, int code = Configuration.DefaultStatusCodeResponse, string? message = null)
        {
            Data = data;
            Message = message;
            _code = code;
        }
        private int _code = Configuration.DefaultStatusCodeResponse;

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;

        public string? Message { get; set; }
        public TData? Data { get; set; }
    }
}
