using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System.Linq;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{

    private readonly Dictionary<string,string> sortOptions = new Dictionary<string,string> {
        {"wins", "Wins"},
        {"losses", "Losses"},
        {"ties" , "Ties"},
        {"name" , "Name"},
        {"id" , "Id"},
    };

    [HttpGet("tournament/statistics")]
    public  async Task<ActionResult<IEnumerable<PokemonDetail>>> Get(string? sortBy, string? sortDirection = "desc")
    {

        if (sortBy == null || string.IsNullOrEmpty(sortBy)){
            return BadRequest("sortBy parameter is required");
        }else if( !sortOptions.ContainsKey(sortBy)){
            return BadRequest("sortBy parameter is invalid");
        }else if( sortDirection != "desc" && sortDirection != "asc"){
            return BadRequest("sortDirection parameter is invalid");
        }

        var propertyInfo = typeof(PokemonDetail).GetProperty(sortOptions[sortBy]);

        IEnumerable<PokemonDetail> pokemonList = await GetPokemonService.SimulateBattlesAsync(8);

        
        return Ok(sortDirection == "desc" ? 
        pokemonList.OrderByDescending(p => propertyInfo.GetValue(p, null)) : 
        pokemonList.OrderBy(p => propertyInfo.GetValue(p,null))
        );
    }
}
