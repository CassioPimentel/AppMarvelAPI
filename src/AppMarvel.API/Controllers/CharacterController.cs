using AppMarvel.API.Application.Commands;
using AppMarvel.API.Application.Queries;
using AppMarvel.API.Models;
using AppMarvel.API.Services;
using AppMarvel.API.Services.Parameters;
using AppMarvel.API.Services.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMarvel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private CharacterService _characterService;
        private IMediator _mediator;

        public CharacterController(CharacterService characterService,
                                   IMediator mediator)
        {
            _characterService = characterService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Favorites>> GetAllCharacter([FromQuery] CharacterParameter characterParameter)
        {
            var characters = _characterService.GetAll(characterParameter);

            if (characters == null) return NotFound();

            characters = OrderCharacterFavoritesResult(characters);

            characters = RemoveCharacterDeletedResult(characters);

            return Ok(await characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Favorites>> GetCharacter(int id)
        {
            var character = _characterService.GetById(id);

            if (character == null) return NotFound();

            character = RemoveCharacterDeletedResult(character);

            return Ok(await character);
        }

        private Task<BaseResponse> RemoveCharacterDeletedResult(Task<BaseResponse> character)
        {
            var excluded = _mediator.Send(new GetCharacterDeletedListQuery()).Result;

            foreach (var item in excluded)
            {
                if (item != null)
                {
                    character.Result.Data.Results.RemoveAll(x => x.Id == item.IdCharacter);
                }   
            }

            return character;
        }

        private Task<BaseResponse> OrderCharacterFavoritesResult(Task<BaseResponse> character)
        {
            var favorites = _mediator.Send(new GetFavoriteCharacterListQuery()).Result;

            List<int> list = favorites.Select(x => x.IdCharacter).ToList();

            character.Result.Data.Results = character.Result.Data.Results.OrderBy(x =>
            {
                var index = list.IndexOf(x.Id);

                if (index == -1)
                    index = Int32.MaxValue;

                return index;
            }).ToList();

            return character;
        }

        [HttpPost("AddCharacterFavorite/{id}")]
        public async Task<ActionResult> AddCharacterFavorite(int id)
        {
            var favorite = _mediator.Send(new CreateFavoriteCharacterCommand { Id = id });

            if (favorite == null) return BadRequest();

            return Ok(await favorite);
        }

        [HttpDelete("DeleteCharacterFavorite/{id}")]
        public async Task<ActionResult> RemoveCharacterFavorite(int id)
        {
            var favorite = _mediator.Send(new DeleteFavoriteCharacterCommand { Id = id });

            if (favorite == null) return BadRequest();

            return Ok(await favorite);
        }

        [HttpDelete("DeleteCharacter/{id}")]
        public async Task<ActionResult> RemoveCharacter(int id)
        {
            var excluded = _mediator.Send(new DeleteCharacterCommand { Id = id });

            if (excluded == null) return BadRequest();

            return Ok(await excluded);
        }
    }
}