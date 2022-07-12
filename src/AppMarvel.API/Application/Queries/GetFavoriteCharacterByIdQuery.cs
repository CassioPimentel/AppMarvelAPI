using AppMarvel.API.Models;
using MediatR;

namespace AppMarvel.API.Application.Queries
{
    public class GetFavoriteCharacterByIdQuery : IRequest<Favorites>
    {
        public int Id { get; set; }
    }
}