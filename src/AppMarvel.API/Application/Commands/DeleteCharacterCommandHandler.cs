using AppMarvel.API.Models;
using MediatR;
using NHibernate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppMarvel.API.Application.Commands
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Excluded>
    {
        private readonly ISession _session;
        public DeleteCharacterCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task<Excluded> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var excluded = await _session.GetAsync<Excluded>(request.Id);

            if (excluded != null) return null;

            ITransaction transaction = null;

            excluded = new Excluded { IdCharacter = request.Id };

            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(excluded);
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

            return excluded;
        }
    }
}