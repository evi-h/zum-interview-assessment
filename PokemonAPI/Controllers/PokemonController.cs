using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System.Linq;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{

    private readonly Dictionary<string, string> sortOptions = new Dictionary<string, string> {
        {"wins", "Wins"},
        {"losses", "Losses"},
        {"ties" , "Ties"},
        {"name" , "Name"},
        {"id" , "Id"},
    };

    [HttpGet("tournament/statistics")]
    public async Task<ActionResult<IEnumerable<PokemonDetail>>> Get(string? sortBy, string? sortDirection = "desc")
    {

        if (string.IsNullOrEmpty(sortBy))
        {
            return BadRequest($"{nameof(sortBy)} parameter is required");
        }
        else if (!sortOptions.ContainsKey(sortBy))
        {
            return BadRequest($"{nameof(sortBy)} parameter is invalid");
        }
        else if (sortDirection != "desc" && sortDirection != "asc")
        {
            return BadRequest($"{nameof(sortDirection)} parameter is invalid");
        }

        List<PokemonDetail> pokemonList = new List<PokemonDetail>();

        try
        {
            pokemonList = await GetPokemonService.GetPokemonListAsync(8);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, new { Message = "An internal error occurred.", Details = ex.Message });
        }

        GetPokemonService.SimulateBattles(pokemonList);


        var propertyInfo = typeof(PokemonDetail).GetProperty(sortOptions[sortBy]);

        return Ok(sortDirection == "desc" ?
                    pokemonList.OrderByDescending(p => propertyInfo.GetValue(p, null)) :
                    pokemonList.OrderBy(p => propertyInfo.GetValue(p, null))
        );
    }
}
