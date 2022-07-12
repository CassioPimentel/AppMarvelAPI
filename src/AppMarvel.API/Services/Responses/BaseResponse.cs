namespace AppMarvel.API.Services.Responses
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public ContainerResult Data { get; set; }
        public string Etag { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
    }
}