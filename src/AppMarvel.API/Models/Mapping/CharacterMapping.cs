using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppMarvel.API.Models.Mapping
{
    public class CharacterMapping : ClassMapping<Character>
    {
        public CharacterMapping()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("Id");
            });

            Table("Character");
        }
    }
}