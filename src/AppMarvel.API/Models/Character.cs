using System;

namespace AppMarvel.API.Models
{
    public class Character
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual string ResourceURI { get; set; }
    }
}