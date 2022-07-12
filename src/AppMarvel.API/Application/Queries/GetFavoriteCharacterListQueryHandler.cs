using AppMarvel.API.Models;
using MediatR;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppMarvel.API.Application.Queries
{
    public class GetFavoriteCharacterListQueryHandler : IRequestHandler<GetFavoriteCharacterListQuery, List<Favorites>>
    {
        private readonly ISession _session;
        public GetFavoriteCharacterListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<List<Favorites>> Handle(GetFavoriteCharacterListQuery request, CancellationToken cancellationToken)
        {
            var favorites = _session.Query<Favorites>().ToList();

            if (favorites == null) return null;

            return favorites;
        }
    }
}