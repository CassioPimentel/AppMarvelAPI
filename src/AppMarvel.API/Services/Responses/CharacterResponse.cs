using System;

namespace AppMarvel.API.Services.Responses
{
    public class CharacterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
    }
}