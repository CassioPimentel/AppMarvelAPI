using AppMarvel.API.Models;
using MediatR;
using System.Collections.Generic;

namespace AppMarvel.API.Application.Queries
{
    public class GetCharacterDeletedListQuery : IRequest<IEnumerable<Excluded>>
    {
    }
}