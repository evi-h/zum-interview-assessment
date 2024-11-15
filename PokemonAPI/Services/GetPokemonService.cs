using PokemonAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PokemonAPI.Services{

    public class GetPokemonService
    {
        private static HashSet<int> pokemonIds = new HashSet<int>();
        private static Random random = new Random();
        private static readonly HttpClient client = new HttpClient();

        private static readonly Dictionary<string, string> fightingRulesMap = new Dictionary<string, string> { { "water", "fire"}, { "fire", "grass"}, {"grass", "electric"}, {"electric", "water"}, {"ghost", "psychic"}, {"psychic","fighting"}, {"fighting","dark"}, {"dark", "ghost"} };




        public static async Task<IEnumerable<PokemonDetail>> SimulateBattlesAsync (int numOfPokemon){

            List<PokemonDetail> pokemonDetailList = await GetPokemonList(numOfPokemon);

            for (int i = 0; i < pokemonDetailList.Count() - 1; i++){
                for(int j = i + 1;  j < pokemonDetailList.Count(); j++){
                    SimulateBattle( pokemonDetailList[i],  pokemonDetailList[j]);
                }
            }

            IEnumerable<PokemonDetail> enumerablePokemonList = pokemonDetailList;
            return enumerablePokemonList;
        }

        private static void SimulateBattle( PokemonDetail pokemon1,  PokemonDetail pokemon2){

            if(IsWinner(pokemon1,pokemon2)){
                pokemon1.Wins++;
                pokemon2.Losses++;
            }else if(IsWinner(pokemon2,pokemon1)){
                pokemon2.Wins++;
                pokemon1.Losses++;
            }else{
                pokemon1.Ties++;
                pokemon2.Ties++;
            }
        }

        private static bool IsWinner(PokemonDetail pokemon1,  PokemonDetail pokemon2){
            return (fightingRulesMap.ContainsKey(pokemon1.Type) && fightingRulesMap[pokemon1.Type] == pokemon2.Type) || pokemon1.BaseExperience > pokemon2.BaseExperience;
        }




        private static async Task<List<PokemonDetail>> GetPokemonList(int numOfPokemon){

            int minId = 1;
            int maxId = 152;
            int range = maxId - minId;
            List<PokemonDetail> pokemonList = new List<PokemonDetail>();

            while(pokemonList.Count < numOfPokemon && pokemonIds.Count < range){
                int randomId = random.Next(minId, maxId);
                if(!pokemonIds.Contains(randomId)){
                    pokemonIds.Add(randomId);
                    PokemonDetail pokemonDetail = await GetPokemonAsync(randomId);
                    if(pokemonDetail != null){
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
