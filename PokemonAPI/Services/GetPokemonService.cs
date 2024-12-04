using PokemonAPI.Models;
using Newtonsoft.Json;

namespace PokemonAPI.Services
{

    public class GetPokemonService
    {
        private static readonly HttpClient client = new HttpClient();

        private enum BattleResult
        {
            Win,
            OpponentWin,
            Tie,
        }

        private static readonly Dictionary<string, string> fightingRulesMap = new Dictionary<string, string> {
            {"water", "fire"},
            {"fire", "grass"},
            {"grass", "electric"},
            {"electric", "water"},
            {"ghost", "psychic"},
            {"psychic","fighting"},
            {"fighting","dark"},
            {"dark", "ghost"}
        };

        public static void SimulateBattles(List<PokemonDetail> pokemonDetailList)
        {
            for (int i = 0; i < pokemonDetailList.Count() - 1; i++)
            {
                for (int j = i + 1; j < pokemonDetailList.Count(); j++)
                {
                    SetScore(pokemonDetailList[i], pokemonDetailList[j]);
                }
            }
        }

        private static void SetScore(PokemonDetail pokemon1, PokemonDetail pokemon2)
        {
            switch (SimulateBattle(pokemon1, pokemon2))
            {
                case BattleResult.Win:
                    pokemon1.Wins++;
                    pokemon2.Losses++;
                    break;
                case BattleResult.OpponentWin:
                    pokemon2.Wins++;
                    pokemon1.Losses++;
                    break;
                case BattleResult.Tie:
                    pokemon1.Ties++;
                    pokemon2.Ties++;
                    break;
            }
        }

        private static BattleResult SimulateBattle(PokemonDetail pokemon1, PokemonDetail pokemon2)
        {
            if (fightingRulesMap.ContainsKey(pokemon1.Type))
            {
                if (fightingRulesMap[pokemon1.Type] == pokemon2.Type)
                {
                    return BattleResult.Win;
                }
            }

            if (fightingRulesMap.ContainsKey(pokemon2.Type))
            {
                if (fightingRulesMap[pokemon2.Type] == pokemon1.Type)
                {
                    return BattleResult.OpponentWin;
                }
            }

            if (pokemon1.BaseExperience > pokemon2.BaseExperience)
            {
                return BattleResult.Win;
            }
            else if (pokemon1.BaseExperience < pokemon2.BaseExperience)
            {
                return BattleResult.OpponentWin;
            }
            else
            {
                return BattleResult.Tie;
            }
        }

        public static async Task<List<PokemonDetail>> GetPokemonListAsync(int numOfPokemon)
        {
            int minId = 1;
            int maxId = 151;
            int range = maxId - minId;
            int tries = 1;
            int maxRetries = 10;

            List<PokemonDetail> pokemonList = new List<PokemonDetail>();
            Random random = new Random();

            HashSet<int> pokemonIds = new HashSet<int>();

            while (pokemonList.Count < numOfPokemon && pokemonIds.Count <= range && tries <= maxRetries)
            {
                int randomId = random.Next(minId, maxId + 1);
                if (!pokemonIds.Contains(randomId))
                {
                    try
                    {
                        PokemonDetail pokemonDetail = await GetPokemonAsync(randomId);

                        pokemonIds.Add(randomId);
                        pokemonList.Add(pokemonDetail);
                    }
                    catch (Exception)
                    {
                        tries++;
                    };
                };
            }

            if (tries > maxRetries)
            {
                throw new Exception("Max retries, try again");
            }

            return pokemonList;
        }

        private static async Task<PokemonDetail> GetPokemonAsync(int pokemonId)
        {
            try
            {
                // Specify the URL of the API endpoint
                string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonId}";

                // Call the GET API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse JSON into a dynamic object
                dynamic? jsonObject = JsonConvert.DeserializeObject(responseBody);

                // Process the response body
                return new PokemonDetail
                {
                    Id = pokemonId,
                    Name = jsonObject?.forms[0].name.ToString() ?? "",
                    Type = jsonObject?.types[0].type.name.ToString() ?? "",
                    BaseExperience = jsonObject?.base_experience ?? 0,
                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
        }
    }
}
