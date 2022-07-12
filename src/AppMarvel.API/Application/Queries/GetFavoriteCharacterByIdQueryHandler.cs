using AppMarvel.API.Models;
using MediatR;
using NHibernate;
using System.Threading;
using System.Threading.Tasks;

namespace AppMarvel.API.Application.Queries
{
    public class GetFavoriteCharacterByIdQueryHandler : IRequestHandler<GetFavoriteCharacterByIdQuery, Favorites>
    {
        private readonly ISession _session;
        public GetFavoriteCharacterByIdQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<Favorites> Handle(GetFavoriteCharacterByIdQuery request, CancellationToken cancellationToken)
        {
            var favorite = await _session.GetAsync<Favorites>(request.Id);

            if (favorite == null) return null;

            return favorite;
        }
    }
}