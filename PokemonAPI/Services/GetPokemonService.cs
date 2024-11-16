using PokemonAPI.Models;
using Newtonsoft.Json;

namespace PokemonAPI.Services{

    public class GetPokemonService
    {
        private static HashSet<int> pokemonIds = new HashSet<int>();
        private static Random random = new Random();
        private static readonly HttpClient client = new HttpClient();

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

        public static List<PokemonDetail> SimulateBattles(List<PokemonDetail> pokemonDetailList){

            for(int i = 0; i < pokemonDetailList.Count() - 1; i++){
                for(int j = i + 1;  j < pokemonDetailList.Count(); j++){
                    GetResult( pokemonDetailList[i],  pokemonDetailList[j]);
                }
            }

            return pokemonDetailList;
        }

        private static void GetResult(PokemonDetail pokemon1,  PokemonDetail pokemon2){

            int result = SimulateBattle(pokemon1,pokemon2);

            if(result == 1){
                pokemon1.Wins++;
                pokemon2.Losses++;
            }else if(result == 2){
                pokemon2.Wins++;
                pokemon1.Losses++;
            }else{
                pokemon1.Ties++;
                pokemon2.Ties++;
            }
        }

        private static int SimulateBattle(PokemonDetail pokemon1,  PokemonDetail pokemon2){
            
            if(fightingRulesMap.ContainsKey(pokemon1.Type) && fightingRulesMap.ContainsKey(pokemon2.Type)){
                if(fightingRulesMap[pokemon1.Type] == pokemon2.Type){
                    return 1; // pokemon 1 wins
                }else if(fightingRulesMap[pokemon2.Type] == pokemon1.Type){
                    return 2; // opponent wins
                }
            }
            
            if(pokemon1.BaseExperience > pokemon2.BaseExperience){
                return 1;
            }else if(pokemon1.BaseExperience < pokemon2.BaseExperience){
                return 2;
            }else{
                return 0; // tie
            }

        }

        public static async Task<List<PokemonDetail>> GetPokemonListAsync(int numOfPokemon){

            int minId = 1;
            int maxId = 152;
            int range = maxId - minId;
            List<PokemonDetail> pokemonList = new List<PokemonDetail>();

            while(pokemonList.Count < numOfPokemon && pokemonIds.Count < range){
                int randomId = random.Next(minId, maxId);
                if(!pokemonIds.Contains(randomId)){
                    PokemonDetail pokemonDetail = await GetPokemonAsync(randomId);
                    if(pokemonDetail != null){
                        pokemonIds.Add(randomId);
                        pokemonList.Add(pokemonDetail);
                    }
                };
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
                dynamic jsonObject = JsonConvert.DeserializeObject(responseBody);

                
                // // Display or process the response body
                 return new PokemonDetail{
                    Id = pokemonId,
                    Name = jsonObject.forms[0].name.ToString(),
                    Type = jsonObject.types[0].type.name.ToString(),
                    BaseExperience = jsonObject.base_experience,
                 };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }


            // Return a default or null value in case of an error
            return null;
        }
    }
}
