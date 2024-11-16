namespace PokemonAPI.Tests;

using Xunit;
using PokemonAPI.Services;
using PokemonAPI.Models;

public class GetPokemonServiceTests
{

    static private List<PokemonDetail> pokemonDetailList = new List<PokemonDetail>
    {
        new PokemonDetail { Id = 1, Name = "arbok", Type="electric", BaseExperience = 157},
        new PokemonDetail { Id = 2, Name = "zubat", Type="ghost", BaseExperience = 49},
        new PokemonDetail { Id = 3, Name = "weepinbell", Type="grass", BaseExperience = 137 },
        new PokemonDetail { Id = 4, Name = "kingler", Type="water", BaseExperience = 166 }
    };

   [Fact]
    public void SimulateBattles_UpdatesPokemonDetailsCorrectly()
    {
        // Act: Simulate battles
        var result = GetPokemonService.SimulateBattles(pokemonDetailList);

        // Assert: Check if the Pokemon details were updated correctly
        Assert.Equal(4, result.Count); // Ensure the list has 4 Pokémon
        Assert.Equal(2, result[0].Wins); // Check if arbok wins were updated
        Assert.Equal(1, result[2].Losses); // Check if weepinbell losses were updated
        Assert.Equal(0, result[3].Ties); // Make sure kingler ties are still 0
    }
}