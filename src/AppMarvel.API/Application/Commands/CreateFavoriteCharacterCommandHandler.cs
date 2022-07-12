using AppMarvel.API.Models;
using MediatR;
using NHibernate;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppMarvel.API.Application.Commands
{
    public class CreateFavoriteCharacterCommandHandler : IRequestHandler<CreateFavoriteCharacterCommand, Favorites>
    {
        private int limitFavorite = 5;
        private readonly ISession _session;
        public CreateFavoriteCharacterCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task<Favorites> Handle(CreateFavoriteCharacterCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _session.GetAsync<Favorites>(request.Id);

            if (favorite != null) return null;

            if(LimitFavorites()) return null;

            ITransaction transaction = null;

            favorite = new Favorites { IdCharacter = request.Id };

            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(favorite);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }

            return favorite;
        }

        public bool LimitFavorites()
        {
            return _session.Query<Favorites>().Count() == limitFavorite;
        }
    }
}