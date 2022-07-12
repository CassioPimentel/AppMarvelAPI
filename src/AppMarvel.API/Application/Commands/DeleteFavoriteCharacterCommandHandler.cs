using AppMarvel.API.Models;
using MediatR;
using NHibernate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppMarvel.API.Application.Commands
{
    public class DeleteFavoriteCharacterCommandHandler : IRequestHandler<DeleteFavoriteCharacterCommand, Favorites>
    {
        private readonly ISession _session;
        public DeleteFavoriteCharacterCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task<Favorites> Handle(DeleteFavoriteCharacterCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _session.GetAsync<Favorites>(request.Id);

            if (favorite == null) return null;

            ITransaction transaction = null;

            try
            {
                transaction = _session.BeginTransaction();
                await _session.DeleteAsync(favorite);
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
    }
}