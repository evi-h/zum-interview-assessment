using System.ComponentModel.DataAnnotations;


namespace PokemonAPI.Models
{

    public class PokemonDetail
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Type { get; set; }

        [Required]
        public int BaseExperience { get; set; }

        public int Wins { get; set; } = 0;

        public int Losses { get; set; } = 0;

        public int Ties { get; set; } = 0;

    }
}


