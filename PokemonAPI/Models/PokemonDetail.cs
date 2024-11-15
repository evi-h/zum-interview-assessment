using System;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;



namespace PokemonAPI.Models{

    public class PokemonDetail{
        
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int BaseExperience { get; set; }
        
        public int Wins { get; set; } = 0;

        public int Losses { get; set; } = 0;

        public int Ties { get; set; } = 0;

    }
}


