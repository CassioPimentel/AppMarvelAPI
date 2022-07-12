using AppMarvel.API.Models;
using MediatR;

namespace AppMarvel.API.Application.Commands
{
    public class DeleteFavoriteCharacterCommand : IRequest<Favorites>
    {
        public int Id { get; set; }
    }
}