using AppMarvel.API.Models;
using MediatR;

namespace AppMarvel.API.Application.Commands
{
    public class DeleteCharacterCommand : IRequest<Excluded>
    {
        public int Id { get; set; }
    }
}