using AppMarvel.API.Models;
using MediatR;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppMarvel.API.Application.Queries
{
    public class GetCharacterDeletedListQueryHandler : IRequestHandler<GetCharacterDeletedListQuery, IEnumerable<Excluded>>
    {
        private readonly ISession _session;
        public GetCharacterDeletedListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Excluded>> Handle(GetCharacterDeletedListQuery request, CancellationToken cancellationToken)
        {
            var tt = _session.Query<Excluded>().ToList();

            var excluded = tt;

            if (excluded == null) return null;

            return excluded;
        }
    }
}