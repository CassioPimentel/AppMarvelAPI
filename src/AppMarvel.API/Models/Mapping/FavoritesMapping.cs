using NHibernate.Mapping.ByCode.Conformist;

namespace AppMarvel.API.Models.Mapping
{
    public class FavoritesMapping : ClassMapping<Favorites>
    {
        public FavoritesMapping()
        {
            Id(c => c.IdCharacter);

            Table("Favorites");
        }
    }
}