using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamManager.Data.Models;

namespace TeamManager.Models
{
    public class CreatePlayerViewModel
    {
        public List<Team> Teams { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 99)]
        public int ShirtNumber { get; set; }

        [Required]
        [RegularExpression("\\b(Sturm|Abwehr|Torhüter|Mittelfeld)\\b")]
        public string Position { get; set; }

        public Player GetPlayer()
        {
            return new Player
            {
                TeamId = TeamId,
                Name = Name,
                ShirtNumber = ShirtNumber,
                Position = Position
            };
        }
    }
}
