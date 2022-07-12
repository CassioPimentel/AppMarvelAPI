using System.Collections.Generic;

namespace AppMarvel.API.Services.Responses
{
    public class ContainerResult
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<CharacterResponse> Results { get; set; }
    }
}