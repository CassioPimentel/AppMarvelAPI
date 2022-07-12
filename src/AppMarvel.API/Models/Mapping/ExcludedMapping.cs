using NHibernate.Mapping.ByCode.Conformist;

namespace AppMarvel.API.Models.Mapping
{
    public class ExcludedMapping : ClassMapping<Excluded>
    {
        public ExcludedMapping()
        {
            Id(c => c.IdCharacter);

            Table("Excluded");
        }
    }
}